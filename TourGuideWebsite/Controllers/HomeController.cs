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
                return View(rr);
 
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
                // Add password confirmation
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


        //[HttpGet]
        //public ActionResult Tours(string id)
        //{
        //    if(id!=null && id!="" && id!="undefined") // The user typed a keyword
        //    { 
        //        ViewBag.keyword = id;
        //        BTourGuideOp tourOp = new BTourGuideOp();
        //        List<ATour> tours = tourOp.GetTours(id);
        //        if (tours.Count > 0)
        //        {
        //            return View(tours);
        //        }
        //        else
        //        {
        //            TempData["SearchMessage"] = "No tours that match your keyword were found";
        //            TempData["SearchTextbox"] = id;
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else // The search textbox is empty / the user clicked on "Tours"
        //    {
        //        BTourGuideOp tourOp = new BTourGuideOp();
        //        List<ATour> tours = tourOp.GetTours();
        //        return View(tours);
        //        //return RedirectToAction("AdvancedSearch");
        //    }
        //}

        //[HttpGet]
        //public ActionResult TourDescription(string id, string keyword)
        //{
        //    ViewBag.keyword = keyword;
        //    BTourGuideOp tourOp = new BTourGuideOp();
        //    ATour tour = tourOp.GetTourByID(id);
        //    return View(tour);
        //}

        //[HttpGet]
        //public ActionResult TourDates(string id, string keyword="")
        //{
        //    ViewBag.keyword = keyword;
        //    BTourGuideOp tourOp = new BTourGuideOp();
        //    List<AEvent> tourEvents = tourOp.GetEventsByTourId(id);
        //    return View(tourEvents);
        //}

        [HttpGet]
        public ActionResult AdvancedSearch()
        {
            return View();
        }




    }
    }

