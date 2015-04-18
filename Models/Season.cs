using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class Season
    {
        public int SeasonID { get; set; }
        public string SeasonName { get; set; }
        public Nullable<decimal> SeasonCommissionPercent { get; set; }
    }
}
