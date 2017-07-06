using ATM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Gordibute]
    public class UserController : Controller
    {
        public MainModel db = new MainModel();

        public ActionResult MyProfile()
        {
            var id = Guid.Parse(Session["UserId"].ToString());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            if (ControllerContext.IsChildAction)
                return PartialView(person);
            return View(person);
        }

        public ActionResult Status()
        {
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }

        public ActionResult Edit()
        {
            Guid id = Guid.Parse(Session["UserId"].ToString());
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Person person = db.People.Find(id);
            person.Password = "";
            if (person == null)
                return HttpNotFound();
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,Firstname,Lastname,PictureFileId")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

    }
}