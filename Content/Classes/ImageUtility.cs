using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using BootstrapVillas.Content;
using BootstrapVillas.Models;
using DotNetOpenAuth.Messaging;

namespace BootstrapVillas.Content.Classes
{ 
    //Class to manage the images for each directory
  
    public class PRCImageCollection
    {

        public static string IfFeaturedExistsReturnURLElseReturnRandomImage(Property property, DirectoryInfo Server)
        {

            string imgRef = @"/PropertyImages/" + property.LegacyReference + "/featured/featured.jpg";
            var theImageURL = PRCImageCollection.GetRandomImageURLForProperty(property, Server);
            var featuredPath = Server.FullName + "PropertyImages\\" + property.LegacyReference + "\\featured\\featured.jpg";

            if (File.Exists(featuredPath))
            {
                theImageURL = imgRef;
            }


            return theImageURL;
        }

        public static string GetRandomImageURLForProperty(Property property, DirectoryInfo Server)
        {
            try
            {
                string[] randomImageString = Directory.GetFileSystemEntries((Server.FullName + "PropertyImages\\" + property.LegacyReference), "*.jpg", SearchOption.AllDirectories);
                randomImageString.AddRange(Directory.GetFileSystemEntries((Server.FullName + "PropertyImages\\" + property.LegacyReference), "*.png", SearchOption.AllDirectories));

                List<string> randomImageStringsNoPath = new List<string>();

                foreach (var img in randomImageString)
                {
                    var stripAllThis = Server.FullName;
                    img.Remove(0, Server.FullName.Length);
                    randomImageStringsNoPath.Add(img.Replace("\\", "/").Remove(0, stripAllThis.Length));
                }



                Random rand = new Random();
                var imgNo = rand.Next(0, randomImageStringsNoPath.Count());
                string theImageURL = "/";

                if (randomImageStringsNoPath.Any())
                {
                    return theImageURL += randomImageStringsNoPath[imgNo];
                }
            }
            catch (Exception ex)
            {
                // do fuck all
                
            }

           
            return "";
        }

        long? PropertyID {get; set; }
        public string PRCRef { get; set; }
        DirectoryInfo ServerPath { get; set; }
        DirectoryInfo CarouselImagesDirectoryPath {get; set;}
        DirectoryInfo PropertyImagesDirectoryPath {get; set;}

/*        string CarouselPathString = "/CarouselImages/";*/
        string PropertyPathString = "/PropertyImages/";
        
        //holds the images
        public List<PRCVImage> CarouselImageList = new List<PRCVImage>();
        public List<PRCVImage> PropertyImageList = new List<PRCVImage>();


        public bool CarouselImagesExists = false;
        public bool PropertyImagesExists = false;



        public PRCImageCollection(HttpServerUtilityBase Server, string PRCRef)
        {
            this.PRCRef = PRCRef;
            this.ServerPath = new DirectoryInfo(Server.MapPath(@"~\"));

          
            this.PropertyImagesDirectoryPath = new DirectoryInfo(this.ServerPath + "PropertyImages\\" + PRCRef);

            //get all directories and files and create PRCImages
        /*    GetAllImagesFromADirectoryAndPlaceInACollection(CarouselImagesDirectoryPath, CarouselImageList, this.CarouselImagesExists);*/
            GetAllImagesFromADirectoryAndPlaceInACollection(PropertyImagesDirectoryPath, PropertyImageList, this.PropertyImagesExists);



            if (CarouselImageList.Count > 0)
            {
                CarouselImagesExists = true;
                foreach (var image in CarouselImageList)
                {
                    image.CapitalizeCamelCasedString();

                }
            }
            if (PropertyImageList.Count > 0)
            {
                PropertyImagesExists = true;
                foreach (var image in PropertyImageList)
                {
                    image.CapitalizeCamelCasedString();

                }


            }
        }
            

        



        //////////////////////////////////
        ///// Begin Methods //////////////
        //////////////////////////////////



        //gets all images for a property
        

        //gets all the images for this particular PRCRef

        public int PlaceAllImagesInACollection()
        {



            return 0;
        }

        

        //returns the count of images - 0 = fail, any positive number = pass
        public int GetAllImagesFromADirectoryAndPlaceInACollection(DirectoryInfo theDirectory, List<PRCVImage> theCollection, bool DoesCollectionExist)
        {
            //check there are elements
            if(theDirectory.Exists)
            {
           
                foreach (var directory in theDirectory.GetDirectories())
                {
                    //get all dirs and files in those dirs
                    foreach (var file in directory.GetFiles())
                    {
                        if (file.Extension.ToLower() == ".jpg" || file.Extension.ToLower() == ".bmp" || file.Extension.ToLower() == ".png")
                        {
                            //create a new PRCVImage object and assign it's values
                            theCollection.Add(
                                new PRCVImage()
                                {

                                    ImageName = file.ToString(),
                                    ImageDescription = directory.ToString(),
                                    ImageFullFilePath = theDirectory.ToString() + "\\" + directory.ToString(),
                                    FullImageName = file.ToString(),
                                    ImageDirectory = directory.ToString()

                                });

                        }
                    }

                }



                return theCollection.Count;
            }
            return 0;
        }



    }
}