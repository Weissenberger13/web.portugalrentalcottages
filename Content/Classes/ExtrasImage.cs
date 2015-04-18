using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Content.Classes
{
    public class ExtrasImage
    {
        public long? BookingExtraID { get; set; }
        public string ExtraLegacyReference { get; set; }
        public string FullImageName { get; set; }
        public string ImageFullFilePath { get; set; }
        public string ImageDescription { get; set; }
        public string ImageDirectory { get; set; }
        public bool IsMainImage { get; set; }

        DirectoryInfo ServerPath { get; set; }
        DirectoryInfo ImagesDirectoryPath { get; set; }



        public ExtrasImage(HttpServerUtilityBase Server, string extraLegacyReference, string imageDirectory)
        {          

            BookingExtraID = 0;
            ExtraLegacyReference = extraLegacyReference;
            FullImageName = "";
            ImageFullFilePath = "";
            ImageDescription = "";
            ImageDirectory = "";
            IsMainImage = false;

            ServerPath = new DirectoryInfo(Server.MapPath(@"~\"));
            ImagesDirectoryPath = new DirectoryInfo(this.ServerPath + "ExtraImages\\" + extraLegacyReference);

        }






    }

}