using ATM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    public class AuthController : Controller
    {
        private MainModel db = new MainModel();
        // GET: User/Create
        [Gordibute(JustNonAuthorized =true)]
        public ActionResult Login()
        {
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [Gordibute(JustNonAuthorized = true)]
        public ActionResult Login(Models.Login login)
        {
            if (ModelState.IsValid)
            {
                var us = db.People.Where(x => x.Username == login.User).FirstOrDefault();
                if (us != null && us.Password == new RexaHash().MD5(login.Pass))
                {
                    TempData["Message"] = "ورود موفق به سیستم!";
                    Session["Username"] = us.Username;
                }
                else
                    TempData["Message"] = "نام کاربری یا کلمه ی عبور صحیح نیست!";

                if (ControllerContext.IsChildAction)
                    ControllerContext.HttpContext.Response.Redirect(ControllerContext.HttpContext.Request.Url.ToString());
                else
                    return RedirectToAction("Index", "Home");
            }
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }

        [Gordibute(JustNonAuthorized =true)]
        public ActionResult Register()
        {
            if (ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }

        [HttpPost]
        [Gordibute(JustNonAuthorized =true)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "Id,Username,Password,Firstname,Lastname,PictureFileId")] Person person)
        {
            if (ModelState.IsValid)
            {
                person.Id = Guid.NewGuid();
                person.Password = new RexaHash().MD5(person.Password);
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