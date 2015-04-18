using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Content.Interfaces;
using BootstrapVillas.Models;
using System.Data.Entity;


namespace BootstrapVillas.Models
{
    public partial class Customer //: IContactable
    {

        //IContactable Wrappers
    /*    public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmailAddress { get; set; }*/


        /////////////////////
        //static methods
        /////////////////////
        /// 

        public static Customer MapCustomerRegistrationToNewCustomer(CustomerRegistration theCustomerRegistration)
        {
            try
            {
                Customer theCustomer = new Customer
                {
                    Title = theCustomerRegistration.Title,
                    Address1 = theCustomerRegistration.Address1,
                    Address2 = theCustomerRegistration.Address2,
                    AltEmailAddress = theCustomerRegistration.AltEmailAddress,
                    AltTelephone = theCustomerRegistration.AltTelephone,
                    CompanyTelephone = theCustomerRegistration.CompanyTelephone,
                    County = theCustomerRegistration.County,
                    Country = theCustomerRegistration.Country,
                    CreationDate = theCustomerRegistration.CreationDate,
                    DOB = theCustomerRegistration.DOB,
                    DayTimeTelephone = theCustomerRegistration.DayTimeTelephone,
                    HomeTelephone = theCustomerRegistration.HomeTelephone,
                    EmailAddress = theCustomerRegistration.EmailAddress,
                    FirstName = theCustomerRegistration.FirstName,
                    LastName = theCustomerRegistration.LastName,
                    MiddleName = theCustomerRegistration.MiddleName,
                    Town = theCustomerRegistration.Town,
                    Postcode = theCustomerRegistration.Postcode,
                    MobileTelephone = theCustomerRegistration.MobileTelephone,
                    PreferredCurrency = theCustomerRegistration.PreferredCurrency,
                    PreferredCurrencySymbol = theCustomerRegistration.PreferredCurrencySymbol,
                    Test = theCustomerRegistration.Test

                };


                return theCustomer;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }

        protected static List<Customer> GetCustomerByFirstName(string firstName)
        {
            //may return a list of customers with same first name

            PortugalVillasContext _db = new PortugalVillasContext();
            List<Customer> customerList;

            customerList = (from customers in _db.Customers
                            where customers.FirstName == firstName
                            select customers).ToList();

            return customerList;
        }

        protected static List<Customer> GetCustomerByLastName(string lastName)
        {
            //may return a list of customers with same last name

            PortugalVillasContext _db = new PortugalVillasContext();
            List<Customer> customerList;

            customerList = (from customers in _db.Customers
                            where customers.FirstName == lastName
                            select customers).ToList();

            return customerList;
        }



        public static Customer GetCustomerByEmailAddress(string emailAddress)
        {            

            PortugalVillasContext _db = new PortugalVillasContext();
            Customer customer;

            customer = (from customers in _db.Customers
                        where customers.EmailAddress.Trim().ToLower() == emailAddress.Trim().ToLower()
                select customers).FirstOrDefault();

            return customer;
        }

        protected static List<Customer> GetCustomerByPostcode(string postcode)
        {
            //may return a list of customers with same last name

            PortugalVillasContext _db = new PortugalVillasContext();
            List<Customer> customerList;

            //Clean up postcode within LINQ
            
            customerList = (from customers in _db.Customers
                            where customers.FirstName.ToLower().TrimEnd().TrimStart().Replace(" ", "") == postcode.ToLower().TrimEnd().TrimStart().Replace(" ", "")
                            select customers).ToList();

            return customerList;
        }

        public static Customer GetSingleCustomer(long? CustomerID)
        {
            PortugalVillasContext _db = new PortugalVillasContext();

            var aCustomer = (_db.Customers
                                     .Where(x => x.CustomerID == CustomerID).FirstOrDefault());
                        
            return aCustomer;
        }


    }
}