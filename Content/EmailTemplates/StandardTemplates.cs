using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aspose.Email;
using BootstrapVillas.Models;
using Aspose.Email.Mail;
using System.Configuration;

namespace BootstrapVillas.Content.EmailTemplates
{
    /// <summary>
    /// Sends out emails to customers / clients
    /// </summary>
    public class StandardEmailTemplate
    {

        public MailMessage theAsposeMessage;
        SmtpClient theSMTPClient = new SmtpClient();



        public string FromEmailAddress = ConfigurationManager.AppSettings["EMAIL_Username"];
        public string LoginEmailAddress = ConfigurationManager.AppSettings["EMAIL_Username"];
        public string EmailPassword = ConfigurationManager.AppSettings["EMAIL_Password"];

        public string ToEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EmailAttachment { get; set; }

        /// <summary>
        /// Sends an email for 1*1 bookings or extrabookings
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="booking"></param>
        /// <param name="bookingExtraSelection"></param>
        public StandardEmailTemplate(string toEmailAddress, int EmailTemplateID, Customer customer, Booking booking = null, BookingExtraSelection bookingExtraSelection = null)
        {

            EmailTemplate theTemplate;
            using(PortugalVillasContext db = new PortugalVillasContext())
            {                
                theTemplate = db.EmailTemplates.Find(EmailTemplateID);
            }
            theAsposeMessage = new MailMessage();
            Property property = new Property();
            BookingExtra bookingExtra = new BookingExtra();

            EmailOutType theMainBody = new EmailOutType();

            //map all passed in object fields
            bool bookingToggle = false;
            bool bookingExtraToggle = false;
            bool combinedServicesToggle = false;


            //gets necessary

            //assign body
            if (booking == null)
            {
                bookingExtraToggle = true;
                bookingExtra = BookingExtra.GetBookingExtraByID(bookingExtraSelection.BookingExtraID);
                //theMainBody = new EmailOutType(EmailOutType.EmailOutEmailType.BookingExtraSelectionConfirm);
            }
            else if (booking != null && bookingExtraSelection == null)
            {
                bookingToggle = true;
                long propertyID = (long)booking.PropertyID;
                property = Property.GetPropertyByID(propertyID);
                //theMainBody = new EmailOutType(EmailOutType.EmailOutEmailType.BookingConfirm);//
            }
            else if (booking != null && bookingExtraSelection != null)
            {
                long propertyID = (long)booking.PropertyID;
                property = Property.GetPropertyByID(propertyID);
                bookingExtra = BookingExtra.GetBookingExtraByID(bookingExtraSelection.BookingExtraID);
                combinedServicesToggle = true;
                //theMainBody = new EmailOutType(EmailOutType.EmailOutEmailType.BookingAndBookingExtraSelectionConfirm);
            }

            
          

         


            //customer
            var customerFullName = customer.FirstName + " " + customer.LastName;

            //bookings and extras

            var prcRef = booking != null ? property.LegacyReference : bookingExtra.LegacyReference;

            string bookingPropertyOrSelectionName = booking != null ? property.PropertyName : bookingExtra.BookingExtraName;
            string bookingStartDate = booking != null ? Convert.ToDateTime(booking.StartDate.ToString()).ToShortDateString() : Convert.ToDateTime(bookingExtraSelection.ExtraRentalDate.ToString()).ToShortDateString();
            string bookingEndDate = booking != null ? Convert.ToDateTime(booking.EndDate.ToString()).ToShortDateString() : new DateTime(1901, 01, 01).ToShortDateString();
            string numberOfNights = booking != null ? booking.NumberOfNights.ToString() : "0";


            theAsposeMessage.To = customer.EmailAddress;
            theAsposeMessage.From = "phrvillas@gmail.com";

            theSMTPClient.Host = "smtp.gmail.com";
            theSMTPClient.Username = LoginEmailAddress;
            theSMTPClient.Password = EmailPassword;
            theSMTPClient.Port = 587;
            theSMTPClient.EnableSsl = true;
            theSMTPClient.SecurityMode = SmtpSslSecurityMode.Explicit;


            //set some email body   
            //set the body
            //subject
            //           
            theAsposeMessage.Subject = theTemplate.EmailSubject;
            theAsposeMessage.HtmlBody = theTemplate.EmailTemplateBodyHTML;
             
           //get correct email according to emailEnumID

            //string merges
            if (bookingToggle == true)
            {
                var temp = String.Format(theAsposeMessage.HtmlBody, customerFullName, property.LegacyReference, bookingStartDate, bookingEndDate, numberOfNights);              
            }

            else if (bookingExtraToggle == true)
            {
                var temp = String.Format(theAsposeMessage.HtmlBody, customerFullName, prcRef, bookingExtraSelection.BookingExtraSelectionID);
                
            }
            else if (combinedServicesToggle == true)
            {
                var temp = String.Format(theAsposeMessage.HtmlBody, customerFullName, booking.BookingID);
              
            }




            //set the footer
            theAsposeMessage.HtmlBody += new EmailOutType(EmailOutType.EmailOutEmailType.Footer).body;


        }
        //put in the DB


        public bool SendEmail()
        {
            this.theSMTPClient.Send(this.theAsposeMessage);
            return true;
        }


        public string GetHTMLMessageBody()
        {
            return this.theAsposeMessage.HtmlBody;

        }

        public StandardEmailTemplate(string toEmailAddress)
        {
            theAsposeMessage = new MailMessage();
            theAsposeMessage.To = toEmailAddress;
            theAsposeMessage.From = ConfigurationManager.AppSettings["EMAIL_Username"];

            theSMTPClient.Host = ConfigurationManager.AppSettings["EMAIL_Host"];
            theSMTPClient.Username = LoginEmailAddress;
            theSMTPClient.Password = EmailPassword;
            theSMTPClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EMAIL_Port"]);
            theSMTPClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EMAIL_EnableSsl"]);
            theSMTPClient.SecurityMode = (SmtpSslSecurityMode)Enum.Parse(typeof(SmtpSslSecurityMode), ConfigurationManager.AppSettings["EMAIL_Host"].ToString());

        }

    }





    public class EmailOutType
    {



        public enum EmailOutEmailType
        {
            Header = 0,
            Footer = 1,
            BookingConfirmationVoucher = 2,
            BookingExtraSelectionConfirm = 3,
            BookingAndBookingExtraSelectionConfirm = 4,
            FinalBookingInstructions = 5,
            StandardErrorToCustomer = 6,
            BookingRequestFormUK = 7,
            BookingRequestFormEU = 8,            
            BookingFinalPaymentReciept = 10,
            BookingFinalInstructions = 11,
            BookingBreakageDepositRefundNoDamage = 12,
            BookingBreakageDepositRefundDamage = 13

        }

        public string subject = "";
        public string body = "";

        public EmailOutType()
        {
        }

        public EmailOutType(EmailOutEmailType partType)
        {
            try
            {
                switch (partType)
                {

                    case EmailOutEmailType.BookingRequestFormUK:
                        {
                            subject = "Portugal Rental Cottages: Your PRC Booking Confirmation Email - Please read";
                            body = @"                           
                            Dear {0}, <br/><br/>
                            Thanks for choosing to book property {1}. Please see attached, the Booking Request form {2} which you need to complete and return to me via email as soon as possible. The form provides our UK Sterling bank details, where you can transfer the initial deposit. <br/><br/>
                            For added security, just prior to doing the bank transfer,  I would ask that you please give me a quick call on the number - 07906 137718 or text me to say you are transferring. I will answer the phone or acknowledge with an SMS. When funds are received, you will be sent the remaining documentation via email confirming your booking.<br/><br/>
                            This is the phone number that is published on the www.portugalrentalcottages.com site and the only number we would ever use to communicate with our customers. If anyone does try to make contact with you using a different number, then, please know, this is not from us. Do not do the bank transfer until you have spoken with me or received an SMS from this phone number. Be safe & be secure.<br/><br/>";

                        }
                        break;
                    case EmailOutEmailType.BookingRequestFormEU:
                        {
                            subject = "Portugal Rental Cottages: Your PRC Booking Confirmation Email - Please read";
                            body = @"                           
                            Dear {0}, <br/><br/>
                            Thanks for choosing to book property {1}. Please see attached, the Booking Request form {2} which you need to complete and return to me via email as soon as possible. The form provides our UK Sterling bank details, where you can transfer the initial deposit. <br/><br/>
                            For added security, just prior to doing the bank transfer,  I would ask that you please give me a quick call on the number - 07906 137718 or text me to say you are transferring. I will answer the phone or acknowledge with an SMS. When funds are received, you will be sent the remaining documentation via email confirming your booking.<br/><br/>
                            This is the phone number that is published on the www.portugalrentalcottages.com site and the only number we would ever use to communicate with our customers. If anyone does try to make contact with you using a different number, then, please know, this is not from us. Do not do the bank transfer until you have spoken with me or received an SMS from this phone number. Be safe & be secure.<br/><br/>";
                        }
                        break;
                    case EmailOutEmailType.StandardErrorToCustomer:
                        {
                            subject = "Portugal Rental Cottages: Your PRC Booking Confirmation Email - Please read";
                            body = @"                           
                            Dear Customer, <br/><br/>
                            Pricing for this property is currently unavailable for the period you are interested in. Please contact us by email on phrvillas@gmail.com and we will get back to you with information as soon as possible.";
                        }
                        break;


                    case EmailOutEmailType.BookingConfirmationVoucher:
                        {
                            subject = "Portugal Rental Cottages: Your PRC Booking Confirmation Email - Please read";
                            body = @"                           
                            Dear {0}, <br/><br/>
                            Your booking for {1} is now confirmed. Thanks. Please see attached the Booking Confirmation Voucher {2} and other relevant documentation that completes the booking process. Kindly sign and return one copy of the voucher for our records. 
                            Your second and final payment, along with the breakage deposit is due on 18 May 2015. For added security, just prior to doing the bank transfer, please call OR Text me on the number - 07906 137718 to say you are transferring. I will answer the phone or acknowledge with an SMS. I will confirm receipt of funds via email message.
                            The phone number above is published on the www.portugalrentalcottages.com site is the only number we would ever use to communicate with our customers. If anyone does try to make contact with you using a different number, then, please know, this is not from us. Do not do the bank transfer until you have spoken with me or received an SMS from this phone number. Be safe & be secure.";

                        }
                        break;
                    case EmailOutEmailType.BookingExtraSelectionConfirm:
                        {
                            subject = "Portugal Rental Cottages: Your PRC Booking Confirmation Email - Please read";
                            body = @"                                   
                                Dear {0}, <br/><br/>
                                            Thank you for your recent order number PRC-B-{1}. Provisional arrangements have been made for you for the services ordered, pending receipt of payment by bank transfer, using the bank details provided on the site. You will be contacted by us as soon as the funds are received in the designated bank account, with a full confirmation voucher to conclude this process.
                                            If we do not receive payment within 3 working days of this message, or we do not hear from you, then the services ordered will be duly cancelled.";
                        }
                        break;
                    case EmailOutEmailType.BookingAndBookingExtraSelectionConfirm:
                        {
                            subject = "Portugal Rental Cottages: Your PRC Booking Confirmation Email - Please read";
                            body = @"Dear {0}, <br/><br/>
                                          Thank you for your recent order number PRC-EX-{1}. A provisional booking / service arrangement has been made for you for the selected dates and services ordered, pending receipt of payment by bank transfer using the bank details provided on the site. You will be contacted by us as soon as the funds are received in the designated bank account, with a full confirmation voucher to conclude this process.
                                          If we do not receive payment within 3 working days of this message, or we do not hear from you, then the provisional booking and services ordered will be duly cancelled.
                                            ";
                        }
                        break;

                    case EmailOutEmailType.FinalBookingInstructions:
                        {
                            subject = "Portugal Rental Cottages: Your Booking Final Confirmation Documents - Please read";
                            body = @"Dear {0}, <br/><br/>
                                          Please see attached, final instructions for property {1} which includes information on key retrieval, emergency numbers, FAQ, security entry codes and directions from Lisbon airport with GPS coordinates. Also attached is information about local attractions, beaches, amenities, restaurant guide, and the name and contact details of a local caretaker should you have any issues when on vacation.<br/><br/>
                                          You are advised to stay in touch with me for all your questions until the point you actually arrive at the property; after which, you should contact the local caretaker if you need advice or assistance.<br/><br/>
                                          Have a safe trip to Portugal and a great holiday!<br/><br/>";

                        }
                        break;
                    case EmailOutEmailType.BookingBreakageDepositRefundNoDamage:
                        {
                            subject = "Portugal Rental Cottages: Your Booking Final Confirmation Documents - Please read";
                            body = @"Dear {0}, <br/><br/>
                                   This is to confirm that we have today refunded your breakage deposit amount to the bank details you provided during the booking process. There was no damage for breakage reported during your stay, so we have returned your deposit in full.  <br/><br/>";

                        }
                        break;
                    case EmailOutEmailType.BookingBreakageDepositRefundDamage:
                        {
                            subject = "Portugal Rental Cottages: Your Booking Final Confirmation Documents - Please read";
                            body = @"Dear {0}, <br/><br/>
                                   We regret to inform you that there was some damage reported during your stay, by our local property management services department, and therefore we will need to make appropriate deductions from your deposit to cover the cost of replacement or repair. Once this is done, we will refund the remaining amount to you by bank transfer using the bank details you provided during the booking process. <br/><br/>";

                        }
                        break;
                    case EmailOutEmailType.BookingFinalPaymentReciept:
                        {
                            subject = "Portugal Rental Cottages: Your Booking Final Confirmation Documents - Please read";
                            body = @"Dear {0}, <br/><br/>
                                   We hereby confirm receipt of your second and final rental payment of {1} {2}{3}/- via bank transfer on 19 May 2015. Thanks. I further confirm that we have received the breakage deposit amount of GB £300/- in the same transaction. The breakage deposit will be refunded to you within 10 working days of your departure from the property.<br/><br/>
                                   You will receive final instructions with all necessary information, two weeks prior to your arrival in Portugal.";


                        }
                        break;


                    case EmailOutEmailType.Footer:
                        {
                            subject = "";
                            body = @"    Best Regards
                                                <br/><br/>
                                                 Ben Wilson   
                                                <br/>
                                                 <p style=""font-family:Verdanafont-size:8pt;font-style:italic;"">Apt 194 2500-Caldas da Rainha, Portugal<p>
                                                 <p style=""font-family:Verdanafont-size:8pt;font-style:italic;"">Telephone: Office: +351 262 824 221</p>

                                                 <p style=""font-family:Verdanafont-size:8pt;font-style:italic;"">Telephone Mobile: +44 7906 137718</p>

                                                 <p style=""font-family:Verdanafont-size:8pt;font-style:italic;"">Telephone Mobile: +351 93 466 4982</p>

                                                 <p style=""font-family:Verdanafont-size:8pt;font-style:italic;"">info@portugalrentalcottages.com</p>

                                                 <a href=""fabholsportugal@gmail.com"" style=""font-family:Verdanafont-size:8pt;font-style:italic;"">fabholsportugal@gmail.com</p>

                                                 <a href=""info@portugalrentalcottages.com""  ""style=""font-family:Verdanafont-size:8pt;font-style:italic;"">www.portugalrentalcottages.com</a>";

                        }
                        break;

                    case EmailOutEmailType.Header:
                        {
                            subject = "";
                            body = @" <div style=""vertical-align:left; text-align:left""><img  src=""https://dl.dropboxusercontent.com/u/85895568/EmailLogoStandard.png""></div>";
                        }
                        break;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}










