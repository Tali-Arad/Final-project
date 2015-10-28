using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security;
using TourGuideProtocol.DataInt;
using TourGuideProtocol.DataStruct;
using TourGuideBLL;

namespace TourGuideWebsite.Controllers
{
   [Authorize(Users = "admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<ATour> tours = tourOp.GetTours();
            return View(tours);
        }

    }
    
}
