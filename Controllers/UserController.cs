using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    public class UserController : Controller
    {
        public MainModel db = new MainModel();

        [Gordibute]
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

        [Gordibute]
        public ActionResult Status()
        {
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }

    }
}