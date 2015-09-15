using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideBLL;
using TourGuideProtocol.DataStruct;


namespace TourGuideWebsite.Controllers
{
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
            return View();
        }

        //
        // POST: /Event/Create

        [HttpPost]
        public ActionResult Create(AEvent tourEvent)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    List<ATour> tours = tourOp.GetTours();
                    if (tours.Any(tour => tour.TourID == tourEvent.TourID))
                    {
                        tourOp.AddEvent(tourEvent);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(); // add message for user: no tour with this id. Redirect to 
                        // another action which asks the admin if he wants to create a new tour, and 
                        // then with a button to TourController/Create
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

        //
        // GET: /Event/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Event/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Event/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Event/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
