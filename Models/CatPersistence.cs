using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class CatPersistence
    {
        public Nullable<long> CartPersistenceID { get; set; }
        public int VillaObjectID { get; set; }
        public byte[] Object { get; set; }
        public string SessionID { get; set; }
        public string VilllaStageProcessID { get; set; }
    }
}
