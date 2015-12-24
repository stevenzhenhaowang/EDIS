using EDISAngular.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDISAngular.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "EDIS";

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult IndexLogin(LoginViewModel model, string returnUrl)
        {

            string usr = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Email == "peter.truong@ediservices.com.au" && model.Password == "edis098EDIS")
            {
                return View("~/Views/Home/IndexList.cshtml");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(model);
            }
        }


        [AllowAnonymous]
        public ActionResult IndexLogin()
        {
            return View("~/Views/Home/IndexLogin.cshtml");
        }

    }
}
