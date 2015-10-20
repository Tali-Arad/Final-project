using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideWebsite.Models;
using System.Web.Security;
using TourGuideBLL;
using TourGuideProtocol.DataInt;
using TourGuideProtocol.DataStruct;


namespace TourGuideWebsite.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ViewResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl) 
        {
            if (ModelState.IsValid)
            {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    List<AUser> users = tourOp.GetUsers();
                    bool userValid = users.Any(user => user.Username == model.UserName && user.UserPassword == model.Password);
                    if (userValid)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect Username Or Password");
                        return View();
                    }
             }      
            else
            {
                return View();
            }
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }



        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

       


    }
}

