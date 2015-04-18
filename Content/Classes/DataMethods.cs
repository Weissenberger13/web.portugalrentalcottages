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
using BootstrapVillas.Data.SQLCommands;
using BootstrapVillas.Data.DataSets;
using System.Configuration;
using System.Web.Hosting;


namespace BootstrapVillas.Content.Classes
{
    public class PRCDocumentData
    {

        /*
        protected static DataTable RenameDataTableColumn(DataTable theTable, string oldColName, string newColName)
        {
        // return   theTable.Columns[oldColName].ColumnName = newColName;

        }
        */
         
        public enum PRCReturnDataTableWrapperTypes { 
            CustomerByCustomerID, 
            CustomerBankDetailByCustomerID, 
            BookingByBookingID,
            BookingParticipantByBookingID,
            BookingExtraSelectionByBookingExtraSelectionID, 
            BookingExtraSelectionByCustomerID,
            BookingExtraSelectionByBookingID,
            BookingExtraParticipantByBookingExtraParticipantID,
            PropertyByPropertyID,
            PropertyRegionByPropertyRegionID,
            BookingExtraParticipantByBookingExtraSelectionID,
            StandardPRCInformation,
            BookingExtraByBookingExtraID,
            StandardPropertyOwner,
            StandardBookingParentContainer,
            BookingExtraAttributesByBookingExtraID,
                 PropertyTownByPropertyTownID
            

         }

        //datatables
        DataTable BookingExtraSelectionDataTable = new DataTable();
        DataTable BookingDataTable = new DataTable();
        DataTable NullResults;

        private static ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["PortugalVillasContext"];
                
        //main method - pass an enum so it knows which query to call
        public DataTable GetPRCDocumentData(PRCReturnDataTableWrapperTypes aType, long primaryParamater)
        {
            
            //assign to this connection 
            using (SqlConnection theConn = new SqlConnection()) {

                //set up the connection
                string connectionString = connection.ConnectionString;
                theConn.ConnectionString = connectionString;

                //depending on which type it is, call all the necessary Commands/populate the tables
                using (SqlDataAdapter anAdaptor = new SqlDataAdapter())
                {
                    //create a command to hold whichever SQLCommand we need
                    PVillaSQLCommands aSQLCommand = new PVillaSQLCommands();
                                        
                    DataSet aDataSet = new DataSet();

                    //check which command has been called, assign it, then pull back our data
                    switch(aType)
                    {
                        case PRCReturnDataTableWrapperTypes.CustomerByCustomerID:  
                            aSQLCommand.AddStandardCustomerQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardCustomerSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@CustomerID"].Value = primaryParamater;
                            DataTable CustomerDataTable = new DataTable();
                            anAdaptor.Fill(CustomerDataTable);

                            //add a date for display
                            System.Data.DataColumn col = new System.Data.DataColumn("CurrentDate", typeof(string));
                            col.DefaultValue = DateTime.Now.ToString("dd/MM/yyyy");
                            CustomerDataTable.Columns.Add(col);

                            return CustomerDataTable;
                       
                        case PRCReturnDataTableWrapperTypes.CustomerBankDetailByCustomerID:
                            aSQLCommand.AddStandardCustomerBankDetailQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardCustomerBankDetailCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@CustomerID"].Value = primaryParamater;
                            DataTable CustomerBankDetailDataTable = new DataTable();
                            anAdaptor.Fill(CustomerBankDetailDataTable);
                            return CustomerBankDetailDataTable;
                          
                      
                        case PRCReturnDataTableWrapperTypes.BookingByBookingID:
                            aSQLCommand.AddStandardBookingQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardBookingSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingID"].Value = primaryParamater;
                            anAdaptor.Fill(BookingDataTable);
                            //change column name
                            BookingDataTable.Columns["StartDate"].ColumnName = "BookingStartDate";
                            BookingDataTable.Columns["EndDate"].ColumnName = "BookingEndDate";

                           
                            
                            return BookingDataTable;
                                                 
                        case PRCReturnDataTableWrapperTypes.BookingParticipantByBookingID:
                            aSQLCommand.AddStandardStandardBookingParticipantQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardBookingParticipantSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingID"].Value = primaryParamater;
                            DataTable BookingParticipantDataTable = new DataTable();
                            anAdaptor.Fill(BookingParticipantDataTable);
                            return BookingParticipantDataTable;


                        case PRCReturnDataTableWrapperTypes.BookingExtraParticipantByBookingExtraSelectionID:
                            aSQLCommand.AddBookingExtraParticipantByBookingExtraSelectionIDQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.BookingExtraParticipantByBookingExtraSelectionIDSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingExtraSelectionID"].Value = primaryParamater;
                            anAdaptor.Fill(BookingExtraSelectionDataTable);
                            return BookingExtraSelectionDataTable;
                            
                   
                        case PRCReturnDataTableWrapperTypes.BookingExtraSelectionByCustomerID:
                            aSQLCommand.AddBookingExtraSelectionByCustomerIDQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.BookingExtraSelectionByCustomerIDSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@CustomerID"].Value = primaryParamater;
                            anAdaptor.Fill(BookingExtraSelectionDataTable);
                            return BookingExtraSelectionDataTable;
                      
                        case PRCReturnDataTableWrapperTypes.BookingExtraSelectionByBookingID:
                            aSQLCommand.AddBookingExtraSelectionByBookingIDQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.BookingExtraSelectionByBookingIDSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingID"].Value = primaryParamater;
                            anAdaptor.Fill(BookingExtraSelectionDataTable);
                            return BookingExtraSelectionDataTable;


                        case PRCReturnDataTableWrapperTypes.BookingExtraSelectionByBookingExtraSelectionID:
                            aSQLCommand.AddStandardBookingExtraSelectionQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardBookingExtraSelectionSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingExtraSelectionID"].Value = primaryParamater;
                            anAdaptor.Fill(BookingExtraSelectionDataTable);
                            return BookingExtraSelectionDataTable;
                            

                    
                        case PRCReturnDataTableWrapperTypes.BookingExtraParticipantByBookingExtraParticipantID:
                            aSQLCommand.AddStandardBookingExtraParticipantQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardBookingExtraParticipantSQLCommand;
                            DataTable BookingExtraParticipantDataTable = new DataTable();
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingExtraParticipantID"].Value = primaryParamater;
                            anAdaptor.Fill(BookingExtraParticipantDataTable);
                            return BookingExtraParticipantDataTable;
                            
                        case PRCReturnDataTableWrapperTypes.PropertyByPropertyID:
                            aSQLCommand.AddStandardPropertyQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardPropertySQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@PropertyID"].Value = primaryParamater;
                            DataTable PropertyDataTable = new DataTable();
                            anAdaptor.Fill(PropertyDataTable);
                            return PropertyDataTable;
                            
                            
                        case PRCReturnDataTableWrapperTypes.StandardPRCInformation:
                            aSQLCommand.AddStandardPRCInformationQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardPRCInformationSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@PRCInformationID"].Value = 1;
                            DataTable PRCInformationDataTable = new DataTable();
                            anAdaptor.Fill(PRCInformationDataTable);
                            return PRCInformationDataTable;
                           
                        case PRCReturnDataTableWrapperTypes.PropertyRegionByPropertyRegionID:
                            aSQLCommand.AddStandardPropertyRegionQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardPropertyRegionSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@PropertyRegionID"].Value = primaryParamater;
                            DataTable PropertyRegionDataTable = new DataTable();
                            anAdaptor.Fill(PropertyRegionDataTable);
                            return PropertyRegionDataTable;

                        case PRCReturnDataTableWrapperTypes.PropertyTownByPropertyTownID:
                            aSQLCommand.AddStandardPropertyTownQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardPropertyTownSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@PropertyTownID"].Value = primaryParamater;
                            DataTable PropertyTownDataTable = new DataTable();
                            anAdaptor.Fill(PropertyTownDataTable);
                            return PropertyTownDataTable;



                        case PRCReturnDataTableWrapperTypes.BookingExtraByBookingExtraID:
                            aSQLCommand.AddStandardBookingExtraQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardBookingExtraSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingExtraID"].Value = primaryParamater;
                            DataTable BookingExtraDataTable = new DataTable();
                            anAdaptor.Fill(BookingExtraDataTable);

                            BookingExtraDataTable.Columns["LegacyReference"].ColumnName = "ExtraLegacyReference";
                            return BookingExtraDataTable;

                        case PRCReturnDataTableWrapperTypes.StandardPropertyOwner:
                            aSQLCommand.AddStandardPropertyOwnerQueryParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardPropertyOwnerSQLCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@PropertyID"].Value = primaryParamater;
                            DataTable PropertyOwnerDataTable = new DataTable();
                            anAdaptor.Fill(PropertyOwnerDataTable);
                            return PropertyOwnerDataTable;
                        //added 06 -05 -2013 by ND


                        case PRCReturnDataTableWrapperTypes.StandardBookingParentContainer:
                            aSQLCommand.AddStandardBookingParentContainerParams();
                            anAdaptor.SelectCommand = aSQLCommand.StandardBookingParentContainerCommand;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingParentContainerID"].Value = primaryParamater;
                            DataTable BookingParentContainerTable = new DataTable();
                            anAdaptor.Fill(BookingParentContainerTable);
                            //change column name
                            return BookingParentContainerTable;



                        case PRCReturnDataTableWrapperTypes.BookingExtraAttributesByBookingExtraID:
                            aSQLCommand.AddBookingExtraAttributesByBookingExtraIDParams();
                            anAdaptor.SelectCommand = aSQLCommand.BookingExtraAttributesByBookingExtraID;
                            anAdaptor.SelectCommand.Connection = theConn;
                            anAdaptor.SelectCommand.Parameters["@BookingExtraID"].Value = primaryParamater;
                            DataTable BookingExtraAttributesTable = new DataTable();
                            anAdaptor.Fill(BookingExtraAttributesTable);
                            //change column name
                            return BookingExtraAttributesTable;                            

                    }

                    return NullResults;
               

                    
                }

                 
                           
            
            }

           

        //end method
        }

    }
}