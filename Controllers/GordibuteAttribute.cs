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

        //public GordibuteAttribute()
        //{

        //}

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


            if (JustNonAuthorized)
            {
                if (CanAccess)
                    actionContext.Result = new HttpUnauthorizedResult();
            }
            else if (!JustNonAuthorized)
            {
                if (!CanAccess)
                    actionContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}