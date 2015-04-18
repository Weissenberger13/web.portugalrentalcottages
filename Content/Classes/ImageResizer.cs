using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Content.Classes
{
    public class ImageResizer
    {

        public void Resize(string imageFile, string outputFile, int newWidth, int newHeight)
        {
            using (var srcImage = Image.FromFile(imageFile))
            {
                
                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                    newImage.Save(outputFile);
                }
            }
        }



    }
}