using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebCourseWorkApplication.Models;

namespace WebCourseWorkApplication.Controllers
{
    public class ViewedUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ViewedUsers
        public ActionResult Index()
        {
            var viewedUsers = db.ViewedUsers.Include(v => v.Announement);
            return View(viewedUsers.ToList());
        }

        // GET: ViewedUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewedUser viewedUser = db.ViewedUsers.Find(id);
            if (viewedUser == null)
            {
                return HttpNotFound();
            }
            return View(viewedUser);
        }

        // GET: ViewedUsers/Create
        public ActionResult Create()
        {
            ViewBag.AnnouncementId = new SelectList(db.Announcements, "AnnouncementId", "Title");
            return View();
        }

        // POST: ViewedUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,AnnouncementId")] ViewedUser viewedUser)
        {
            if (ModelState.IsValid)
            {
                db.ViewedUsers.Add(viewedUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AnnouncementId = new SelectList(db.Announcements, "AnnouncementId", "Title", viewedUser.AnnouncementId);
            return View(viewedUser);
        }

        // GET: ViewedUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewedUser viewedUser = db.ViewedUsers.Find(id);
            if (viewedUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.AnnouncementId = new SelectList(db.Announcements, "AnnouncementId", "Title", viewedUser.AnnouncementId);
            return View(viewedUser);
        }

        // POST: ViewedUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,AnnouncementId")] ViewedUser viewedUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(viewedUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AnnouncementId = new SelectList(db.Announcements, "AnnouncementId", "Title", viewedUser.AnnouncementId);
            return View(viewedUser);
        }

        // GET: ViewedUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewedUser viewedUser = db.ViewedUsers.Find(id);
            if (viewedUser == null)
            {
                return HttpNotFound();
            }
            return View(viewedUser);
        }

        // POST: ViewedUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewedUser viewedUser = db.ViewedUsers.Find(id);
            db.ViewedUsers.Remove(viewedUser);
            db.SaveChanges();
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
