using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EDISAngular.Models.BindingModels;
using EDISAngular;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.Owin;
using EDISAngular.Models;
using EDISAngular.Providers;
using EDISAngular.Results;
using System.Web.Security;
using EDISAngular.Services;
using EDISAngular.Infrastructure.Authorization;
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DbFirst;
using Domain.Portfolio.Entities.CreationModels;
using Client = Domain.Portfolio.AggregateRoots.Client;
using ClientGroup = Domain.Portfolio.AggregateRoots.ClientGroup;
using SqlRepository;
using Novacode;
using Shared;




namespace EDISAngular.APIControllers
{
    public class PersonClientController : ApiController
    {
        EdisRepository edisRepo = new EdisRepository();

        [HttpPost, Route("api/Personclient/Create")]
        public IHttpActionResult adviserCreateNewClient_Person(PersonClientCreationBindingModel model)
        {
            //if (!ModelState.IsValid) {
            //    var errors = ModelState.Select(x => x.Value.Errors)
            //               .Where(y => y.Count > 0)
            //               .ToList();
            //}
            if (model != null)                                          //  deleted   && ModelState.IsValid         must put it back afterward
            {
                
                #region create asp user and send email
                var user = new ApplicationUser
                {
                    Email = model.email,
                    UserName = model.email,
                    PhoneNumber = model.contactPhone
                };
                var password = "123456";//Membership.GeneratePassword(10, 0);
                var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
                userManager.Create(user, password);
                userManager.AddToRole(user.Id, AuthorizationRoles.Role_Preclient);
                //EmailUtilities.SendEmailToUser(user.Id, "", "", "");//send password
                #endregion

                #region create client profile
                ClientRegistration client = new ClientRegistration
                {
                    CreateOn = DateTime.Now,
                    ClientNumber = user.Id,
                    FirstName = model.firstName,
                    MiddleName = model.middleName,
                    Email = model.email,
                    LastName = model.lastName,
                    Phone = model.contactPhone,                        
                    ClientType = BusinessLayerParameters.clientType_person,                      
                };
                #endregion

                #region create client group or add to existing group
                if (model.isGroupLeader.HasValue && model.isGroupLeader.Value)
                {
                    var adviserNumber = User.Identity.GetUserId();
                    ClientGroupRegistration group = new ClientGroupRegistration
                    {
//#warning adviser number needs replacement
                        AdviserNumber = adviserNumber,                            
                        GroupAlias = model.newGroupAlias,
                        GroupName = model.newGroupAccountName,
                        CreateOn = DateTime.Now,
                        client = client,
                    };
                    edisRepo.CreateNewClientGroupSync(group);
                }
                else
                {
                    client.GroupNumber = model.existingGroupId;
                    edisRepo.CreateNewClientSync(client);
                }

                using (DocX document = DocX.Create("C:\\Test\\"+ client.FirstName + "_" + client.LastName +".docx"))
                {
                    Paragraph paragraph = document.InsertParagraph();
                    paragraph.AppendLine(ClientDocInfo.FirstName + model.firstName);
                    paragraph.AppendLine(ClientDocInfo.MiddleName + model.middleName);
                    paragraph.AppendLine(ClientDocInfo.LastName + model.lastName);
                    paragraph.AppendLine(ClientDocInfo.Email + model.email);
                    paragraph.AppendLine(ClientDocInfo.ContactPhone + model.contactPhone);
                    document.Save();
                }

                string code = userManager.GenerateEmailConfirmationTokenAsync(user.Id).Result;
                string path = HttpContext.Current.Server.MapPath("~/EmailTemplate/ConfirmEmail.html");
                var Url = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Current.Request.Url.Scheme);
                string content = System.IO.File.ReadAllText(path).Replace("###Name###", user.UserName).Replace("###Confirm###", callbackUrl);
                userManager.SendEmailAsync(user.Id, "Confirm your account", content);

                #endregion


                //#region create risk profile if present
                //if (model.riskProfile != null)
                //{
                //    var riskProfile = model.riskProfile;
                //    RiskProfile profile = new RiskProfile
                //    {
                //        CapitalLossAttitude = riskProfile.capitalLossAttitude,
                //        ClientID = client.ClientUserID,
                //        Comments = riskProfile.comments,
                //        DateCreated = DateTime.Now,
                //        DateModified = DateTime.Now,
                //        IncomeSource = riskProfile.incomeSource,
                //        InvestmentKnowledge = riskProfile.investmentKnowledge,
                //        InvestmentObjective1 = riskProfile.investmentObjective1,
                //        InvestmentObjective2 = riskProfile.investmentObjective2,
                //        InvestmentObjective3 = riskProfile.investmentObjective3,
                //        InvestmentProfile = riskProfile.investmentProfile,
                //        InvestmentTimeHorizon = riskProfile.investmentTimeHorizon,
                //        LongTermGoal1 = riskProfile.longTermGoal1,
                //        LongTermGoal2 = riskProfile.longTermGoal2,
                //        LongTermGoal3 = riskProfile.longTermGoal3,
                //        MedTermGoal1 = riskProfile.medTermGoal1,
                //        MedTermGoal2 = riskProfile.medTermGoal2,
                //        MedTermGoal3 = riskProfile.medTermGoal3,
                //        RetirementAge = string.IsNullOrEmpty(riskProfile.retirementAge) ? (int?)null : Convert.ToInt32(riskProfile.retirementAge),
                //        RetirementIncome = riskProfile.retirementIncome,
                //        RiskProfileID = Guid.NewGuid().ToString(),
                //        RiskAttitude = riskProfile.riskAttitude,
                //        ShortTermAssetPercent = riskProfile.shortTermAssetPercent,
                //        ShortTermEquityPercent = riskProfile.shortTermEquityPercent,
                //        ShortTermGoal1 = riskProfile.shortTermGoal1,
                //        ShortTermGoal2 = riskProfile.shortTermGoal2,
                //        ShortTermGoal3 = riskProfile.shortTermGoal3,
                //        ShortTermTrading = riskProfile.shortTermTrading
                //    };
                //    db.RiskProfiles.Add(profile);
                //}
                //#endregion

                //#region save all changes and return ok
                //db.SaveChanges();
                return Ok();
                //#endregion

            }
            else
            { 
                return BadRequest(ModelState);
            }
        }

        [HttpGet, Route("api/Personclient/GetAllGlientGroup")]
        public List<PersonClientCreationBindingModel> GetAllGlientGroup()
        {
            List<ClientGroup> group = edisRepo.GetAllClientGroupsForAdviser(User.Identity.GetUserId(), DateTime.Now).Result;
            List<PersonClientCreationBindingModel> modelList = new List<PersonClientCreationBindingModel>();

            for (int i = 0; i < modelList.Count; i++ )
            {
                PersonClientCreationBindingModel model = new PersonClientCreationBindingModel()
                {
                    newGroupAccountName = group[i].GroupName
                };
            }

            return modelList;
        }

    }
}
