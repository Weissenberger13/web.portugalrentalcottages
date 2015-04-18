using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
namespace BootstrapVillas.Content.Classes
{
    public class PRCVImage : IPRCVImage
    {

        public string ImageName { get; set; }
        public string ImageExtension { get; set; }
        public string FullImageName { get; set; }
        public string ImageFullFilePath { get; set; }
        public string ImageDescription { get; set; }
        public string ImageDirectory { get; set; }

        protected string ServerPath { get; set; }

        //empty constructor
        public PRCVImage()
        {
            

            ImageName = "";
            ImageExtension = "";
            FullImageName = "";
            ImageFullFilePath = "";
            ImageDescription = "";
            ImageDirectory = "";
           // HttpContextBase Server;
           // ServerPath = new DirectoryInfo(Server.MapPath(@"~\"));
        }


        public bool CapitalizeCamelCasedString()
        {
            if (this.ImageDescription != "")
            {
                Regex.Replace(this.ImageDescription, "([A-Z])", "$0");

                return true;
            }
            return false;


        }
   
    }

   
}