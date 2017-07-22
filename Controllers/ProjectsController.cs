using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATM.Models;

namespace ATM.Controllers
{
    [Gordibute]
    public class ProjectsController : Controller
    {
        private MainModel db = new MainModel();

        // GET: Projects
        public ActionResult Index()
        {
            var UserId = Guid.Parse(Session["UserId"].ToString());
            var projects = db.Projects.Where(x => x.OwnerId == UserId)
                .Include(p => p.DateDimension)
                .Include(p => p.DateDimension1)
                .Include(p => p.Person);
            if (ControllerContext.IsChildAction)
                return PartialView(projects.ToList());
            return View(projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            if (project.OwnerId != Guid.Parse(Session["UserId"].ToString()))
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.Start = new SelectList(db.DateDimensions, "DateKey", "DaySuffix");
            ViewBag.End = new SelectList(db.DateDimensions, "DateKey", "DaySuffix");
            ViewBag.OwnerId = new SelectList(db.People, "Id", "Username");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Start,End")] Project project)
        {
            project.OwnerId = Guid.Parse(Session["UserId"].ToString());
            if (ModelState.IsValid)
            {
                project.Id = Guid.NewGuid();
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Start = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", project.Start);
            ViewBag.End = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", project.End);
            ViewBag.OwnerId = new SelectList(db.People, "Id", "Username", project.OwnerId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            if (project.OwnerId != Guid.Parse(Session["UserId"].ToString()))
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            ViewBag.Start = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", project.Start);
            ViewBag.End = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", project.End);
            ViewBag.OwnerId = new SelectList(db.People, "Id", "Username", project.OwnerId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Start,End")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Start = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", project.Start);
            ViewBag.End = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", project.End);
            ViewBag.OwnerId = new SelectList(db.People, "Id", "Username", project.OwnerId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            if (project.OwnerId != Guid.Parse(Session["UserId"].ToString()))
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Project project = db.Projects.Find(id);
            if (project.OwnerId != Guid.Parse(Session["UserId"].ToString()))
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            db.Projects.Remove(project);
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
