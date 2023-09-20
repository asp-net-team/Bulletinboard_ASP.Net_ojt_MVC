using Bulletinboard.Web.DAL;
using Bulletinboard.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Bulletinboard.Web.Controllers
{
    public class UserController : Controller
    {
        private BulletinboardContext db = new BulletinboardContext();

        // GET: User/Index
        /// <summary>
        /// Show user list form
        /// </summary>
        /// <returns>User list view</returns>
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: User/Index
        /// <summary>
        /// Search user accroding to name or email keyword
        /// </summary>
        /// <returns>User list view</returns>
        [HttpPost]
        public ActionResult Index(string name, string email)
        {
            int userCount = 0;
            List<User> userList = new List<User>();
            // If searched name and email are empty, show all user list as default
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(email))
            {
                userList = db.Users.ToList();
            } else
            {
                // add searched name or eamil in ViewBag for data holding
                if (!string.IsNullOrEmpty(name))
                {
                    ViewBag.name = name;
                }
                if (!string.IsNullOrEmpty(email))
                {
                    ViewBag.email = email;
                }
                // If name and email are not empty, search user according to name and email keyword
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email))
                {
                    userList = db.Users.Where(eachUser => eachUser.Name.Contains(name) || eachUser.Email.Contains(email)).ToList();
                }
                // If name is not empty, search user according to name keyword
                else if (!string.IsNullOrEmpty(name))
                {
                    userList = db.Users.Where(eachUser => eachUser.Name.Contains(name)).ToList();
                }
                // If email is not empty, search user according to email keyword
                else if (!string.IsNullOrEmpty(email))
                {
                    userList = db.Users.Where(eachUser => eachUser.Email.Contains(email)).ToList();
                }
            }
            // Get user count of searching result
            userCount = userList.Count;
            if (userCount == 0)
            {
                ViewBag.emptyMessage = "No user was found";
            }
            return View(userList);
        }

        // GET: User/Create
        /// <summary>
        /// Show user create form
        /// </summary>
        /// <returns>User create view</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        /// <summary>
        /// Insert user
        /// </summary>
        /// <param name="userView"></param>
        /// <returns>User list view or user create view</returns>
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userView, HttpPostedFileBase Profile)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Name = userView.Name,
                    Email = userView.Email,
                    Password = userView.Password,
                    Type = userView.Type,
                    Phone = userView.Phone,
                    Birthday = userView.Birthday,
                    Address = userView.Address,
                    Profile = userView.Profile.FileName
                };
                db.Users.Add(user);
                db.SaveChanges();
                // Store profile image in images folder
                userView.Profile.SaveAs(Server.MapPath("~/Resource/images/" + userView.Profile.FileName));

                TempData["success"] = "User has been successfully created";
                return RedirectToAction("Index");
            }
            return View(userView);
        }

        // GET: User/Edit/5
        /// <summary>
        /// Show user edit form
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User edit view</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            UserEditModel userEditModel = new UserEditModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Type = user.Type,
                Phone = user.Phone,
                Birthday = user.Birthday,
                Address = user.Address,
                ProfilePath = "~/Resource/images/" + user.Profile
            };
            ViewBag.editImageName = user.Profile;
            return View(userEditModel);
        }

        // POST: User/Edit/5
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User list view or user create view</returns>
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Email,Password,Type,Phone,Birthday,Address,Profile")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

                TempData["success"] = "User has been successfully created";
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Details/5
        /// <summary>
        /// Show user detail form
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User detail view</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Delete/5
        /// <summary>
        /// Show user delete form
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User delete view</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User list view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();

            TempData["success"] = "Post has been successfully deleted";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
