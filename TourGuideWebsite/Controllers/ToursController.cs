using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideProtocol.DataInt;
using TourGuideProtocol.DataStruct;
using TourGuideBLL;
using System.Globalization;

namespace TourGuideWebsite.Controllers
{
    public class ToursController : Controller
    {
        //
        // GET: /Tours/

        [HttpGet]
        public ActionResult Index(string id)
        {
            if (id != null && id != "" && id != "undefined") // The user typed a keyword
            {
                ViewBag.keyword = id;
                BTourGuideOp tourOp = new BTourGuideOp();
                List<ATour> tours = tourOp.GetTours(id);
                if (tours.Count > 0)
                {
                    return View(tours);
                }
                else
                {
                    TempData["SearchMessage"] = "No tours that match your keyword were found";
                    TempData["SearchTextbox"] = id;
                    return RedirectToAction("Index");
                }
            }
            else // The search textbox is empty / the user clicked on "Tours"
            {
                BTourGuideOp tourOp = new BTourGuideOp();
                List<ATour> tours = tourOp.GetTours();
                return View(tours);
                //return RedirectToAction("AdvancedSearch");
            }
        }

        [HttpGet]
        public ActionResult TourDescription(string id, string keyword)
        {
            ViewBag.keyword = keyword;
            BTourGuideOp tourOp = new BTourGuideOp();
            ATour tour = tourOp.GetTourByID(id);
            return View(tour);
        }

        [HttpGet]
        public ActionResult TourDates(string id, string keyword = "")
        {
            ViewBag.keyword = keyword;
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> tourEvents = tourOp.GetEventsByTourId(id);
            return View(tourEvents);
        }

        [HttpGet]
        public ActionResult Location()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> tourEvents = tourOp.GetEvents();
            List<ATour> tours = tourOp.GetTours();
            List<string> locations = new List<string>();
            foreach(ATour tour in tours)
            {
                if (!locations.Contains(tour.TourLocation))
                {
                    locations.Add(tour.TourLocation);
                }
            }
            ViewBag.locations = locations;
            return View(tourEvents);
        }

        [HttpGet]
        public ActionResult Area()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> tourEvents = tourOp.GetEvents();
            List<ATour> tours = tourOp.GetTours();
            List<string> areas = new List<string>();
            foreach (ATour tour in tours)
            {
                if (!areas.Contains(tour.TourArea))
                {
                    areas.Add(tour.TourArea);
                }
            }
            ViewBag.areas = areas;
            return View(tourEvents);
        }

        [HttpGet]
        public ActionResult Guide()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> tourEvents = tourOp.GetEvents();
            List<string> guides = new List<string>();
            foreach (AEvent tourEvent in tourEvents)
            {
                if (!guides.Contains(tourEvent.TourGuide))
                {
                    guides.Add(tourEvent.TourGuide);
                }
            }
            ViewBag.guides = guides;
            return View(tourEvents);
        }

        [HttpGet]
        public ActionResult Category()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> tourEvents = tourOp.GetEvents();
            List<ATour> tours = tourOp.GetTours();
            List<string> categories = new List<string>();
            foreach (ATour tour in tours)
            {
                if (!categories.Contains(tour.TourCategory))
                {
                    categories.Add(tour.TourCategory);
                }
            }
            ViewBag.categories = categories;
            return View(tourEvents);
        }

        [HttpGet]
        public ActionResult Date()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AEvent> tourEvents = tourOp.GetEvents();
            List<string> dates = new List<string>();
            foreach (AEvent tourEvent in tourEvents)
            {
                if (!dates.Contains(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(tourEvent.TourDate.Month)))
                {
                    dates.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(tourEvent.TourDate.Month));
                }
            }
            ViewBag.dates = dates;
            return View(tourEvents);
        }


    }
}
