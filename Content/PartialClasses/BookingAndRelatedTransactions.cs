using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes
{
    public class BookingAndRelatedTransactions
    {
        public Booking booking { get; set; }
        public List<AccountTransaction> transactions { get; set; }

        public BookingAndRelatedTransactions()
        {
                
        }
    }
}
