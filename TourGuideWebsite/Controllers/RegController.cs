using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideBLL;
using TourGuideProtocol.DataStruct;
using TourGuideWebsite.Models;

namespace TourGuideWebsite.Controllers
{
    public class RegController : Controller
    {
        //
        // GET: /Reg/

        public ActionResult Index()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AReg> registrations = tourOp.GetRegistrations();
            return View(registrations);
        }

        //
        // GET: /Reg/Details/5

        public ActionResult Details(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AReg> registrations = tourOp.GetRegistrations();
            AReg reg = registrations.Single<AReg>(x => x.RegID == id);
            return View(reg);
        }

        //
        // GET: /Reg/Create

        public ActionResult Create()
        {   List<SelectListItem> tourList= Lists.CreateTourList();
            ViewBag.TourNameOptions = tourList;
            // The initial TourDateOptions ddl fits the first tour in the TourNameOptions ddl:
            string tourName = tourList[0].Text;
            ViewBag.TourDateOptions = Lists.CreateTourDateList(tourName);
            //The tourDates dropdown list options change based on the tourName choice. This is done with AJAX via query using
            // a web service. 
            ViewBag.UsernameOptions = Lists.CreateUserList();
            
            return View();  
        }

        //
        // POST: /Reg/Create

        [HttpPost]
        public ActionResult Create(AReg reg, string TourNameOptions, string UsernameOptions, string TourDateOptions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    List<ATour> tours = tourOp.GetTours();
                    ATour tour = tours.Single(x => x.TourName == TourNameOptions);
                    reg.TourID = tour.TourID;
                    List<AUser> users = tourOp.GetUsers();
                    AUser user = users.Single(x => x.Username == UsernameOptions);
                    reg.UserID = user.UserID;
                    List<AEvent> events = tourOp.GetEvents();
                    AEvent tourEvent = events.Single(x => x.TourName == TourNameOptions && x.TourDate.ToString() == TourDateOptions.ToString());
                    reg.TourDate = tourEvent.TourDate;
                    tourOp.AddReg(reg);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(reg);
                }
            }
            catch
            {
                return View(reg);
            }
        }

        //
        // GET: /Reg/Edit/5

        public ActionResult Edit(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AReg> registrations = tourOp.GetRegistrations();
            AReg reg = registrations.Single<AReg>(x => x.RegID == id);
            return View(reg);
        }

        //
        // POST: /Reg/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, AReg reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    reg.RegID = id;
                    tourOp.EditReg(reg);
                    return RedirectToAction("Index");
                }
                else
                    return View(reg);
            }
            catch
            {
                return View(reg);
            }
        }

        //
        // GET: /Reg/Delete/5

        public ActionResult Delete(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AReg> regs = tourOp.GetRegistrations();
            AReg reg = regs.Single<AReg>(x => x.RegID == id);
            return View(reg);
        }

        //
        // POST: /Reg/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, AReg reg)
        {
            try
            {
                BTourGuideOp tourOp = new BTourGuideOp();
                tourOp.DeleteReg(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
