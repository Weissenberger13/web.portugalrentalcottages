using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aspose.Email.Exchange.Schema;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Content.EmailTemplates;
using BootstrapVillas.Interfaces;
using BootstrapVillas.Models;
using System.IO;

namespace BootstrapVillas.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/

        public ActionResult QuickMail()
        {

            return View();

        }

        [HttpPost]
        public ActionResult QuickMail(string emailTo, string message)
        {
            SendMaintainanceMail(emailTo, message);

            return View();
        }


        public StandardEmailTemplate CreateForgottenPasswordEmailTemplate(string resetURL, string toEmailAddress, string passwordlink)
        {

             
            StandardEmailTemplate email = new StandardEmailTemplate(toEmailAddress);

            email.theAsposeMessage.Subject = "Portugal Rental Cottages: Password Reset - Please read";
            email.theAsposeMessage.HtmlBody = "<h2>Thank you for using Portugal Rental Cottages. You have requested a password reset, please click the link below to resent your password</h2>";
            email.theAsposeMessage.HtmlBody = "<a href=\"" + resetURL + "?token=" + passwordlink + "\">Click here to reset your password</a>";

            return email;
        }


        public int SendEmailToCustomer(Customer customer, int EmailTemplateID, Booking booking = null, BookingExtraSelection bes = null)
        {
            try
            {
                StandardEmailTemplate template = new StandardEmailTemplate(customer.EmailAddress, EmailTemplateID, customer, booking, bes);
                template.SendEmail();

                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int SendEmail(string emailAddress, int emailEnumID, Booking booking = null, BookingExtraSelection bes = null)
        {
            try
            {
                //StandardEmailTemplate template = new StandardEmailTemplate(emailAddress, emailEnumID, customer, booking, bes);
                //template.SendEmail();

                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        public int SendMaintainanceMail(string emailTo, string message)
        {
            try
            {
                IMailService ms = new MaintainanceMailer();
                ms.Mail("emailnadz@gmail.com", message);

                return 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


       

    }
}
