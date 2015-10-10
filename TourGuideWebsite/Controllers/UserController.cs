using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TourGuideBLL;
using TourGuideProtocol.DataStruct;

namespace TourGuideWebsite.Controllers
{
     [Authorize(Users = "admin")]
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AUser> users = tourOp.GetUsers();
            return View(users);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AUser> users = tourOp.GetUsers();
            AUser user = users.Single<AUser>(x => x.UserID == id);
            return View(user);
        }

        //
        // GET: /User/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /User/Create

        [HttpPost]
        public ActionResult Create(AUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    user.RegTime = DateTime.Now;
                    user.UserIP = Request.ServerVariables["REMOTE_ADDR"];
                    tourOp.AddUser(user);
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
        // GET: /User/Edit/5

        public ActionResult Edit(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AUser> users = tourOp.GetUsers();
            AUser user = users.Single<AUser>(x => x.UserID == id);
            return View(user);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, AUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    user.UserID = id;
                    tourOp.EditUser(user);
                    return RedirectToAction("Index");
                }
                else
                    return View(user);
            }
            catch
            {
                return View(user);
            }
        }

        //
        // GET: /User/Delete/5

        public ActionResult Delete(string id)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AUser> users = tourOp.GetUsers();
            AUser user = users.Single<AUser>(x => x.UserID == id);
            return View(user);
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(string id, AUser user)
        {
            try
            {
                BTourGuideOp tourOp = new BTourGuideOp();
                tourOp.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
