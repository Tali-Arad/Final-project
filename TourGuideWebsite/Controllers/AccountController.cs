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
                        //var cookie = new HttpCookie("userame", model.UserName.ToString());
                        //Response.Cookies.Add(cookie);
                        //System.Web.HttpContext.Current.Session["username"] = model.UserName;
                        return Redirect(returnUrl ?? Url.Action("Index", "Home"));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect Username Or Password");
                        ViewBag.ReturnUrl = returnUrl;
                        return View(); 
                    }
             }      
                return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UserProfile(string username)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            AUser user = tourOp.GetUser(username);
            List<AReg> userRegs =  tourOp.GetRegistrationsByUserID(user.UserID);
            UserProfile userProfile = new UserProfile();
            UserChanges userChanges = new UserChanges();
            userProfile.UserRegs = userRegs;
            userChanges.UserEmail = user.UserEmail;
            userChanges.UserPhone = user.UserPhone;
            userProfile.UserChanges = userChanges;

            ViewBag.Username = username;

            return View(userProfile);
        }

        [HttpPost]
        public ActionResult UserProfile(UserProfile userProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    string username = User.Identity.Name;
                    AUser user = tourOp.GetUser(username);
                    user.UserPhone = userProfile.UserChanges.UserPhone;
                    user.UserEmail = userProfile.UserChanges.UserEmail;
                    tourOp.EditUser(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                    return View(userProfile);
            }
            catch
            {
                return View(userProfile);
            }
        }



        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

       


    }
}

