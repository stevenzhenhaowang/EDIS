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

            ClientPersonCompleteProfileBinding model = new ClientPersonCompleteProfileBinding { 
            
                UserId = userId,
                FirstName = client.FirstName,
                MiddleName = client.MiddleName,
                LastName = client.LastName,
                Phone = client.Phone,
                Mobile = client.Mobile,
                DOB = client.Dob,
                Fax = client.Fax,
                Gender = client.Gender
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
            ClientEntityCompleteProfileBinding model = new ClientEntityCompleteProfileBinding { 
            
                UserID = userId,
                EntityName = client.EntityName,
                EntityType = client.EntityType,
                Phone = client.Phone,
                ABN = client.ABN,
                ACN = client.ACN,
                Fax = client.Fax,
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