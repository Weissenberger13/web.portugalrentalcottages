using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class EventCommandResult
    {
        public long EventCommandResultID { get; set; }
        public Nullable<long> EventCommandID { get; set; }
        public string CommandExecutedInfo { get; set; }
        public Nullable<int> ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public virtual EventCommand EventCommand { get; set; }
    }
}
