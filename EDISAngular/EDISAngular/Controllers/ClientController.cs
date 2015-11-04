using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EDISAngular.Services;
using EDISAngular;
using Microsoft.AspNet.Identity;
using EDISAngular.Models.ViewModels;
using EDISAngular.Models.BindingModels;
using System.Data.Entity;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Infrastructure.Authorization;
using SqlRepository;
using Domain.Portfolio.Entities.CreationModels;
using Microsoft.AspNet.Identity.Owin;
using Domain.Portfolio.EdisDatabase;
using Shared;

namespace EDISAngular.Controllers
{

    public class ClientController : Controller
    {
        private EdisRepository edisRopo = new EdisRepository();
        private ApplicationUserManager _userManager;
        //private ClientRepository clientRepo = new ClientRepository();

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = AuthorizationRoles.Role_Client)]           //delete  + "," + AuthorizationRoles.Role_Client) 
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = AuthorizationRoles.Role_Preclient + "," + AuthorizationRoles.Role_Client)]
        public ActionResult CompleteProfile()
        {
            var userId = User.Identity.GetUserId();
            var client = edisRopo.GetClientSync(userId, DateTime.Now);
            PreClientViewModel model = new PreClientViewModel();
            model.ClientType = client.ClientType;


            return PartialView(model);
            //return PartialView(clientRepo.GetPersonClientProfileBindingModel(userId));
            //return View(clientRepo.PopulatePreclientProfileAndReturnViewModel(userId));
        }

        //[Authorize(Roles = AuthorizationRoles.Role_Preclient + "," + AuthorizationRoles.Role_Client)]
        //public void UpdateClientProfile() {
        //    var userId = User.Identity.GetUserId();
        //    var client = edisRopo.GetClientSync(userId, DateTime.Now);
        //    if (client.ClientType == "person")
        //        CompletePersonProfile();
        //    else
        //        CompleteEntityProfile();
        //}



        [Authorize(Roles = AuthorizationRoles.Role_Preclient + "," + AuthorizationRoles.Role_Client)]
        public PartialViewResult CompletePersonProfile()
        {
            var userId = User.Identity.GetUserId();
            var client = edisRopo.GetClientSync(userId, DateTime.Now);
            var riskProfile = edisRopo.getRiskProfileForClient(client.Id);

            RiskProfileBindingModel riskModel = new RiskProfileBindingModel {
                capitalLossAttitude = riskProfile.CapitalLossAttitude,
                clientId = riskProfile.ClientID,
                comments = riskProfile.Comments,
                incomeSource = riskProfile.IncomeSource,
                retirementAge = riskProfile.RetirementAge.ToString(),
                riskAttitude = riskProfile.RiskAttitude,
                shortTermEquityPercent = riskProfile.ShortTermEquityPercent,
                shortTermAssetPercent = riskProfile.ShortTermAssetPercent,
                investmentKnowledge = riskProfile.InvestmentKnowledge,
                investmentObjective1 = riskProfile.InvestmentObjective1,
                investmentObjective2 = riskProfile.InvestmentObjective2,
                investmentObjective3 = riskProfile.InvestmentObjective3,
                investmentProfile = riskProfile.InvestmentProfile,
                investmentTimeHorizon = riskProfile.InvestmentTimeHorizon,
                longTermGoal1 = riskProfile.LongTermGoal1,
                longTermGoal2 = riskProfile.LongTermGoal2,
                longTermGoal3 = riskProfile.LongTermGoal3,
                medTermGoal1 = riskProfile.MedTermGoal1,
                medTermGoal2 = riskProfile.MedTermGoal2,
                medTermGoal3 = riskProfile.MedTermGoal3,
                profileId = riskProfile.RiskProfileID,
                retirementIncome = riskProfile.RetirementIncome,
                shortTermGoal1 = riskProfile.ShortTermGoal1,
                shortTermGoal2 = riskProfile.ShortTermGoal2,
                shortTermGoal3 = riskProfile.ShortTermGoal3,
                shortTermTrading = riskProfile.ShortTermTrading
            };
            ClientPersonCompleteProfileBinding model = new ClientPersonCompleteProfileBinding { 
            
                UserId = userId,
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                Phone = client.Phone,
                Mobile = client.Mobile,
                DOB = client.Dob,
                Fax = client.Fax,
                Gender = client.Gender,
                riskProfile = riskModel
            };

            
            
            if (!string.IsNullOrEmpty(client.Address))
            {
                string[] address = client.Address.Split(' ');
                model.PostCode = address[address.Length - 1];
                model.Country = address[address.Length - 2];
                model.State = address[address.Length - 3];
                model.Suburb = address[address.Length - 4];
                for(int i = 0 ; i < address.Length - 4; i ++){
                    model.line1 += address[i] + " ";
                }
            }

            

            return PartialView(model);
        }
        [Authorize(Roles = AuthorizationRoles.Role_Preclient + "," + AuthorizationRoles.Role_Client)]
        public PartialViewResult CompleteEntityProfile()
        {
            var userId = User.Identity.GetUserId();
            var client = edisRopo.GetClientSync(userId, DateTime.Now);
            var riskProfile = edisRopo.getRiskProfileForClient(client.Id);

            RiskProfileBindingModel riskModel = new RiskProfileBindingModel {
                capitalLossAttitude = riskProfile.CapitalLossAttitude,
                clientId = riskProfile.ClientID,
                comments = riskProfile.Comments,
                incomeSource = riskProfile.IncomeSource,
                retirementAge = riskProfile.RetirementAge.ToString(),
                riskAttitude = riskProfile.RiskAttitude,
                shortTermEquityPercent = riskProfile.ShortTermEquityPercent,
                shortTermAssetPercent = riskProfile.ShortTermAssetPercent,
                investmentKnowledge = riskProfile.InvestmentKnowledge,
                investmentObjective1 = riskProfile.InvestmentObjective1,
                investmentObjective2 = riskProfile.InvestmentObjective2,
                investmentObjective3 = riskProfile.InvestmentObjective3,
                investmentProfile = riskProfile.InvestmentProfile,
                investmentTimeHorizon = riskProfile.InvestmentTimeHorizon,
                longTermGoal1 = riskProfile.LongTermGoal1,
                longTermGoal2 = riskProfile.LongTermGoal2,
                longTermGoal3 = riskProfile.LongTermGoal3,
                medTermGoal1 = riskProfile.MedTermGoal1,
                medTermGoal2 = riskProfile.MedTermGoal2,
                medTermGoal3 = riskProfile.MedTermGoal3,
                profileId = riskProfile.RiskProfileID,
                retirementIncome = riskProfile.RetirementIncome,
                shortTermGoal1 = riskProfile.ShortTermGoal1,
                shortTermGoal2 = riskProfile.ShortTermGoal2,
                shortTermGoal3 = riskProfile.ShortTermGoal3,
                shortTermTrading = riskProfile.ShortTermTrading
            };

            ClientEntityCompleteProfileBinding model = new ClientEntityCompleteProfileBinding { 
                UserID = userId,
                EntityName = client.EntityName,
                EntityType = client.EntityType,
                Phone = client.Phone,
                ABN = client.ABN,
                ACN = client.ACN,
                Fax = client.Fax,
                riskProfile = riskModel
            };

            
            if (!string.IsNullOrEmpty(client.Address))
            {
                string[] address = client.Address.Split(' ');
                model.PostCode = address[address.Length - 1];
                model.Country = address[address.Length - 2];
                model.State = address[address.Length - 3];
                model.Suburb = address[address.Length - 4];
                for (int i = 0; i < address.Length - 4; i++)
                {
                    model.line1 += address[i] + " ";
                }
            }

            return PartialView(model);
        }



        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Role_Preclient + "," + AuthorizationRoles.Role_Client)]
        public ActionResult CompletePersonProfile(ClientPersonCompleteProfileBinding model, HttpPostedFileBase image = null)
        {
            var userId = User.Identity.GetUserId();

            if (userId != model.UserId)
            {
                ModelState.AddModelError("", "Invalid user id provided");
            }
            if (ModelState.IsValid)
            {
                ClientRegistration clientRegistration = new ClientRegistration() {
                    ClientNumber = model.UserId,
                    Address = model.line1 + " " + model.line2 + " " + model.line3 + " " + model.Suburb + " " + model.State + " " + model.Country + " " + model.PostCode,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    Dob = model.DOB,
                    Phone = model.Phone,
                    Mobile = model.Mobile,
                    Gender = model.Gender,
                    Fax = model.Fax,
                };

                edisRopo.UpdateClientSync(clientRegistration);


                #region create risk profile if present
                if (model.riskProfile != null) {
                    var riskProfile = model.riskProfile;
                    RiskProfile profile = new RiskProfile {
                        CapitalLossAttitude = riskProfile.capitalLossAttitude,
                        ClientID = edisRopo.GetClientSync(model.UserId, DateTime.Now).Id,
                        Comments = riskProfile.comments,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        IncomeSource = riskProfile.incomeSource,
                        InvestmentKnowledge = riskProfile.investmentKnowledge,
                        InvestmentObjective1 = riskProfile.investmentObjective1,
                        InvestmentObjective2 = riskProfile.investmentObjective2,
                        InvestmentObjective3 = riskProfile.investmentObjective3,
                        InvestmentProfile = riskProfile.investmentProfile,
                        InvestmentTimeHorizon = riskProfile.investmentTimeHorizon,
                        LongTermGoal1 = riskProfile.longTermGoal1,
                        LongTermGoal2 = riskProfile.longTermGoal2,
                        LongTermGoal3 = riskProfile.longTermGoal3,
                        MedTermGoal1 = riskProfile.medTermGoal1,
                        MedTermGoal2 = riskProfile.medTermGoal2,
                        MedTermGoal3 = riskProfile.medTermGoal3,
                        RetirementAge = string.IsNullOrEmpty(riskProfile.retirementAge) ? (int?)null : Convert.ToInt32(riskProfile.retirementAge),
                        RetirementIncome = riskProfile.retirementIncome,
                        RiskAttitude = riskProfile.riskAttitude,
                        ShortTermAssetPercent = riskProfile.shortTermAssetPercent,
                        ShortTermEquityPercent = riskProfile.shortTermEquityPercent,
                        ShortTermGoal1 = riskProfile.shortTermGoal1,
                        ShortTermGoal2 = riskProfile.shortTermGoal2,
                        ShortTermGoal3 = riskProfile.shortTermGoal3,
                        ShortTermTrading = riskProfile.shortTermTrading,
                        //riskLevel = (int)RiskLevels.NotSet
                    };

                    if (edisRopo.getRiskProfileForClient(edisRopo.GetClientSync(model.UserId, DateTime.Now).Id) != null) {
                        edisRopo.UpdateRiskProfile(profile);
                    } else {
                        edisRopo.CreateRiskProfileForClient(profile);
                    }
                }
                #endregion

                UserManager.RemoveFromRole(userId, AuthorizationRoles.Role_Preclient);
                UserManager.AddToRole(userId, AuthorizationRoles.Role_Client);

                //TempData["message"] = "Your profile has been successfully updated";
                //return JavaScript("document.location.replace('" + Url.Action("showMessage") + "');");
                return JavaScript("document.location.replace('" + Url.Action("Index", "Client") + "');");

                
            }
            else
            {
                return PartialView(model);
            }
        }
        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Role_Preclient + "," + AuthorizationRoles.Role_Client)]
        public ActionResult CompleteEntityProfile(ClientEntityCompleteProfileBinding model)
        {
            var userId = User.Identity.GetUserId();
            if (userId != model.UserID)
            {
                ModelState.AddModelError("", "Invalid user id provided");
            }
            if (ModelState.IsValid)
            {
                ClientRegistration clientRegistration = new ClientRegistration()
                {
                    ClientNumber = model.UserID,
                    Address = model.line1 + " " + model.line2 + " " + model.line3 + " " + model.Suburb + " " + model.State + " " + model.Country + " " + model.PostCode,
                    EntityName = model.EntityName,
                    EntityType = model.EntityType,
                    ABN = model.ABN,
                    ACN = model.ACN,
                    Phone = model.Phone,
                    Fax = model.Fax
                };

                edisRopo.UpdateClientSync(clientRegistration);


                #region create risk profile if present
                if (model.riskProfile != null) {
                    var riskProfile = model.riskProfile;
                    RiskProfile profile = new RiskProfile {
                        CapitalLossAttitude = riskProfile.capitalLossAttitude,
                        ClientID = edisRopo.GetClientSync(model.UserID, DateTime.Now).Id,
                        Comments = riskProfile.comments,
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        IncomeSource = riskProfile.incomeSource,
                        InvestmentKnowledge = riskProfile.investmentKnowledge,
                        InvestmentObjective1 = riskProfile.investmentObjective1,
                        InvestmentObjective2 = riskProfile.investmentObjective2,
                        InvestmentObjective3 = riskProfile.investmentObjective3,
                        InvestmentProfile = riskProfile.investmentProfile,
                        InvestmentTimeHorizon = riskProfile.investmentTimeHorizon,
                        LongTermGoal1 = riskProfile.longTermGoal1,
                        LongTermGoal2 = riskProfile.longTermGoal2,
                        LongTermGoal3 = riskProfile.longTermGoal3,
                        MedTermGoal1 = riskProfile.medTermGoal1,
                        MedTermGoal2 = riskProfile.medTermGoal2,
                        MedTermGoal3 = riskProfile.medTermGoal3,
                        RetirementAge = string.IsNullOrEmpty(riskProfile.retirementAge) ? (int?)null : Convert.ToInt32(riskProfile.retirementAge),
                        RetirementIncome = riskProfile.retirementIncome,
                        RiskAttitude = riskProfile.riskAttitude,
                        ShortTermAssetPercent = riskProfile.shortTermAssetPercent,
                        ShortTermEquityPercent = riskProfile.shortTermEquityPercent,
                        ShortTermGoal1 = riskProfile.shortTermGoal1,
                        ShortTermGoal2 = riskProfile.shortTermGoal2,
                        ShortTermGoal3 = riskProfile.shortTermGoal3,
                        ShortTermTrading = riskProfile.shortTermTrading,
                        //riskLevel = (int)RiskLevels.NotSet
                    };
                    if (edisRopo.getRiskProfileForClient(edisRopo.GetClientSync(model.UserID, DateTime.Now).Id) != null) {
                        edisRopo.UpdateRiskProfile(profile);
                    } else {
                        edisRopo.CreateRiskProfileForClient(profile);
                    }
                }
                #endregion
                
                UserManager.RemoveFromRole(userId, AuthorizationRoles.Role_Preclient);
                UserManager.AddToRole(userId, AuthorizationRoles.Role_Client);

                //redirect to client dashboard here
                //return RedirectToAction("Index", "Client");
                //TempData["message"] = "Your profile has been successfully updated";
                //return JavaScript("document.location.replace('" + Url.Action("showMessage") + "');");
                return JavaScript("document.location.replace('" + Url.Action("Index", "Client") + "');");
            }
            else
            {
                return PartialView(model);
            }
        }
        public ActionResult showMessage()
        {
            return View("Message");
        }
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public ViewResult UpdateRiskProfile()
        {
            var userId = User.Identity.GetUserId();
            //return View(clientRepo.GetRiskProfileBindingModel(userId));

            return null;
        }
        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public ActionResult UpdateRiskProfile(RiskProfileBindingModel model)
        {
            if (model == null)
            {
                ModelState.AddModelError("", "Model is not entered");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                //clientRepo.AddOrUpdateRiskProfile(model);
                //clientRepo.Save();
                TempData["success"] = "Profile has been successfully updated";
            }
            return View(model);
        }
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public ActionResult UpdateImage()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public ActionResult UpdateImage(HttpPostedFileBase image = null)
        {
            if (image != null)
            {

                //clientRepo.AddOrUpdateClientProfileImage(User.Identity.GetUserId(), image);
                //clientRepo.Save();
            }
            else
            {
                TempData["error"] = "No image is selected";
            }
            return View();

        }
        [Authorize]
        public FileContentResult GetImage(string clientId = "")
        {
            if (!string.IsNullOrEmpty(clientId))
            {

                //var client = clientRepo.GetAllClients().FirstOrDefault(c => c.ClientUserID == clientId);
                //if (client != null)
                //{
                //    return File(client.Image, client.ImageMimeType);
                //}
                //else
                //{
                //    return null;
                //}

                return null;
            }
            else
            {

                var userid = User.Identity.GetUserId();
                //var client = clientRepo.GetAllClients().FirstOrDefault(c => c.ClientUserID == userid);
                //if (client != null)
                //{
                //    return File(client.Image, client.ImageMimeType);
                //}
                //else
                //{
                //    return null;
                //}

                return null;
            }

        }

    }
}