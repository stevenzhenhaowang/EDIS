using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular.Models.BindingModels;
using EDISAngular;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using EDISAngular.Models;
using EDISAngular.Providers;
using EDISAngular.Results;
using System.Web.Security;
using EDISAngular.Services;
using EDISAngular.Infrastructure.Authorization;
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DbFirst;
using Domain.Portfolio.Entities.CreationModels;
using SqlRepository;
using System.Web;

namespace EDISAngular.APIControllers
{
    public class EntityClientController : ApiController
    {
        [HttpPost, Route("api/Entityclient/Create")]
        public IHttpActionResult adviserCreateNewClient_Entity(EntityClientCreationBindingModel model)
        {
            if (model != null)          //ModelState.IsValid && 
            {
                using (EdisRepository edisRepo = new EdisRepository())
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
                        EntityName = model.nameOfEntity,
                        EntityType = model.typeOfEntity,
                        ABN = model.abn,
                        ACN = model.acn,
                        Email = model.email,
                        Phone = model.contactPhone,
                        ClientType = BusinessLayerParameters.clientType_entity,
                        GroupNumber = model.existingGroupId
                    };

                    string code = userManager.GenerateEmailConfirmationTokenAsync(user.Id).Result;
                    string path = HttpContext.Current.Server.MapPath("~/EmailTemplate/ConfirmEmail.html");
                    var Url = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Current.Request.Url.Scheme);
                    string content = System.IO.File.ReadAllText(path).Replace("###Name###", user.UserName).Replace("###Confirm###", callbackUrl);
                    userManager.SendEmailAsync(user.Id, "Confirm your account", content);


                    #endregion

                    #region insert both records to db

                    edisRepo.CreateNewClientSync(client);
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
                    //        RetirementAge = string.IsNullOrEmpty(riskProfile.retirementAge)? (int?)null: Convert.ToInt32(riskProfile.retirementAge),
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
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
