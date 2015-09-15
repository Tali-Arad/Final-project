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

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Tour/Edit/5

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
        // GET: /Tour/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Tour/Delete/5

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
