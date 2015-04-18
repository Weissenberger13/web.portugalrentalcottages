using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.UI.WebControls;

namespace BootstrapVillas.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ImageUploaderController : Controller
    {
        //
        // GET: /ImageUploader/

        public ActionResult UploadImageFolder()
        {

            return View();
        }
        /*
                public ActionResult UploadImageFolder(string propertyRef, string directoryName)
                {
               /*     Server.MapPath("PropertyImages/tempImage/" + fileupload1.FileName
                    Server.MapPath("CarouselImages/" + fileupload1.FileName#1#



                    return View();
                }*/



        public void ResizeImage(FileUpload FileUpload)
        {
            /*            FileUpload fileupload1 = new FileUpload();
                fileupload1.SaveAs(Server.MapPath("temp/tempImage/" + fileupload1.FileName));
            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath("images/tempImage/" + fileupload1.FileName));
            int newwidthimg = 200;
            float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
            int newHeight = 200;
            System.Drawing.Bitmap bitMAP1 = new System.Drawing.Bitmap(newwidthimg, newHeight);
            Graphics imgGraph = Graphics.FromImage(bitMAP1);
            bitMAP1.imgQuality = CompositingQuality.HighQuality;
            bitMAP1.SmoothingMode = SmoothingMode.HighQuality;
            bitMAP1.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imgDimesions = new Rectangle(0, 0, newwidthimg, newHeight);
            bitMAP1.DrawImage(image, imgDimesions);
            bitMAP1.Save(Server.MapPath("images/Shops/" +fileupload1.filename), ImageFormat.Jpeg);
            bitMAP1.Dispose();
            bitMAP1.Dispose();
            image.Dispose();

            bitMAP1.set;*/
        }




        private static void CopyDirectoryStructure(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath =
                    DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);

                        //resize the file                     

                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo diDi = new DirectoryInfo(drs);
                        CopyDirectoryStructure(drs, DestinationPath + diDi.Name, overwriteexisting);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
