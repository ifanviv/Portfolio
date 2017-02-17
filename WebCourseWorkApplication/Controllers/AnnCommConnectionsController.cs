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
    public class AnnCommConnectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AnnCommConnections
        public ActionResult Index()
        {
            return View(db.AnnCommConnections);
        }

        // GET: AnnCommConnections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnCommConnection annCommConnection = db.AnnCommConnections;
            if (annCommConnection == null)
            {
                return HttpNotFound();
            }
            return View(annCommConnection);
        }

        // GET: AnnCommConnections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnnCommConnections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id")] AnnCommConnection annCommConnection)
        {
            if (ModelState.IsValid)
            {
                db.AnnCommConnections=annCommConnection;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(annCommConnection);
        }

        // GET: AnnCommConnections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnCommConnection annCommConnection = db.AnnCommConnections;
            if (annCommConnection == null)
            {
                return HttpNotFound();
            }
            return View(annCommConnection);
        }

        // POST: AnnCommConnections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] AnnCommConnection annCommConnection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(annCommConnection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(annCommConnection);
        }

        // GET: AnnCommConnections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnnCommConnection annCommConnection = db.AnnCommConnections;
            if (annCommConnection == null)
            {
                return HttpNotFound();
            }
            return View(annCommConnection);
        }

        // POST: AnnCommConnections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnnCommConnection annCommConnection = db.AnnCommConnections;
            db.AnnCommConnections=null;
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
