using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideBLL;
using TourGuideProtocol.DataStruct;

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
        {
            return View();
        }

        //
        // POST: /Reg/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Reg/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Reg/Edit/5

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
        // GET: /Reg/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Reg/Delete/5

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
