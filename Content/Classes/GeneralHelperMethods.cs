using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;
using BootstrapVillas.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace BootstrapVillas.Content.Classes
{
    public static class GeneralStaticHelperMethods
    {
        //////////TITLES//////////////////
        //////////////////////////////////

        public static List<SelectListItem> AddBlankDDLItemAndReturnSelectListItems<T>(IEnumerable<T> listOfType, string listID, string listValue)
        {
            // This generic method returns a List with ten elements initialized.
            // ... It uses a type parameter.
            // ... It uses the "open type" T.
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.AddRange(new SelectList(listOfType, listID, listValue));
            listItems.Insert(0, new SelectListItem { Text = "--Please Select--", Value = "" });                     

            return listItems;
        }

        public static bool IsPersonAdultChildOrInfant(BookingExtraParticipant theParticipant)
        {
            DateTime dob = (DateTime)theParticipant.BookingExtraParticipantDOB;

            //under 2
            if ((DateTime.Now.Subtract(dob).Days < 730))
            {
                theParticipant.BookingExtraParticipantInfant = true;
                theParticipant.BookingExtraParticipantChild = false;

                return true;
            }


            else if ((DateTime.Now.Subtract(dob).Days < 6575))
            {

                theParticipant.BookingExtraParticipantInfant = false;
                theParticipant.BookingExtraParticipantChild = true;
                return true;
            }

            theParticipant.BookingExtraParticipantInfant = false;
            theParticipant.BookingExtraParticipantChild = false;

            return false;

        }


        public enum AdultChildInfant { Adult, Child, Infant}

        public static bool IsPersonAdultChildOrInfant(BookingParticipant theParticipant)
        {
            DateTime dob = (DateTime)theParticipant.BookingParticipantDOB;

            //under 2
            if ((DateTime.Now.Subtract(dob).Days < 730))
            {
                theParticipant.BookingParticipantInfant = true;
                theParticipant.BookingParticipantChild = false;

                return true;
            }


            else if ((DateTime.Now.Subtract(dob).Days < 6575))
            {

                theParticipant.BookingParticipantInfant = false;
                theParticipant.BookingParticipantChild = true;
                return true;
            }

            theParticipant.BookingParticipantInfant = false;
            theParticipant.BookingParticipantChild = false;

            return false;

        }


        //GET the title
        public static List<Title> GetTitles()
        {
            using (PortugalVillasContext _db = new PortugalVillasContext())
            {

                List<Title> TitleList = _db.Titles.ToList();

                return TitleList;
            }

        }


        //end method
        public static List<SelectListItem> GetTitlesAsSelectListItems()
        {

            using (PortugalVillasContext _db = new PortugalVillasContext())
            {
                List<SelectListItem> TitleSelectListItems = _db.Titles.Select(x => new SelectListItem
                {
                    Text = x.TitleID.ToString(),
                    Value = x.Title1.ToString()
                }).ToList();

                return TitleSelectListItems;

            }

        }


        public static int CalculateNoofDays(DateTime? startDate, DateTime? endDate)
        {

            try
            {

                DateTime castStartDate = (DateTime)startDate;
                DateTime castEndDate = (DateTime)endDate;

                int noOfDays = 0;
                noOfDays = (castEndDate - castStartDate).Days + 1;

                return noOfDays;
            }
            catch (Exception ex)
            { throw ex; }

        }

        public static int CalculateNoofNights(DateTime? startDate, DateTime? endDate)
        {

            try
            {

                DateTime castStartDate = (DateTime)startDate;
                DateTime castEndDate = (DateTime)endDate;

                int noOfNights = 0;
                noOfNights = (castEndDate - castStartDate).Days;

                return noOfNights;
            }
            catch (Exception ex)
            { throw ex; }



        }









        //end class
    }
}