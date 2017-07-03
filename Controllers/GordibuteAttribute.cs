using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    internal class GordibuteAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            bool CanAccess = false;
            try
            {
                if (actionContext.HttpContext.Session["Username"] != null)
                    CanAccess = true;
            }
            catch
            {
                CanAccess = false;
            }
            if (!CanAccess)
                actionContext.Result = new HttpUnauthorizedResult();
        }
    }
}