using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Web.Mvc;
using Bulletinboard.Web.DAL;
using Bulletinboard.Web.Models;

namespace Bulletinboard.Web.Controllers
{
    public class PostController : Controller
    {
        private BulletinboardContext db = new BulletinboardContext();

        // GET: Post
        /// <summary>
        /// Show poar list form
        /// </summary>
        /// <returns>Poar list view</returns>
        public ActionResult Index()
        {
            return View(db.Posts.ToList());
        }

        // GET: Post/Details/5
        /// <summary>
        /// Show post detail form
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Post detail view</returns>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Post/Create
        /// <summary>
        /// Show post create form
        /// </summary>
        /// <returns>Post create view</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        /// <summary>
        /// Insert Post
        /// </summary>
        /// <param name="postView"></param>
        /// <returns>Post list view or post create view</returns>
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel postView)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post
                {
                    Title = postView.Title,
                    Description = postView.Description
                };
                db.Posts.Add(post);
                db.SaveChanges();

                TempData["success"] = "Post has been successfully created";
                return RedirectToAction("Index");
            }
            return View(postView);
        }

        // GET: Post/Edit/5
        /// <summary>
        /// Show post edit form
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Post edit view</returns>
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            PostViewModel postViewModel = new PostViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description
            };
            return View(postViewModel);
        }

        // POST: Post/Edit/5
        /// <summary>
        /// Update post
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Post list view or post create view</returns>
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostId,Title,Description")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();

                TempData["success"] = "Post has been successfully created";
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Post/Delete/5
        /// <summary>
        /// Show post delete form
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Post delete view</returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        /// <summary>
        /// Delete post
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Post list view</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
