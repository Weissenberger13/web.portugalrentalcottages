using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using Aspose.Email.Mail;
using BootstrapVillas.Content.EmailTemplates;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.ProcessEvents
{
    [NotMapped]
    public class EventCommandSendEmail : EventCommand
    {
        private int EmailTemplateId { get; set; }
        Customer customer { get; set; }
        Booking booking { get; set; }
        public BookingExtraSelection bes { get; set; }       


        /// <summary>
        /// There is only one template pattern at the moment - Standard template
        /// </summary>
        /// <param name="anEvent"></param>
        /// <param name="customer"></param>
        /// <param name="documentType"></param>
        /// <param name="booking"></param>
        /// <param name="bes"></param>
        public EventCommandSendEmail(Event anEvent, int EmailTemplateId, Customer customer, Booking booking = null, BookingExtraSelection bes = null)
        {
            this.Event = anEvent;
            this.customer = customer;           
            this.booking = booking;
            this.bes = bes;
            this.EmailTemplateId = EmailTemplateId;

        }

        public override EventCommandResult ExecuteCommand()
        {            
            var result = new EventCommandResult();

            var template = new StandardEmailTemplate(customer.EmailAddress, this.EmailTemplateId, customer, booking, bes);


            if (this.Event.Documents.Equals(null))
            {
                this.Event.Documents = new Collection<Document>();
                this.Event.Documents.Add(new Document());
                
            }

            if (this.Event.Documents.Count > 0)
            {
                foreach (var doc in this.Event.Documents)
                {
                    Stream docStream = new MemoryStream(doc.DocumentBLOB);
                    template.theAsposeMessage.AddAttachment(new Attachment(docStream, doc.DocumentName+".pdf"));
                }
            }                        

            if(template.SendEmail())                
                {
                    result.ResultCode = 200;
                    result.CommandExecutedInfo = "EmailOutCommand";
                    result.ResultMessage = "OK";
                }
                else
                {
                    result.ResultCode = 800;
                    result.CommandExecutedInfo = "EmailOutCommand";
                    result.ResultMessage = "Fail";
                }

            return result;
        }

    }
}