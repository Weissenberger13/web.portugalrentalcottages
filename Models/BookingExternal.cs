using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Ajax.Utilities;

namespace BootstrapVillas.Models
{
    public partial class BookingExternal
    {
        [Key]
        public long BookingExternalID { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public long PropertyID { get; set; }
        public string Notes { get; set; }
        public virtual Property Property { get; set; }

       
       
        public int SystemId { get; set; }
        [ForeignKey("SystemId")]
        [InverseProperty("BookingExternals")]
        public virtual System System { get; set; }
    }
}
