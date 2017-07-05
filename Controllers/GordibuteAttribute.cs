using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    internal class GordibuteAttribute : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// Makes everything reverse :)
        /// </summary>
        public bool JustNonAuthorized { get; set; }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            bool CanAccess = false;
            try
            {
                if (actionContext.HttpContext.Session["UserId"] != null)
                    CanAccess = true;
            }
            catch
            {
                CanAccess = false;
            }


            if (JustNonAuthorized)
            {
                if (CanAccess)
                    //throw new HttpException(404, "Not found");
                actionContext.Result = new HttpNotFoundResult();
            }
            else
            {
                if (!CanAccess)
                    //throw new HttpException(401, "Unauthorized");
                actionContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}