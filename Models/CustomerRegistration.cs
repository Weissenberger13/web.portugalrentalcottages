using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Models
{

    //requied fields = Title, First, Last, DOB, EmailAddress, Password
    [NotMapped]
    public class CustomerRegistration : Customer
    {       
         public string Password { get; set; } 
    }
}