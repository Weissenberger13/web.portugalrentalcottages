using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class StoredSession
    {
        public long StoredSessionID { get; set; }
        public string NETSessionID { get; set; }
        public DateTime WhenCreated { get; set; }
    }
}
