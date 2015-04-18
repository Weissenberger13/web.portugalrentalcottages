using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;
using BootstrapVillas.Content.Interfaces;

namespace BootstrapVillas.Content.Classes
{
    public class CustomerSet : ICustomerSet
    {


        public CustomerSet(Customer aCustomer)
        {
            this.Id = aCustomer.CustomerID;
            this.aCustomer = aCustomer;
            this.aCustomersBankDetails = CustomerBankDetail.GetCustomerBankDetailFromCustomer(aCustomer);
            this.theEventsChainToComplete = new EventProvider(this);

        }


        public long Id {get; set;}
       



        public Customer aCustomer
        { get; set; }

        public CustomerBankDetail aCustomersBankDetails
        { get; set; }


        public bool SetCustomer(Customer aCustomer)
        {
         
                this.Id = aCustomer.CustomerID;
                this.aCustomer = aCustomer;
                return true;                                 
        }

        public Customer GetCustomer()
        {
            return this.aCustomer;
        }

        public bool SetBankDetail(CustomerBankDetail aBankDetail)
        {
            this.aCustomersBankDetails = aBankDetail;
            return true;
        }

        public CustomerBankDetail GetBankDetail()
        {
            return this.aCustomersBankDetails;
        }

        public long GetCustomerID()
        {
            if (this.aCustomer.CustomerID > 0)

            { return this.aCustomer.CustomerID; }

            else return -1;
        }

    
        public IEventProvider theEventsChainToComplete
        { get; set; }
    }
}