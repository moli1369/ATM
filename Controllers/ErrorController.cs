using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ATM.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [Route("Error/{id:int}")]
        public ActionResult Index(int id = 404, string message = "Not found")
        {
            ViewBag.Message = message;
            ViewBag.Code = id;
            if (Request.IsAjaxRequest() || ControllerContext.IsChildAction)
                return PartialView();
            return View();
        }
    }
}