using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Data.QueriesSQL
{
    public class PVillasQueryLibrary
    {
        //public static string Standard = @"";

        
        //CUSTOMER
        public static string StandardCustomerQuery = @"SELECT TOP 1 * FROM CUSTOMER WHERE CUSTOMERID = @CustomerID";

        //CUSTOMER BANK DETAIL
        public static string StandardCustomerBankDetailQuery = @"SELECT TOP 1 * FROM CustomerBankDetail where CustomerID = @CustomerID";


        //BOOKING
        public static string StandardBookingQuery = @"SELECT TOP 1  
                    *                 
                    FROM Booking WHERE BOOKINGID = @BookingID";

        //BOOKING PARTICIPANT
        public static string StandardBookingParticipantQuery = @"SELECT * FROM BookingParticipant WHERE BOOKINGID = @BookingID";
        

        
        //BOOKING EXTRA SELECTION
        public static string StandardBookingExtraSelectionQuery = @"SELECT * FROM BookingExtraSelection WHERE BookingExtraSelectionID = @BookingExtraSelectionID";

        public static string BookingExtraSelectionByCustomerIDQuery = @"SELECT * FROM BookingExtraSelection WHERE CUSTOMERID = @CustomerID";

        public static string BookingExtraSelectionByBookingIDQuery = @"SELECT * FROM BookingExtraSelection WHERE BookingID = @BookingID";

        

        //BOOKING EXTRA PARTICIPANT
        public static string StandardBookingExtraParticipantQuery = @"SELECT * FROM BookingExtraParticipant WHERE BookingExtraParticipantID = @BookingExtraParticipantID";

        public static string BookingExtraParticipantByBookingExtraSelectionIDQuery = @"SELECT * FROM BookingExtraParticipant WHERE BookingExtraSelectionID = @BookingExtraSelectionID";

        //PROPERTY
        public static string StandardPropertyQuery = @"SELECT TOP 1  * FROM Property WHERE PropertyID = @PropertyID";


        //TOWN
        public static string StandardPropertyTownQuery = @"SELECT TOP 1 * FROM PROPERTYTOWN WHERE PropertyTownID = @PropertyTownID";

        //PRC Information
        //brings back the PRC Info (there is only one row)
        public static string StandardPRCInformationQuery = @"Select TOP 1 * from PRCInformation Where PRCInformationID = @PRCInformationID";
        
        //////////////////////////
        //added by ND 06 -05 -2013
        //////////////////////////

        //PROPERTY REGION
        public static string StandardPropertyRegionQuery = @"SELECT TOP 1  * FROM PropertyRegion WHERE PropertyRegionID = @PropertyRegionID";

        //BOOKING EXTRA
        public static string StandardBookingExtraQuery = @"SELECT * from BookingExtra WHERE BookingExtraID = @BookingExtraID";

        public static string BookingExtraByBookingExtraSelectionIDQuery = @"SELECT * FROM BookingExtraSelection bes
                                                                            inner join BookingExtra be on bes.BookingExtraID = be.BookingExtraID
                                                                            where BookingExtraSelectionID = @BookingExtraSelectionID";

        //PROPERTY OWNER
        public static string StandardPropertyOwnerQuery = @"SELECT TOP 1 * FROM PropertyOwner inner join Property on Property.PropertyOwnerID = PropertyOwner.PropertyOwnerID  WHERE PropertyID = @PropertyID";


        //PROPERTY OWNER
        public static string StandardBookingParentContainerQuery = @"SELECT TOP 1 * FROM BookingParentContainer WHERE BookingParentContainerID = @BookingParentContainerID";

        public static string StandardBookingAttributeQuery = @"SELECT * FROM BookingExtraAttribute WHERE BookingExtraID = @BookingExtraID";
        

        
        
        


    }
}