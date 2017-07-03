using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    public class AuthController : Controller
    {
        private MainModel db = new MainModel();
        // GET: User/Create
        public ActionResult Login()
        {
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            TempData["Message"] = "ورود موفق به سیستم!";
            if (ModelState.IsValid)
            {



                if (ControllerContext.IsChildAction)
                    ControllerContext.HttpContext.Response.Redirect(ControllerContext.HttpContext.Request.Url.ToString());
                else
                    return RedirectToAction("Index", "Home");
            }
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }

        public ActionResult Register()
        {
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Id,Username,Password,Firstname,Lastname,PictureFileId")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid();
                db.People.Add(person);
                await db.SaveChangesAsync();

                TempData["Message"] = "ثبت نام با موفقیت انجام شد!";

                if (ControllerContext.IsChildAction)
                    ControllerContext.HttpContext.Response.Redirect(ControllerContext.HttpContext.Request.Url.ToString());
                else
                    return RedirectToAction("Index", "Home");
            }

            TempData["Message"] = "حداقل یکی از ورودی ها نامعتبر است";
            if (ControllerContext.IsChildAction)
                return PartialView(person);
            return View(person);


        }
    }
}