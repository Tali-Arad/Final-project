using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using TourGuideWebsite.Models;
using TourGuideWebsite.Hashing;
using TourGuideBLL;
using TourGuideProtocol.DataInt;
using TourGuideProtocol.DataStruct;

namespace TourGuideWebsite.Controllers
{
    public class HomeController : Controller
    {
        
        //
        // GET: /Home/
        public ActionResult Index()
        {
            if (TempData["SearchMessage"]==null)
            {
                TempData["SearchMessage"] = "";
            }
            if (TempData["SearchTextbox"] == null)
            {
                TempData["SearchTextbox"] = "";
            }
            ViewBag.SearchMessage = TempData["SearchMessage"].ToString();
            ViewBag.SearchTextbox = TempData["SearchTextbox"].ToString();
            return View();
        }

        // GET: /EventDetails/
        [HttpGet]
        public ActionResult EventDetails(string id, DateTime date)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            AEvent tourEvent = tourOp.GetEvent(id, date);
            ATour tour = tourOp.GetTourByID(id);
            EventDetails ed = new EventDetails();
            ed.eventInfo = tourEvent;
            ed.tourInfo = tour;
            return View(ed);
        }

        // GET: /RegForm/

        [Authorize]// only logged in users can register on tours
        [HttpGet] 
        public ActionResult RegForm(string id, DateTime date)
        {
                BTourGuideOp tourOp = new BTourGuideOp();
                AEvent tourEvent = tourOp.GetEvent(id, date);
                string username = HttpContext.User.Identity.Name;
                AUser user = tourOp.GetUser(username);
                RegResponse rr = new RegResponse();
                rr.EventInfo = tourEvent;
                rr.UserInfo = user;
                return View(rr);
        }
        // POST: /RegForm/
        [HttpPost]
        public ActionResult RegForm(RegResponse rr, string id)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    AReg reg = new AReg();
                    reg.TourID = id;
                    reg.TourDate = rr.EventInfo.TourDate;
                    reg.RegFirstName = rr.FirstName;
                    reg.RegLastName = rr.LastName;
                    reg.RegBirthday = rr.Birthday;
                    reg.UserID = rr.UserInfo.UserID;
                    reg.RegTime = DateTime.Now;
                    reg.WillAttend = rr.WillAttend;
                    BTourGuideOp tourOp = new BTourGuideOp();
                    tourOp.AddReg(reg);

                    // Send email to user:

                    // Email stuff
                    string subject = "Your registartion to the tour " + rr.EventInfo.TourName + " on " + rr.EventInfo.TourDate.ToString("dd-MM-yyyy");
                    string body = "Thank you for your registartion to the tour " + rr.EventInfo.TourName + " on " + rr.EventInfo.TourDate.ToString("dd-MM-yyyy") + "<br />" +
                                   "Registration name: " + rr.FirstName + " " + rr.LastName + "<br />" +
                                   "<a href='" + Url.Action("EventDetails", "Home", new { id = rr.EventInfo.TourID, date = rr.EventInfo.TourDate }, "http")
                                   + "'>Click here</a> to see the tour details." + "<br />" +
                                   "To see your user profile <a href='" + Url.Action("UserProfile", "Account", new { username = rr.UserInfo.Username }, "http")
                                   + "'>Click here</a>";

                    string from = "tali85arad@gmail.com";

                    MailMessage message = new MailMessage(from, rr.UserInfo.UserEmail);
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

                        // Updating the IsSentEmail in the DB

                        tourOp.UpdateEmailSent(rr.UserInfo.UserID, rr.FirstName, rr.LastName, rr.EventInfo.TourID, rr.EventInfo.TourDate, true);
                        return View("ThankYou", rr);
                    }
                    catch (Exception e)
                    {
                        TempData["EmailException"] = "Issue sending email: " + e.Message;
                        return View(rr);
                    }
                }
                else
                    return View(rr);
            } 
            catch(Exception e)
            {
            
                TempData["Exception"] = "" + e.Message;
                return View(rr);
            }
 
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserDetails userdetails, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Checking the username availability in the server
                    BTourGuideOp op = new BTourGuideOp();
                    List<AUser> users = op.GetUsers();
                    if (!users.Any(u => u.Username == userdetails.Username))
                    {
                        // password salting & hashing
                        PasswordManager passMan = new PasswordManager();
                        string salt = null;
                        string passwordHash = passMan.GeneratePasswordHash(userdetails.UserPassword, out salt);
                        AUser user = new AUser();
                        user.RegTime = DateTime.Now;
                        user.UserIP = Request.ServerVariables["REMOTE_ADDR"];
                        user.UserFirstName = userdetails.UserFirstName;
                        user.UserLastName = userdetails.UserLastName;
                        user.UserEmail = userdetails.UserEmail;
                        user.UserPhone = userdetails.UserPhone;
                        user.UserPassword = passwordHash;
                        user.Salt = salt;
                        user.Username = userdetails.Username;
                        user.UserBirthday = userdetails.UserBirthday;
                        BTourGuideOp tourOp = new BTourGuideOp();
                        tourOp.AddUser(user);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        userdetails.Username = null;
                        return View();
                    }
                }
                else
                {
                    userdetails.Username = null;
                    return View();
                }
            }
            catch(Exception e)
            {
                TempData["Exception"] = "" + e.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

    }
    }

