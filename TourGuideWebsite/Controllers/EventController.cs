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
     [Authorize(Users = "admin")]
    public class EventController : Controller
    {
        //
        // GET: /Event/

        public ActionResult Index()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> events = tourOp.GetEvents();
            return View(events);
        }

        //
        // GET: /Event/Details/5

        public ActionResult Details(string tourid, DateTime tourdate)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> events = tourOp.GetEvents();
            AEvent tourEvent = events.Single<AEvent>(x => x.TourID == tourid && x.TourDate == tourdate);
            return View(tourEvent);
        }

        //
        // GET: /Event/Create

        public ActionResult Create()
        { 
            ViewBag.TourNameOptions = Lists.CreateTourList();
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(AEvent tourEvent, string TourNameOptions)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    List<ATour> tours = tourOp.GetTours();
                    ATour tour = tours.Single(x => x.TourName == TourNameOptions);
                    tourEvent.TourID = tour.TourID;
                    tourEvent.TourName = TourNameOptions;
                    tourOp.AddEvent(tourEvent);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.TourNameOptions = Lists.CreateTourList();
                    return View(tourEvent);
                }
            }
            catch(Exception e)
            {
                TempData["CreateException"] = "Error in event creation: " + e.Message;
                return View();
            }
        }
    

        //
        // GET: /Event/Edit/5
        public ActionResult Edit(string tourid, DateTime tourdate)
        {
            // Passing the current values to the view:
            BTourGuideOp tourOp = new BTourGuideOp();
            AEvent tourEvent = tourOp.GetEvent(tourid, tourdate);
            if(tourEvent == null)
                 return HttpNotFound();
            return View(tourEvent);
        }
        

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(string tourid, AEvent tourEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    tourEvent.TourID = tourid;
                    tourEvent.TourOriginalDate = tourEvent.TourOriginalDate;
                    tourOp.EditEvent(tourEvent);
                    return RedirectToAction("Index", new { edited="Yes" });
                }
                else
                    return View(tourEvent);
            }
            catch(Exception e)
            {
                TempData["EditException"] = "Error in event edit: " + e.Message;
                return View(tourEvent);
            }
        }

        //
        // GET: /Event/Delete/5

        public ActionResult Delete(string tourid, DateTime tourdate)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> events = tourOp.GetEvents();
            AEvent tourEvent = events.SingleOrDefault<AEvent>(x => x.TourID == tourid && x.TourDate == tourdate);
            if (tourEvent == null)
            {
                return HttpNotFound();
            }
            return View(tourEvent);
        }

        //
        // POST: /Event/Delete/5

        [HttpPost]
        public ActionResult Delete(string tourid, AEvent tourEvent)
        {
            try
            {
                BTourGuideOp tourOp = new BTourGuideOp();
                tourOp.DeleteEvent(tourid, tourEvent.TourDate);
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                TempData["DeleteException"] = "Error in event deletion: " + e.Message;
                return View();
            }
        }
    }
}
