using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapVillas.Content.CustomAttribute
{
     /// <summary>
         /// This attribute indicates that a method is an actual page and gives the data for it
         /// </summary>
         [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
         public class MVCUrlAttribute : ActionFilterAttribute
         {
             public string Url { get; private set; }
     
             public MVCUrlAttribute(string url)
            {
                this.Url = url;
            }
    
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                string fullyQualifiedUrl = filterContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + this.Url;
                // We build HTML here because we want the View to be easily able to include it without any conditionals
                // and because the ASP.NET WebForms view engine sometimes doesn’t subsitute <% in certain head items
                filterContext.Controller.ViewData["CanonicalUrl"] = @"<link rel=""canonical"" href=""" + fullyQualifiedUrl + " />";
                base.OnResultExecuting(filterContext);
            }
        }
}