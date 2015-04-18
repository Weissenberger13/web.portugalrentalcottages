using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.WebPages;
using BootstrapVillas.Controllers;
using BootstrapVillas.Models;
using BootstrapVillas.Models.Events;
using Document = BootstrapVillas.Models.Document;

namespace BootstrapVillas.Content.Classes.EventCommands
{

    /// <summary>
    /// Bundles up docs in a dir and sends with email template
    /// </summary>
    [NotMapped]
    public class EventCommandDocumentOutDirectoryBundleAndEmail : EventCommand
    {
        Customer customer { get; set; }
        Booking booking { get; set; }
        public BookingExtraSelection bes { get; set; }
        public PRCDocument.PRCDocumentType documentType { get; set; }
        string BundleDir { get; set; }

        public EventCommandDocumentOutDirectoryBundleAndEmail(string docsPath, Event anEvent, Customer customer, PRCDocument.PRCDocumentType documentType, Booking booking = null, BookingExtraSelection bes = null)
        {
            this.Event = anEvent;
            this.customer = customer;
            this.documentType = documentType;
            this.booking = booking;
            this.bes = bes;
            this.BundleDir = docsPath;

        }


        public override EventCommandResult ExecuteCommand()
        {

            var result = new EventCommandResult();

            var dc = new DocumentGenerationController();

            IEnumerable<byte[]> theDocs = dc.GetAllFilesInADirectory(BundleDir);

            //attempt to pull all docs
            try
            {
                

                foreach (var doc in theDocs)
                {

                    var tempPdf = dc.ConvertWordBytesToPDFBytes(doc);



                    this.Event.Documents.Add(new Document
                    {
                        DocumentBLOB = tempPdf
                    });
                }
            }
            catch (Exception ex)
            {
                //generate email for error/write an error - provide a default document
                throw ex;
            }


            int i = 1;

            foreach (var doc in theDocs)
            {
                if (booking != null)
                {
                    this.Event.Documents.ElementAt(i-1).DocumentName = "C" + customer.CustomerID + "B" + booking.BookingID +
                                                                       DateTime.Now.ToLongDateString() +
                                                                       documentType.ToString() + i.ToString();
                }
                if (bes != null)
                {
                    this.Event.Documents.ElementAt(i - 1).DocumentName = "C" + customer.CustomerID + "BE" + bes.BookingExtraSelectionID +
                                                                         DateTime.Now.ToLongDateString() +
                                                                         documentType.ToString()  +i.ToString();
                }
                i++;
            }
          


            if (!this.Event.Documents.Equals(null) && this.Event.Documents.Any())
            {
                result.ResultCode = 200;
                result.CommandExecutedInfo = "DocumentBundleCommand";
                result.ResultMessage = "OK";
            }
            else
            {
                result.ResultCode = 800;
                result.CommandExecutedInfo = "DocumentBundleCommand";
                result.ResultMessage = "Fail";
            }



            return result;

        }
    }
}
