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
        public ActionResult Create(AReg reg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    tourOp.AddReg(reg);
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
