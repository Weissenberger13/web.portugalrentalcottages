using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.Entity;

namespace BootstrapVillas.Models
{
    public partial class EntityCommonField
    {

        public static List<EntityCommonField> GetAllCommonFields()
        {
            using (PortugalVillasContext _db = new PortugalVillasContext())
            {
              List<EntityCommonField> theList = _db.EntityCommonFields.ToList();

              return theList;
               
            }

        }


    }
}