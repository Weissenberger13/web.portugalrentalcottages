using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aspose.Words;
using Aspose.Email;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Interfaces;
using BootstrapVillas.Models;
using Aspose.Email.Mail;
using Aspose.Email.Imap;


namespace BootstrapVillas.Content.Classes
{
    //this class is basically a wrapper for the aspose.email class 
    public class MaintainanceMailer :IMailService
    {
        //define some props
        //the email message
        public MailMessage theAsposeMessage;
        SmtpClient theSMTPClient = new SmtpClient();


        //wrapper 
        public string FromEmailAddress = @"phrvillas@gmail.com";
        public string LoginEmailAddress = @"phrvillas@gmail.com";
        public string EmailPassword = @"Diagonal23";

        public string ToEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EmailAttachment { get; set; }


   
        public MaintainanceMailer()
        {
            //set some defaults
            using (var db = new PortugalVillasContext())
            {
                var toAddress = db.PRCInformations.First().PRCNotificationEmailAddress;

                ToEmailAddress = toAddress;
                EmailSubject = @"PRC Maintainence Emailer";


                //create the message
                theAsposeMessage = new MailMessage();
                theAsposeMessage.From = FromEmailAddress;
                theAsposeMessage.To = ToEmailAddress;
                //  theAsposeMessage.To.Add("cenarioatlantico@live.co.uk");
                //  theAsposeMessage.To.Add("Marcweissdesign@gmail.com");

                theAsposeMessage.Subject = EmailSubject;

                //set some email body


                //add the attachment
                // Attachment theMailAttachment = new Attachment(@"c:\temp\testfile.txt");
                //  theAsposeMessage.Attachments.Add(theMailAttachment);

                //set smtp vars
                theSMTPClient.Host = "smtp.gmail.com";
                theSMTPClient.Username = LoginEmailAddress;
                theSMTPClient.Password = EmailPassword;
                theSMTPClient.Port = 587;
                theSMTPClient.EnableSsl = true;
                theSMTPClient.SecurityMode = SmtpSslSecurityMode.Explicit;
            }

        }


        //define some methods
        
        //sets all vars ready to email
        public bool SetOutboundVariables()
        {

        return true;
        }

        //check everything is ready to email
        bool CheckAllAutomailerPropertiesAreValid()
        {

        return true;
        }

        //sends the actual email
        public bool SendEmail()
        {
            this.theSMTPClient.Send(this.theAsposeMessage);
            return true;
        }


        public int Mail(string emailTo, string message)
        {
            this.ToEmailAddress = emailTo;
            this.theAsposeMessage.Body = message;
            this.theSMTPClient.Send(this.theAsposeMessage);
            return 0;
        }


    }
}