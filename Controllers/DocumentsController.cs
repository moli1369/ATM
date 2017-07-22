using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATM.Models;

namespace ATM.Controllers
{
    [Gordibute]
    public class DocumentsController : Controller
    {
        private MainModel db = new MainModel();

        // GET: Documents
        public ActionResult Index()
        {
            Guid UserID = Guid.Parse(Session["UserId"].ToString());
            var documents = db.Documents.Where(x => x.PersonId == UserID);

            int today = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
            if (ControllerContext.IsChildAction)
                return PartialView(documents.OrderByDescending(x => x.Expire)/*.Where(x => x.Expire >= today)*/.Take(5).ToList());
            return View(documents.OrderByDescending(x => x.Submit).ToList());
        }

        // GET: Documents/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            Guid UserId = Guid.Parse(Session["UserId"].ToString());
            if (document.PersonId != UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        public ActionResult Create()
        {
            // ViewBag.PersonId = new SelectList(db.People, "Id", "Username");
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        // public async Task<ActionResult> Create([Bind(Include = "Id,Title,Submit,Expire,PersonId,Body,Comment")] Document document)
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Submit,Expire,Body,Comment")] Document document)
        {
            document.PersonId = Guid.Parse(Session["UserId"].ToString());
            if (ModelState.IsValid)
            {
                document.Id = Guid.NewGuid();
                db.Documents.Add(document);
                await db.SaveChangesAsync();
                TempData["Message"] = "سند ایجاد شد";
                return RedirectToAction("Edit", "Documents", new { id = document.Id });
                // return RedirectToAction("Index");
            }

            // ViewBag.PersonId = new SelectList(db.People, "Id", "Username", document.PersonId);
            if (ControllerContext.IsChildAction)
                return PartialView(document);
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            Guid UserId = Guid.Parse(Session["UserId"].ToString());
            if (document.PersonId != UserId)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            if (document == null)
            {
                return HttpNotFound();
            }
            if (ControllerContext.IsChildAction)
                return PartialView(document);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Submit,PersonId,Expire,Body,Comment")] Document document)
        {
            if (ModelState.IsValid)
            {
                Guid UserId = Guid.Parse(Session["UserId"].ToString());
                if (document.PersonId != UserId)
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                db.Entry(document).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["Message"] = "تغییرات با موفقیت اعمال شد";
            }
            if (ControllerContext.IsChildAction)
                return PartialView(document);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            Guid UserId = Guid.Parse(Session["UserId"].ToString());
            if (document.PersonId != UserId)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            if (document == null)
            {
                return HttpNotFound();
            }
            if (ControllerContext.IsChildAction)
                return PartialView(document);
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Document document = await db.Documents.FindAsync(id);
            if (document.PersonId != Guid.Parse(Session["UserId"].ToString()))
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            db.Documents.Remove(document);
            await db.SaveChangesAsync();
            TempData["Message"] = "عملیات حذف سند موفقیت آمیز بود";
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
