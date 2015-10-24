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
        public ActionResult Create(UserDetails userdetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     // Checking the username availability in the server
                      BTourGuideOp op = new BTourGuideOp();
                      List<AUser> users = op.GetUsers();
                      if (!users.Any(u => u.Username == userdetails.Username))
                      {
                          BTourGuideOp tourOp = new BTourGuideOp();
                          AUser user = new AUser();
                          user.RegTime = DateTime.Now;
                          user.UserIP = Request.ServerVariables["REMOTE_ADDR"];
                          user.UserFirstName = userdetails.UserFirstName;
                          user.UserLastName = userdetails.UserLastName;
                          user.UserEmail = userdetails.UserEmail;
                          user.UserPhone = userdetails.UserPhone;
                          user.UserPassword = System.Web.Security.Membership.GeneratePassword(8, 2);
                          user.Username = userdetails.Username;
                          user.UserBirthday = userdetails.UserBirthday;
                          tourOp.AddUser(user);
                          return RedirectToAction("Index");
                      }
                      else
                      {
                          userdetails.Username = null;
                          return View();
                      }
                }
                else
                {
                    return View(userdetails);
                }
            }
            catch
            {
                return View(userdetails);
            }
        }

        //
        // GET: /User/Edit/5

        public ActionResult Edit(string id, DateTime UserBirthday)
        {
            BTourGuideOp tourOp = new BTourGuideOp();
            List<AUser> users = tourOp.GetUsers();
            AUser user = users.Single<AUser>(x => x.UserID == id);
            UserDetails userDetails = new UserDetails();
            userDetails.UserBirthday = user.UserBirthday;
            userDetails.UserEmail = user.UserEmail;
            userDetails.UserPhone = user.UserPhone;
            userDetails.UserFirstName = user.UserFirstName;
            userDetails.UserLastName = user.UserLastName;
            userDetails.Username = user.Username;

            // Not the true password
            userDetails.UserPassword = "12345678";
            userDetails.ConfirmPass = "12345678";
            return View(userDetails);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, UserDetails userDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BTourGuideOp tourOp = new BTourGuideOp();
                    AUser user = tourOp.GetUser(userDetails.Username);
                    user.UserFirstName = userDetails.UserFirstName;
                    user.UserLastName = userDetails.UserLastName;
                    user.UserPhone = userDetails.UserPhone;
                    user.UserEmail = userDetails.UserEmail;
                    user.UserBirthday = userDetails.UserBirthday;
                    tourOp.EditUser(user);
                    return RedirectToAction("Index");
                }
                else
                    return View(userDetails);
            }
            catch
            {
                return View(userDetails);
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
