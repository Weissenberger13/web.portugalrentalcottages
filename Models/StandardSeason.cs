using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class StandardSeason
    {
        public int StandardSeasonID { get; set; }
        public string SeasonName { get; set; }
        public System.DateTime SeasonStartDate { get; set; }
        public System.DateTime SeasonEndDate { get; set; }
    }
}
