using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace BootstrapVillas.Content.Classes
{
    public class AuthorizePRC : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }


    }
}