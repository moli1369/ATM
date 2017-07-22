using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ATM.Models;

namespace ATM.Controllers
{
    public class TasksController : Controller
    {
        private MainModel db = new MainModel();

        // GET: Tasks
        public ActionResult Index(Guid Id)
        {
            var tasks = db.Tasks.Where(x => x.ProjectId == Id)
                //.Include(t => t.DateDimension)
                //.Include(t => t.DateDimension1)
                //.Include(t => t.Project)
                ;
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.Start = new SelectList(db.DateDimensions, "DateKey", "DaySuffix");
            ViewBag.End = new SelectList(db.DateDimensions, "DateKey", "DaySuffix");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Start,End,ProjectId")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.Id = Guid.NewGuid();
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Start = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", task.Start);
            ViewBag.End = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", task.End);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.Start = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", task.Start);
            ViewBag.End = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", task.End);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", task.ProjectId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Start,End,ProjectId")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Start = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", task.Start);
            ViewBag.End = new SelectList(db.DateDimensions, "DateKey", "DaySuffix", task.End);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Title", task.ProjectId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
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
