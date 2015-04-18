using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;
using BootstrapVillas.Content.Interfaces;

namespace BootstrapVillas.Models
{
    public partial class CustomerBankDetail
    {

       
        /// <summary>
        /// Gets 
        /// </summary>
        public static CustomerBankDetail GetCustomerBankDetailFromCustomer(Customer aCustomer)
        {
            PortugalVillasContext db = new PortugalVillasContext();
            CustomerBankDetail theBankDetailsToReturn = db.CustomerBankDetails.Where(x => x.CustomerID == aCustomer.CustomerID).FirstOrDefault();
            return theBankDetailsToReturn;

        }


    }
}