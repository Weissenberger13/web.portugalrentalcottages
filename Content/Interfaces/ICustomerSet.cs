using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;
using BootstrapVillas.Content.Classes;



namespace BootstrapVillas.Content.Interfaces
{
    /// <summary>
    /// customer set interfaces - for in memory customer operations
    /// </summary>
    interface ICustomerSet : IDatasetBase
    {
       
        //props
        Customer aCustomer { get; set; }
        CustomerBankDetail aCustomersBankDetails { get; set; }

        //meths
        bool SetCustomer(Customer aCustomer);
        Customer GetCustomer();

        bool SetBankDetail(CustomerBankDetail aBankDetail);
        CustomerBankDetail GetBankDetail();
        
        long GetCustomerID();

    }
}
