using System.ComponentModel.DataAnnotations;
using BootstrapVillas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Content.Classes
{
    /// <summary>
    /// this is a doucment, built from a template,
    /// </summary>
    public class PRCDocument
    {

        public enum PRCDocumentType
        {
            NULL_OBJECT = 0,
            UK_BookingRequestForm = 1,
            EU_BookingRequestForm = 2, 
            UK_LateBookingRequestForm=3,
            EU_LateBookingRequestForm = 4,
            UK_BookingConfirmationVoucher = 5,
            EU_BookingConfirmationVoucher = 6,
            UK_LateBookingConfirmationVoucher = 7,
            EU_LateBookingConfirmationVoucher = 8,
            BookingConfirmationTicket=9,
            RentalRemittanceAdvice=10,
            HouseNotes=11,
            LocalAreaInfo=12,
            SecurityDeposit = 13,
            BookingMap=14,
            BookingFAQ=15,
            BookingHouseRules=16,
            BookingFinalDocumentBundle=17,
            UK_WineTasting = 101,
            EU_WineTasting = 102,
            UK_AirportTransfer = 103,
            EU_AirportTransfer = 104,
            UK_CarRental = 105,
            EU_CarRental =106,
            UK_CoachTour = 107,
            EU_CoachTour = 108
            
        }

        [Key]
        public long PRCDocumentID { get; set; }
        public DataTable MainDataTable { get; set; } //NEED TO SET NAME SO IT RENDER PROPERLY
        public DataTable SecondaryDataTable { get; set; } //NEED TO SET NAME SO IT RENDER PROPERLY
        public PRCDocumentType Type { get; set; }
        public string ServerDocumentURL { get; set; }
        public string SavePath { get; set; }
        public string FileName { get; set; }        
        public long CustomerID { get; set; }
        public long BookingID { get; set; }
        public long BookingExtraSelectionID { get; set; }
        public long EventID { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Sent { get; set; }
        public DateTime SentDate { get; set; }      
        public PRCDocument(PRCDocumentType type)
        {
            this.Type = type;
            

            this.MainDataTable = new DataTable();
            this.SecondaryDataTable = new DataTable();

            //set the correct server path

            //set the correct document path

            //depending on type, set table name 
        }


        public PRCDocument(Customer customer, PRCDocumentType type, Booking booking = null, BookingExtraSelection  bookingExtraSelection = null)
        {
            this.Type = type;

            this.MainDataTable = new DataTable();
            this.SecondaryDataTable = new DataTable();

            this.CustomerID = customer.CustomerID;
            if(booking != null)
            {
                BookingID = booking.BookingID;
            }
            if(bookingExtraSelection != null)
            {
                this.BookingExtraSelectionID = bookingExtraSelection.BookingExtraSelectionID;
            }

        }

    }
}