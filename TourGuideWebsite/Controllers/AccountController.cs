using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideWebsite.Models;
using System.Web.Security;
using WebMatrix.WebData;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
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
                    AUser user = tourOp.GetUser(model.UserName);
                    if (user != null)
                    {
                        // hasing & salting
                        PasswordManager passMan = new PasswordManager();
                        bool result = passMan.IsPasswordMatch(model.Password, user.Salt, user.UserPassword);
                       // bool result = users.Any(u => u.Username == model.UserName && user.UserPassword == model.Password);   
                        if (result)
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
                            ViewBag.IncorrectInput = "Incorrect";
                            ViewBag.ReturnUrl = returnUrl;
                            return View();
                        }
                    }
                    else
                        return View();
                   // bool userValid = users.Any(user => user.Username == model.UserName && user.UserPassword == model.Password);      
             }      
                return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult UserProfile(string username, string msg)
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
            ViewBag.Msg = msg; // Password change msg
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

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword(string username)
        {
            ViewBag.Username = username;
            TempData["Username"] = username;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword model, string username)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    username = TempData["Username"].ToString();
                    AUser user = tourOp.GetUser(username);
                    if (user.UserPassword == model.OldPassword)
                    {
                        user.UserPassword = model.NewPassword;
                        tourOp.EditUser(user);
                        return RedirectToAction("UserProfile", new { Username = username, msg = "Your password has changed" });
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                 return View();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {      
            return View(); 
        }
   
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Get the user by email:
                    BTourGuideOp tourOp = new BTourGuideOp();
                    List<AUser> users = tourOp.GetUsers();
                    AUser user = users.FirstOrDefault(u => u.UserEmail == model.Email);
                    // If a user with the email provided was found:
                    if (user != null)
                    {
                        // Send Email:

                        // Generae password token that will be used in the email link to authenticate user
                         string resetToken = Guid.NewGuid().ToString();
                   
                        // Generate the html link sent via email
                        user.ResetToken = resetToken;
                        tourOp.EditUser(user);
                        string resetLink = "<a href='"
                           + Url.Action("ResetPassword", "Account", new { rt = resetToken }, "http")
                           + "'>Reset Password Link</a>";

                        // Email stuff
                        string subject = "Reset your password for TourGuideWebsite";
                        string body = "Your link: " + resetLink;
                        string from = "tali85arad@gmail.com";

                        MailMessage message = new MailMessage(from, model.Email);
                        message.Subject = subject;
                        message.Body = body; 
                        message.IsBodyHtml = true;

                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                        {
                            UseDefaultCredentials = false,
                            EnableSsl = true,
                            Timeout = 20000,
                            Credentials = new NetworkCredential("tali85arad@gmail.com", "henhqwcfvmtzplgb")
                            
                        };

                        // Attempt to send the email
                        try
                        {
                            client.Send(message);
                            ViewBag.Message = "A reset password email has been sent.";
                            return View();
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("", "Issue sending email: " + e.Message);
                        }
                    }

                    // For testing:
                    //else // Email not found
                    //{
                    //    /* Note: You may not want to provide the following information
                    //    * since it gives an intruder information as to whether a
                    //    * certain email address is registered with this website or not.
                    //    * If you're really concerned about privacy, you may want to
                    //    * forward to the same "Success" page regardless whether an
                    //    * user was found or not. This is only for illustration purposes.
                    //    */
                    //    ModelState.AddModelError("", "No user found by that email.");
                    //}
                }
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Issue sending email: " + e.Message);
                return View(model);
            }
        }

        public ActionResult ResetPassword(string rt)
        {
            ResetPassword model = new ResetPassword();
            model.ReturnToken = rt;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                BTourGuideOp tourOp = new BTourGuideOp();
                List<AUser> users = tourOp.GetUsers();
                AUser user = users.FirstOrDefault(u => u.ResetToken == model.ReturnToken);
                if(user!=null)
                {
                    user.UserPassword = model.Password;
                    user.ResetToken = null;
                    tourOp.EditUser(user);
                    ViewBag.Message = "Successfully Changed";
                }
                else
                {
                    ViewBag.Message = "Something went wrong!";
                }
            }
            return View(model);
        }
        

        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

       


    }
}

