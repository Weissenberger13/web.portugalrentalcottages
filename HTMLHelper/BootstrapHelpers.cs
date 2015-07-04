using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HtmlTags;
using System.Web.Mvc.Html;

namespace BootstrapVillas.HTMLHelper
{
    public static class BootstrapHelpers
    {

        public static HtmlTag BootstrapButton(this HtmlHelper helper, string text)
        {
            
            return new HtmlTag("button")
                .Attr("type", "submit")
                .AddClasses("btn", "btn primary")
                .Text(text)
                ;

        }

    }
}
