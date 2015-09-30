using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideBLL;
using TourGuideProtocol.DataStruct;

namespace TourGuideWebsite.Controllers
{
    public class TourController : Controller
    {
        //
        // GET: /Tour/

        public ActionResult Index()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<ATour> tours = tourOp.GetTours();
            return View(tours);
        }

        //
        // GET: /Tour/Details/5

        public ActionResult Details(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<ATour> tours = tourOp.GetTours();
            ATour tour = tours.Single<ATour>(x => x.TourID == id);
            return View(tour);
        }

        //
        // GET: /Tour/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tour/Create

        [HttpPost]
        public ActionResult Create(ATour tour)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    tourOp.AddTour(tour);
                    return RedirectToAction("Index");
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
        // GET: /Tour/Edit/5

        public ActionResult Edit(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<ATour> tours = tourOp.GetTours();
            ATour tour = tours.Single<ATour>(x => x.TourID == id);
            return View(tour);
        }

        //
        // POST: /Tour/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, ATour tour)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    tour.TourID = id;
                    tourOp.EditTour(tour);
                    return RedirectToAction("Index");
                }
                else
                    return View(tour);
            }
            catch
            {
                return View(tour);
            }
        }

        //
        // GET: /Tour/Delete/5

        public ActionResult Delete(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<ATour> tours = tourOp.GetTours();
            ATour tour = tours.Single<ATour>(x => x.TourID == id);
            return View(tour);
        }

        //
        // POST: /Tour/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, ATour tour)
        {
            try
            {
                BTourGuideOp tourOp = new BTourGuideOp();
                tourOp.DeleteTour(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
