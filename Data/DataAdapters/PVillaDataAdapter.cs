using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Web.Hosting;

namespace BootstrapVillas.Data.DataAdapters
{
    public class PVillaDataAdapter
    {
        public class StandardVillaDataAdapter
        {
            public static SqlDataAdapter da = new SqlDataAdapter();

            public StandardVillaDataAdapter(SqlCommand theSelectCommand)
            {
                //assign the relevant query to the DataAdapter
                da.SelectCommand = theSelectCommand;

            }


            


        }

    }
}