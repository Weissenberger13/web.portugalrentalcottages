using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Content.Classes
{
    public class VillaSessionMethods
    {
        public bool AddToSession(Object anObjectToAddToSession)
        {
            //check the object doesn't already exists

            

            //add the object


            return true;
        }

        public static List<T> GetFromSessionAllOfType<T>(object anObjectToTestFor)
        {
            /*Explain: we pass the type of object we want to test for as var 1. Then we iterate through the entire session, and test if each 
             * object is of that type (e.g a Customer or a Booking, iterate over all objects in a session, and returna list of all objects of that 
             * type.
            */

            var TheTypeWeWantToReturn = anObjectToTestFor;
            
            List<T> TheMatchingObjects = new List<T>();

            //now test each object and if it matches type, add to the list
            foreach (string key in HttpContext.Current.Session.Keys)
            {
                var temp = HttpContext.Current.Session[key];
                if (temp.GetType() == TheTypeWeWantToReturn.GetType())
                {
                    TheMatchingObjects.Add((T)temp);

                }


            }
            //return the list

            return TheMatchingObjects;
        }



    
    }
}