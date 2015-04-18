using System;
using System.Collections.Generic;

namespace BootstrapVillas.Models
{
    public partial class PropertyEntity
    {
        public long PropertyEntityID { get; set; }
        public long PropertyEntityTypeID { get; set; }
        public long PropertyID { get; set; }
        public string PropertyEntitySummaryDescription { get; set; }
        public string PropertyEntityName { get; set; }
        public string PropertyEntityDescription1 { get; set; }
        public string PropertyEntityDescription2 { get; set; }
        public string PropertyEntityDescription3 { get; set; }
        public string PropertyEntityDescription4 { get; set; }
        public string PropertyEntityDescription5 { get; set; }
        public string PropertyEntityDescription6 { get; set; }
        public string PropertyEntityDescription7 { get; set; }
        public string PropertyEntityDescription8 { get; set; }
        public string PropertyEntityDescription9 { get; set; }
        public string PropertyEntityDescription10 { get; set; }
        public string PropertyEntityDescription11 { get; set; }
        public string PropertyEntityDescription12 { get; set; }
        public string PropertyEntityDescription13 { get; set; }
        public string PropertyEntityDescription14 { get; set; }
        public string PropertyEntityDescription15 { get; set; }
        public System.DateTime WhenCreated { get; set; }
        public byte[] WhenUpdated { get; set; }
        public virtual Property Property { get; set; }
        public virtual PropertyEntityType PropertyEntityType { get; set; }
    }
}
