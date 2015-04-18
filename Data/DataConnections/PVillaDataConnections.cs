using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Aspose.Words;
using Aspose.Email;
using BootstrapVillas.Content.Classes;
using BootstrapVillas.Models;
using Aspose.Email.Mail;
using Aspose.Email.Imap;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using BootstrapVillas.Data.DataConnections;
using BootstrapVillas.Data.DataAdapters;
using BootstrapVillas.Data.QueriesSQL;
using System.Configuration;
using System.Web.Hosting;


namespace BootstrapVillas.Data.DataConnections
{
    public class PVillaDataConnection
    {
        //top level object for connection string

        private static ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["PortugalVillasContext"];

        
        public SqlConnection conn = new SqlConnection();

            public PVillaDataConnection()
            {
                //sets up a standard connection
                 
                string connectionString = connection.ConnectionString;
                conn.ConnectionString = connectionString;
                 
            }


            public bool OpenVillaDataConnection()
            {
                try{
                   conn.Open();
                   return true;
                   }
                catch(Exception OpenVillaDataConnectionEx)
                {
                    return false;
                    throw;
               
                }

            }

            public bool CloseVillaDataConnection()
            {
                try{
                    conn.Close();
                    return true;
                }
                catch(Exception CloseVillaDataConnectionEx)
                {
                    return false;   
                    throw;        
                }

            }


        }
 


        
      }
        
    