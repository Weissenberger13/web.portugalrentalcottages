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

namespace BootstrapVillas.Data.SQLCommands
{
    public class PVillaSQLCommands
    {
        ///////////////////////////////////////////
        //sql command defs
        ///////////////////////////////////////////

          

        //CUSTOMER
        public SqlCommand StandardCustomerSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardCustomerQuery);
             
        public SqlCommand StandardCustomerBankDetailCommand
            = new SqlCommand(PVillasQueryLibrary.StandardCustomerBankDetailQuery);

        //BOOKING / PARTICIPANT
        public SqlCommand StandardBookingSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardBookingQuery);

        public SqlCommand StandardBookingParticipantSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardBookingParticipantQuery);


        //BOOKING EXTRA / PARTICIPANT
        public SqlCommand StandardBookingExtraSelectionSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardBookingExtraSelectionQuery);

        public SqlCommand BookingExtraSelectionByCustomerIDSQLCommand
            = new SqlCommand(PVillasQueryLibrary.BookingExtraSelectionByCustomerIDQuery);

        public SqlCommand BookingExtraSelectionByBookingIDSQLCommand
            = new SqlCommand(PVillasQueryLibrary.BookingExtraSelectionByBookingIDQuery);

        public SqlCommand StandardBookingExtraParticipantSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardBookingExtraParticipantQuery);

        public SqlCommand BookingExtraParticipantByBookingExtraSelectionIDSQLCommand
            = new SqlCommand(PVillasQueryLibrary.BookingExtraParticipantByBookingExtraSelectionIDQuery);
        
       
        //PROPERTY
        public SqlCommand StandardPropertySQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardPropertyQuery);
   
        
        //PRC INFORMATION
         public SqlCommand StandardPRCInformationSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardPRCInformationQuery);
     
        
        //PROPERTY REGION
         public SqlCommand StandardPropertyRegionSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardPropertyRegionQuery);

        //TOWN
         public SqlCommand StandardPropertyTownSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardPropertyTownQuery);

        //BOOKING EXTRA
          public SqlCommand StandardBookingExtraSQLCommand
            = new SqlCommand(PVillasQueryLibrary.StandardBookingExtraQuery);

          public SqlCommand BookingExtraByBookingExtraSelectionIDSQLCommand
            = new SqlCommand(PVillasQueryLibrary.BookingExtraByBookingExtraSelectionIDQuery);

        //PROPERTY OWNER
          public SqlCommand StandardPropertyOwnerSQLCommand
              = new SqlCommand(PVillasQueryLibrary.StandardPropertyOwnerQuery);


          //PARENT CONTAINER
          public SqlCommand StandardBookingParentContainerCommand
              = new SqlCommand(PVillasQueryLibrary.StandardBookingParentContainerQuery);
        //////////////////////////
        //added by ND 06 -05 -2013
        //////////////////////////
          public SqlCommand BookingExtraAttributesByBookingExtraID
                = new SqlCommand(PVillasQueryLibrary.StandardBookingAttributeQuery);
        //END

        ///////////////////////////////////////////
        //define methods to add params to SQL Commands
        ///////////////////////////////////////////

    

        //CUSTOMER
        //Customer 
        public void AddStandardCustomerQueryParams()
        {
            this.StandardCustomerSQLCommand.Parameters.Add("@CustomerID", SqlDbType.BigInt);
        }

        //Customer bank Detail
        public void AddStandardCustomerBankDetailQueryParams()
        {
            this.StandardCustomerBankDetailCommand.Parameters.Add("@CustomerID", SqlDbType.BigInt);
        }

        //BOOKING
        //Booking - takes BookingID
        public void AddStandardBookingQueryParams()
        {
            this.StandardBookingSQLCommand.Parameters.Add("@BookingID", SqlDbType.BigInt);
        }

        //BookingParticipant - takes BookingID
        public void AddStandardStandardBookingParticipantQueryParams()
        {
            this.StandardBookingParticipantSQLCommand.Parameters.Add("@BookingID", SqlDbType.BigInt);
        }


        //Booking Extra Selection
        public void AddStandardBookingExtraSelectionQueryParams() 
        {
            this.StandardBookingExtraSelectionSQLCommand.Parameters.Add("@BookingExtraSelectionID", SqlDbType.BigInt);
        }

        public void AddBookingExtraSelectionByCustomerIDQueryParams() 
        {
            this.BookingExtraSelectionByCustomerIDSQLCommand.Parameters.Add("@CustomerID", SqlDbType.BigInt);
        }

        public void AddBookingExtraSelectionByBookingIDQueryParams()
        {
            this.BookingExtraSelectionByBookingIDSQLCommand.Parameters.Add("@BookingID", SqlDbType.BigInt);
        }

        //booking extra participant
        public void AddStandardBookingExtraParticipantQueryParams()
        {
            this.StandardBookingExtraParticipantSQLCommand.Parameters.Add("@BookingExtraParticipantID", SqlDbType.BigInt);
        }

        public void AddBookingExtraParticipantByBookingExtraSelectionIDQueryParams()
        {
            this.BookingExtraParticipantByBookingExtraSelectionIDSQLCommand.Parameters.Add("@BookingExtraSelectionID", SqlDbType.BigInt);
        }

        //PROPERTY
        public void AddStandardPropertyQueryParams()
        {
            this.StandardPropertySQLCommand.Parameters.Add("@PropertyID", SqlDbType.BigInt);
        }

        //PRC INFORMATION
        public void AddStandardPRCInformationQueryParams()
        {
            this.StandardPRCInformationSQLCommand.Parameters.AddWithValue("@PRCInformationID", 1);
        }
        //added 06 -05 -2013
        //PROPERTY REGION
        public void AddStandardPropertyRegionQueryParams()
        {
            this.StandardPropertyRegionSQLCommand.Parameters.Add("@PropertyRegionID", SqlDbType.BigInt);
        }

        public void AddStandardPropertyTownQueryParams()
        {
            this.StandardPropertyTownSQLCommand.Parameters.Add("@PropertyTownID", SqlDbType.BigInt);
        }


        //BOOKING EXTRA 
        public void AddStandardBookingExtraQueryParams()
        {
            this.StandardBookingExtraSQLCommand.Parameters.Add("@BookingExtraID", SqlDbType.BigInt);
        }

        public void AddBookingExtraByBookingExtraSelectionIDParams()
        {
            this.BookingExtraByBookingExtraSelectionIDSQLCommand.Parameters.Add("@BookingExtraSelectionID", SqlDbType.BigInt);
        }

        //BookingExtraByBookingExtraSelectionID
        public void AddStandardPropertyOwnerQueryParams()
        {
            this.StandardPropertyOwnerSQLCommand.Parameters.Add("@PropertyID", SqlDbType.BigInt);
        }

        //Booking parent container
        public void AddStandardBookingParentContainerParams()
        {
            this.StandardBookingParentContainerCommand.Parameters.Add("@BookingParentContainerID", SqlDbType.BigInt);
        }

        public void AddBookingExtraAttributesByBookingExtraIDParams()
        {
            this.BookingExtraAttributesByBookingExtraID.Parameters.Add("@BookingExtraID", SqlDbType.BigInt);
        }

        //END

        
    }
}