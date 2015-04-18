using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Words;

namespace BootstrapVillas.Controllers
{
    public class AsposeController : Controller
    {
        //
        // GET: /Aspose/

        public void ParseDocument(string filePathAndName)
        {

            Aspose.Words.Document doc = new Document(@"h:\test\template2.doc");
            doc.Save(@"h:\test\testBuga.pdf", SaveFormat.Pdf);

        }

    }
}
