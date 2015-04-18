using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;
using BootstrapVillas.Content.Classes;
using Aspose.Words;
using Aspose.Email;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using BootstrapVillas.Data.DataConnections;
using BootstrapVillas.Data.DataAdapters;
using BootstrapVillas.Data.QueriesSQL;
using BootstrapVillas.Data.SQLCommands;
using BootstrapVillas.Data.DataSets;
using BootstrapVillas;
using System.IO;


namespace BootstrapVillas.Models.ViewModels
{
    public class FeaturedPropertyCarouselItem
    {
        
        
        //for each item in the carousel 
        public long propertyID = 0;
        public string propertyName = "";
        public string PRCRef = "";
        public PriceRange propertyPriceRange = new PriceRange();
        public string thePictureURL = ""; //frontView
        public int starRating = 0;
     

        //on the hoverover
        //these are set in view
       int BookingCalendar = 0;
       int MapOfProperty = 0; 

       //methods to populate one of these badboys:
       
       //setup methods
       public void SetThePictureURL(DirectoryInfo ServerPath, string fileName)
       {


           this.thePictureURL = fileName; //@"/PropertyImages/" + PRCRef + @"/featured/featured.jpg";

       }


        public void SetTheAverageStarRating()
        {
            //set a default of 4
            this.starRating = 4;

        }

       

        public bool GetPropertyPriceRange()
        {
            try{
             this.propertyPriceRange = Property.GetPriceRangeForProperty(Property.GetPropertyByID(this.propertyID));

                //code to stop propeties without a pricing failing
             if (this.propertyPriceRange.MinPrice.Equals(0) || this.propertyPriceRange.MaxPrice == null)
             {
                 this.propertyPriceRange.MinPrice = 1;
                 this.propertyPriceRange.MaxPrice = 1;
             }
                //end

             decimal tempDecimal = (decimal)this.propertyPriceRange.MinPrice;
             this.propertyPriceRange.MinPrice = Decimal.Round(tempDecimal);

             tempDecimal = (decimal)this.propertyPriceRange.MaxPrice;
             this.propertyPriceRange.MaxPrice = Decimal.Round(tempDecimal);
                
             return true;
            }
            catch (Exception ex)
            { throw ex; }
            
        }

       //constructor
       public FeaturedPropertyCarouselItem(Property persistenceLayerProperty, DirectoryInfo ServerPath, string mainPictureFileName)
           {
               this.propertyID = persistenceLayerProperty.PropertyID;
               this.propertyName = persistenceLayerProperty.PropertyName;
               this.PRCRef = Property.GetPropertyByID(propertyID).LegacyReference.ToString();





               SetThePictureURL(ServerPath, mainPictureFileName);
               SetTheAverageStarRating();
               GetPropertyPriceRange();
           }



    }
}