using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.WebPages;

using BootstrapVillas.Controllers;
using BootstrapVillas.Models;
using Document = BootstrapVillas.Models.Document;

namespace BootstrapVillas.Content.Classes.EventCommands
{

    [NotMapped]
    public class EventCommandCreateDocument : EventCommand
    {
        Customer customer { get; set; }
        Booking booking { get; set; }
        public BookingExtraSelection bes { get; set; }
        public PRCDocument.PRCDocumentType documentType { get; set; }


        public EventCommandCreateDocument(Event anEvent, Customer customer, PRCDocument.PRCDocumentType documentType, Booking booking = null, BookingExtraSelection bes = null)
        {
            //inject properties
            this.Event = anEvent;
            this.customer = customer;
            this.documentType = documentType;
            this.booking = booking;
            this.bes = bes;
        }

        public override EventCommandResult ExecuteCommand()
        {
            var result = new EventCommandResult();
            
     
            
            var dc = new DocumentGenerationController();
            //create a document with all parsed variables
            var document = dc.CreateDocument(this.customer, this.documentType, this.booking, this.bes);
            
            this.Event.Documents.Add(new Document
            {               
                DocumentBLOB = document
            });


            if (booking != null)
            {
                this.Event.Documents.FirstOrDefault().DocumentName = "C"+customer.CustomerID + "B"+booking.BookingID +
                                                                   DateTime.Now.ToLongDateString() +
                                                                   documentType.ToString();
            }
            if (bes != null)
            {
                this.Event.Documents.FirstOrDefault().DocumentName = "C"+customer.CustomerID + "BE"+bes.BookingExtraSelectionID +
                                                                     DateTime.Now.ToLongDateString() +
                                                                     documentType.ToString();
            }

            //get that doc just created and add it to the event

           // this.Event.Documents.Add(document);


            if (!document.Equals(null) && !document.Equals(""))
            {
                result.ResultCode = 200;
                result.CommandExecutedInfo = "DocumentOutCommand";
                result.ResultMessage = "OK";                
            }
            else
            {
                result.ResultCode = 800;
                result.CommandExecutedInfo = "DocumentOutCommand";
                result.ResultMessage = "Fail";    
            }

            
            
            return result;
        }


    }
}