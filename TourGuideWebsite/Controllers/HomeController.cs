using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideWebsite.Models;
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
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> events = tourOp.GetEvents();
            return View(events);
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

        [Authorize]// only users can register on tours
        [HttpGet] 
        public ActionResult RegForm(string id, DateTime date)
        {
                BTourGuideOp tourOp = new BTourGuideOp();
                AEvent tourEvent = tourOp.GetEvent(id, date);
                string username = HttpContext.User.Identity.Name;
                AUser user = tourOp.GetUserByUsername(username);
                RegResponse rr = new RegResponse();
                rr.EventInfo = tourEvent;
                rr.UserInfo = user;
                return View(rr);
        }
        // POST: /RegForm/
        [HttpPost]
        public ActionResult RegForm(RegResponse rr, string id)
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
                reg.WillAttend = (rr.WillAttend) ? 1 : 0;

                BTourGuideOp tourOp = new BTourGuideOp();
                tourOp.AddReg(reg);
                return View("ThankYou");
                // Add email sending in a desktop app
            }
            else
                return View();
           
        }

        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.returnUrl = Request.UrlReferrer;
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserDetails userdetails, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                // Add validation and password hashing
                AUser user = new AUser();
                user.RegTime = DateTime.Now;
                user.UserIP = Request.ServerVariables["REMOTE_ADDR"];
                user.UserFirstName = userdetails.UserFirstName;
                user.UserLastName = userdetails.UserLastName;
                user.UserEmail = userdetails.UserEmail;
                user.UserPhone = userdetails.UserPhone;
                user.UserPassword = userdetails.UserPassword;
                user.Username = userdetails.Username;
                user.UserBirthday = userdetails.UserBirthday;

                BTourGuideOp tourOp = new BTourGuideOp();
                tourOp.AddUser(user);
             return RedirectToAction("Index"); // Do: redirect to previous page!
            }
            else
                return View();
        }

    }
    }

