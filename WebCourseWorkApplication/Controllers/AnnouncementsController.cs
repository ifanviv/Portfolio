using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebCourseWorkApplication.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace coursework.Controllers
{
    public class AnnouncementsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Announcements

        public ActionResult Index()
        {
            var user = (ClaimsIdentity)User.Identity;
            ViewBag.canPost = user.FindFirst("canPOST") != null ? true : false;
            ViewsToAnnouncements();
            return View();
        }

        //method called whenver announcement tab is pressed and to secure authentication is made for lecturer using claims

        public ActionResult BuildAnnouncementsTable()
        {
            var userRole = (ClaimsIdentity)User.Identity;
            ViewBag.canPost = userRole.FindFirst("canPOST") != null ? true : false;
            
            return PartialView("_AnnouncementsTable", GetMyAnnouncements());
        }

        //Creating search bar in order to make search post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXFilterBuildAnnouncementsTable(Announcement announcement)
        {

            string searchString = announcement.Search;
            if (!String.IsNullOrEmpty(searchString))
            {
                 var userRole = (ClaimsIdentity)User.Identity;
                 ViewBag.canPost = userRole.FindFirst("canPOST") != null ? true : false;
                 return PartialView("_AnnouncementsTable",
                 GetMySearhcedAnnouncements(searchString));
            }
            return BuildAnnouncementsTable();
        }

        //To update our link model for once its passed to our partial view in order for framework to access our comments and announcements but filtered for search

        private AnnCommConnection GetMySearhcedAnnouncements(string id)
        {

            AnnCommConnection con = new AnnCommConnection();
            IEnumerable<Announcement> SevAnnouncements = db.Announcements.ToList().Where(s => s.Title.Contains(id));
            con.Announcement = new List<Announcement>();
            foreach (var elem in SevAnnouncements)
            {
                con.Announcement.Add(elem);
            }
            db.AnnCommConnections = con;


            return con;
        }
        //To update our link model for once its passed to our partial view in order for framework to access our comments and announcements
        private AnnCommConnection GetMyAnnouncements()
        {
 
            AnnCommConnection con = new AnnCommConnection();
            ICollection<Announcement> SevAnnouncements = db.Announcements.ToList();
            con.Announcement = new List<Announcement>();
            foreach(var elem in SevAnnouncements)
            {
                con.Announcement.Add(elem);
            }
            db.AnnCommConnections = con;
            db.SaveChanges();
            ViewsToAnnouncements();


            return con;
        }
        //In order to obtain our views for each annuncement. Ran when student realoads page
        private void ViewsToAnnouncements()
        {
            var userRole = (ClaimsIdentity)User.Identity;
            var canPost = userRole.FindFirst("canPOST") != null ? true : false;
            if (canPost)
            {

                return;
            }


            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
            ViewedUser viewUser;
            foreach (var elem in db.Announcements.ToList())
            {
                bool flag = true;
                foreach (var element in db.ViewedUsers.ToList())
                {
                    if (element.AnnouncementId == elem.AnnouncementId
                        && element.User == currentUser)
                    {
                        flag  = false;
                        break;
                    }
                   
                }
                if (flag)
                {

                    viewUser = new ViewedUser();
                    viewUser.UserId = currentUser.Id;
                    viewUser.AnnouncementId = elem.AnnouncementId;
                    viewUser.User = currentUser;
                    viewUser.Announement = elem;
                    db.ViewedUsers.Add(viewUser);
                    db.SaveChanges();

                }
           
            }
            
            db.SaveChanges();
        }

        // GET: Announcements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "AnnouncementId,Title,ExtraDetails")] Announcement announcement)
        {
            var user = (ClaimsIdentity)User.Identity;
            bool canPost = user.FindFirst("canPOST") != null ? true : false;
            ViewBag.canPost = user.FindFirst("canPOST") != null ? true : false;
            if (!canPost)
            {
                return RedirectToAction("Index", "Announcements");
            }
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                announcement.User = currentUser;
                announcement.TimeStamp = DateTime.Now.ToString("h:mm:ss tt");
                announcement.Comment = new List<Comment>();
                db.Announcements.Add(announcement);
                GetMyAnnouncements();
                db.SaveChanges();
                return BuildAnnouncementsTable();
            }

          return new EmptyResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCommentCreate(int? id, Comment comment)
        {
            if (ModelState.IsValid)
            {
                Announcement Ann = db.Announcements.Find(id);
                Ann.Comment.Add(comment);
                GetMyAnnouncements();
                db.SaveChanges();
            }

            return BuildAnnouncementsTable();
        }

        // GET: Announcements/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = (ClaimsIdentity)User.Identity;
            bool canPost = user.FindFirst("canPOST") != null ? true : false;
            if (!canPost)
            {
                return RedirectToAction("Index", "Announcements");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AnnouncementId,Title,ExtraDetails,TimeStamp,Author")] Announcement announcement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(announcement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(announcement);
        }

        // GET: Announcements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Announcement announcement = db.Announcements.Find(id);
            if (announcement == null)
            {
                return HttpNotFound();
            }
            return View(announcement);
        }

        // POST: Announcements/Delete/5
        //On delete post request with necesary bind, must delete accosiated comments for announcement to avoid update database error
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Announcement announcement = db.Announcements.Find(id);


            LinkedList<Comment> comment = new LinkedList<Comment>();
            foreach (var com in announcement.Comment)
            {
                comment.AddFirst(com);
            }
            foreach (var com in comment)
            {
                db.Comments.Remove(com);
            }
            db.Announcements.Remove(announcement);
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
