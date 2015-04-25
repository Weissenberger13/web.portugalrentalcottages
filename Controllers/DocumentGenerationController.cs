using System.Collections.ObjectModel;
using System.Runtime.Serialization.Configuration;
using System.Web.UI.WebControls.WebParts;
using AjaxControlToolkit;
using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Words.Tables;
using BootstrapVillas.Content.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootstrapVillas.Models;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;
using SaveFormat = Aspose.Words.SaveFormat;

namespace BootstrapVillas.Controllers
{
    public class DocumentGenerationController
    {
        //
        // GET: /DocumentGeneration/

        public PRCDocument InitialSetUpPRCDocument(PRCDocument.PRCDocumentType Type)
        {
            PRCDocument thePRCDocument = new PRCDocument(Type);
            thePRCDocument = AssignPRCDocumentTypeTemplateURLs(thePRCDocument);

            //need to test path exists - if not create new

            thePRCDocument.SavePath = Path.Combine(HttpRuntime.AppDomainAppPath, "GeneratedDocuments\\");
            thePRCDocument.DateCreated = DateTime.Now;
            thePRCDocument.Sent = false;


            string bookingOrBookingExtraSelectionID = "";
            bookingOrBookingExtraSelectionID = (thePRCDocument.BookingID != null) ? thePRCDocument.BookingID.ToString() : thePRCDocument.BookingExtraSelectionID.ToString();
            string bookingOrBookingExtraLetterPrefix = (thePRCDocument.BookingID != null) ? "B" : "BES";

            //c142_LateBookingVoucher_01/01/2014_BE123            
            string fileNamePrefix = "c" + thePRCDocument.CustomerID.ToString() + "_" + DateTime.Now.ToString("dd_MM_yyyy")
                + "_" + bookingOrBookingExtraLetterPrefix + bookingOrBookingExtraSelectionID;

            thePRCDocument.FileName = fileNamePrefix;

            return thePRCDocument;
        }


        public PRCDocument AssignPRCDocumentTypeTemplateURLs(PRCDocument prcDocument)
        {
            //test the type and assign the correct path
            switch (prcDocument.Type)
            {


                //request forms
                case PRCDocument.PRCDocumentType.UK_BookingRequestForm:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_UK_BookingRequestFormTemplate.doc";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.EU_BookingRequestForm:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_EU_BookingRequestFormTemplate.doc";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.UK_LateBookingRequestForm:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_UK_LateBookingRequestFormTemplate.docx";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.EU_LateBookingRequestForm:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_EU_LateBookingRequestFormTemplate.docx";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;

                //confimation vouchers
                case PRCDocument.PRCDocumentType.UK_BookingConfirmationVoucher:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_UK_StandardBookingConfirmationVoucherTemplate.doc";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.EU_BookingConfirmationVoucher:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_EU_StandardBookingConfirmationVoucherTemplate.doc";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;


                case PRCDocument.PRCDocumentType.UK_LateBookingConfirmationVoucher:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_UK_LateBookingConfirmationVoucherTemplate.docx";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.EU_LateBookingConfirmationVoucher:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_EU_LateBookingConfirmationVoucherTemplate.doc";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;



                //other docs
                case PRCDocument.PRCDocumentType.LocalAreaInfo:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_LocalAreaInfo.rtf";
                    break;
                case PRCDocument.PRCDocumentType.BookingConfirmationTicket:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_ConfirmationTicketTemplate.doc";
                    break;
                case PRCDocument.PRCDocumentType.BookingMap:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_Map.rtf";
                    break;
                case PRCDocument.PRCDocumentType.BookingFAQ:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_FAQ.rtf";
                    break;
                case PRCDocument.PRCDocumentType.BookingHouseRules:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_HouseRules.doc";
                    break;

                case PRCDocument.PRCDocumentType.SecurityDeposit:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_SecurityDepositTemplate.docx";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.RentalRemittanceAdvice:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/BOOKING_HOMEOWNER_RemAdvice.docx";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.UK_WineTasting:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/EXTRA_UK_WineTourVoucher.docx";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.EU_WineTasting:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/EXTRA_EU_WineTourVoucher.docx";
                    prcDocument.MainDataTable.TableName = "BookingTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.UK_AirportTransfer:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/EXTRA_UK_AirportTransferTemplate.docx";
                    prcDocument.MainDataTable.TableName = "BookingExtraTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingExtraParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.EU_AirportTransfer:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/EXTRA_EU_AirportTransferTemplate.docx";
                    prcDocument.MainDataTable.TableName = "BookingExtraTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingExtraParticipantTable";
                    break;

                case PRCDocument.PRCDocumentType.UK_CarRental:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/EXTRA_UK_CarRentalVoucher.docx";
                    prcDocument.MainDataTable.TableName = "BookingExtraTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingExtraParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.EU_CarRental:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/EXTRA_EU_CarRentalVoucher.docx";
                    prcDocument.MainDataTable.TableName = "BookingExtraTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingExtraParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.UK_CoachTour:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/EXTRA_UK_CoachTourVoucher.docx";
                    prcDocument.MainDataTable.TableName = "BookingExtraTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingExtraParticipantTable";
                    break;
                case PRCDocument.PRCDocumentType.EU_CoachTour:
                    prcDocument.ServerDocumentURL = "/DocumentTemplates/EXTRA_EU_CoachTourVoucher.docx";
                    prcDocument.MainDataTable.TableName = "BookingExtraTable";
                    prcDocument.SecondaryDataTable.TableName = "BookingExtraParticipantTable";
                    break;




            }

            return prcDocument;
        }



        //Returns local filepath and file name of the document
        public string CreateDocumentToFileSystem(Customer customer, PRCDocument.PRCDocumentType type, Booking booking = null, BookingExtraSelection bes = null)
        {
            try
            {
                PRCDocument aDocument = new PRCDocument(customer, type, booking, bes);

                var filepathAndName = MergeDocumentWithDatabaseAndReturnFilePath(customer, type, booking, bes);

                return filepathAndName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private string MergeDocumentWithDatabaseAndReturnFilePath(Customer customer, PRCDocument.PRCDocumentType type, Booking booking = null, BookingExtraSelection bes = null)
        {
            //BOOKING needs to have a propertyID, BES needs to have the ExtraID!!!!


            var db = new PortugalVillasContext();
            var bookPartTable = new DataTable();
            var besPartTable = new DataTable();
            var extraAttributes = new DataTable();

            PRCDocument aDocument = new PRCDocument(type);
            aDocument = InitialSetUpPRCDocument(type);

            //tables for merge

            List<DataTable> tablesForMerge_PreMerge = new List<DataTable>();


            PRCDocumentData theDocumentDataInstance = new PRCDocumentData();

            //PRC
            tablesForMerge_PreMerge.Add(
                theDocumentDataInstance.GetPRCDocumentData(
                    PRCDocumentData.PRCReturnDataTableWrapperTypes.StandardPRCInformation, 1));

            //CUSTOMERS
            tablesForMerge_PreMerge.Add(
                theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.CustomerByCustomerID,
                    customer.CustomerID)); //works*/
            //CUSTOMER BANK DETAILS
            
            
            
            if (booking != null)
            {
                tablesForMerge_PreMerge.Add(
                theDocumentDataInstance.GetPRCDocumentData(
                    PRCDocumentData.PRCReturnDataTableWrapperTypes.CustomerBankDetailByCustomerID, customer.CustomerID));


                //COMMISSION FOR YEAR            
                var TotalCommisssion = new DataTable();
                TotalCommisssion.Columns.Add("TotalCommissionThisYear");
                TotalCommisssion.Columns.Add("CommissionDateTime");
                DataRow _row = TotalCommisssion.NewRow();
                _row["TotalCommissionThisYear"] = db.Bookings.Where(x => x.PropertyID == booking.PropertyID).Sum(x => x.CommissionAmount);
                _row["CommissionDateTime"] = DateTime.Now.ToShortDateString();
                TotalCommisssion.Rows.Add(_row);

                tablesForMerge_PreMerge.Add(TotalCommisssion);

                //BOOKING
                tablesForMerge_PreMerge.Add(
                    theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingByBookingID, booking.BookingID));


                //PROPERTY
                tablesForMerge_PreMerge.Add(
                    theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.PropertyByPropertyID,
                        (long)booking.PropertyID));


                //TOWN
                tablesForMerge_PreMerge.Add(
                    theDocumentDataInstance.GetPRCDocumentData(
                        PRCDocumentData.PRCReturnDataTableWrapperTypes.PropertyTownByPropertyTownID, Convert.ToInt64(db.Properties.Where(x => x.PropertyID == booking.PropertyID).FirstOrDefault().PropertyTownID)));

                //REGION
                tablesForMerge_PreMerge.Add(
                    theDocumentDataInstance.GetPRCDocumentData(
                        PRCDocumentData.PRCReturnDataTableWrapperTypes.PropertyRegionByPropertyRegionID, db.Properties.Where(x => x.PropertyID == booking.PropertyID).FirstOrDefault().PropertyTown.PropertyRegionID));

                //BOOKINGPART
                bookPartTable = theDocumentDataInstance.GetPRCDocumentData(
                    PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingParticipantByBookingID, booking.BookingID);
                bookPartTable.TableName = "BOOKINGPARTTABLE";
                tablesForMerge_PreMerge.Add(bookPartTable);

                if (booking.BookingParentContainerID != null)
                {
                    //PARENT
                    tablesForMerge_PreMerge.Add(
                        theDocumentDataInstance.GetPRCDocumentData(
                            PRCDocumentData.PRCReturnDataTableWrapperTypes.StandardBookingParentContainer,
                            (long)booking.BookingParentContainerID));
                }




                BookingParentContainer bookingParentContainer;

                //PROPERTY OWNER
                tablesForMerge_PreMerge.Add(
              theDocumentDataInstance.GetPRCDocumentData(
                  PRCDocumentData.PRCReturnDataTableWrapperTypes.StandardPropertyOwner, (long)booking.PropertyID));
            }




            if (bes != null)
            {

                //get top level item for same sub type to pull out attributes:
                long? extraIDforAtts = null;

                extraIDforAtts = db.BookingExtras.Where(x =>
                        x.TopLevelItem == true)
                        .Where(y => y.BookingExtraSubTypeID == bes.BookingExtra.BookingExtraSubTypeID)
                        .FirstOrDefault().BookingExtraID;

                //BES
                tablesForMerge_PreMerge.Add(
                    theDocumentDataInstance.GetPRCDocumentData(
                        PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingExtraSelectionByBookingExtraSelectionID, bes.BookingExtraSelectionID));

                //BEST PART (name etc)

                besPartTable = theDocumentDataInstance.GetPRCDocumentData(
                    PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingExtraParticipantByBookingExtraSelectionID,
                    bes.BookingExtraSelectionID);
                besPartTable.TableName = "BESPARTTABLE";

                tablesForMerge_PreMerge.Add(besPartTable);
                //EXTRA
                tablesForMerge_PreMerge.Add(
                    theDocumentDataInstance.GetPRCDocumentData(
                        PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingExtraByBookingExtraID, bes.BookingExtraID));

                //PARENT
                if (bes.BookingParentContainerID != null)
                {
                    tablesForMerge_PreMerge.Add(
                        theDocumentDataInstance.GetPRCDocumentData(
                      PRCDocumentData.PRCReturnDataTableWrapperTypes.StandardBookingParentContainer, (long)bes.BookingParentContainerID));
                }

                //ATTRIBUTES

                if (extraIDforAtts != null)
                {
                    extraAttributes = theDocumentDataInstance.GetPRCDocumentData(
                        PRCDocumentData.PRCReturnDataTableWrapperTypes.BookingExtraAttributesByBookingExtraID,
                        (long)extraIDforAtts);
                    extraAttributes.TableName = "ATTRIBUTETABLE";
                }


                //test if there is a related booking / property IF it's a solo BES event
                if(booking != null)
                {
                    if(bes.BookingParentContainerID != 0)
                    {
                        booking = db.Bookings.Where(x => x.BookingParentContainerID == bes.BookingParentContainerID).FirstOrDefault();

                        //PROPERTY
                        tablesForMerge_PreMerge.Add(
                            theDocumentDataInstance.GetPRCDocumentData(PRCDocumentData.PRCReturnDataTableWrapperTypes.PropertyByPropertyID,
                                (long)booking.PropertyID));


                        //TOWN
                        tablesForMerge_PreMerge.Add(
                            theDocumentDataInstance.GetPRCDocumentData(
                                PRCDocumentData.PRCReturnDataTableWrapperTypes.PropertyTownByPropertyTownID, Convert.ToInt64(db.Properties.Where(x => x.PropertyID == booking.PropertyID).FirstOrDefault().PropertyTownID)));

                        //REGION
                        tablesForMerge_PreMerge.Add(
                            theDocumentDataInstance.GetPRCDocumentData(
                                PRCDocumentData.PRCReturnDataTableWrapperTypes.PropertyRegionByPropertyRegionID, db.Properties.Where(x => x.PropertyID == booking.PropertyID).FirstOrDefault().PropertyTown.PropertyRegionID));

                    }


                }
            }








            List<DataTable> tablesForMerge = new List<DataTable>();

            foreach (var dataTable in tablesForMerge_PreMerge)
            {
                if (dataTable.Rows.Count > 0)
                {

                    DataTable tableToModAndReturn = dataTable.Copy();
                    //clone every table
                    DataTable dtCloned = dataTable.Copy();
                    //change every datetime column to a string

                    //for every dateTime colum, load into new table
                    foreach (DataColumn col in dtCloned.Columns)
                    {
                        var theType = dtCloned.Columns[col.ColumnName].DataType.ToString();

                        if (dtCloned.Columns[col.ColumnName].DataType.ToString() == "System.DateTime")
                        {
                            //change the type
                            var name = col.ColumnName;
                            var value = "";
                            if (dtCloned.Rows[0][col].ToString().Count() >= 10)
                            {
                                value = /*DateTime.Parse(*/ Convert.ToDateTime(dtCloned.Rows[0][col].ToString()).ToString("dd/MM/yyyy");
                                //).ToString("dd/MM/yyyy");
                            }


                            tableToModAndReturn.Columns.Remove(name);
                            tableToModAndReturn.Columns.Add(new DataColumn
                            {
                                ColumnName = name,
                                DefaultValue = value
                            });
                        }
                    }



                    tablesForMerge.Add(tableToModAndReturn);
                }
            }


            Aspose.Words.Document theDoc = new Aspose.Words.Document(HttpRuntime.AppDomainAppPath + aDocument.ServerDocumentURL);
            //execute the merges            
            foreach (var dataTable in tablesForMerge)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    theDoc.MailMerge.Execute(dataTable.Rows[i]);
                }
            }

            //N.B The regions in the document need to correspond to the table name for the below merge to work
            theDoc.MailMerge.ExecuteWithRegions(bookPartTable);
            theDoc.MailMerge.ExecuteWithRegions(besPartTable);
            theDoc.MailMerge.ExecuteWithRegions(extraAttributes);

            string filepathAndName = aDocument.SavePath + aDocument.FileName + ".pdf";
            theDoc.Save(aDocument.SavePath + aDocument.FileName + ".pdf");


            db.Dispose();


            return filepathAndName;
        }


        public byte[] CreateDocument(Customer customer, PRCDocument.PRCDocumentType type, Booking booking = null, BookingExtraSelection bes = null)
        {
            try
            {
                PRCDocument aDocument = new PRCDocument(customer, type, booking, bes);

                var filepathAndName = MergeDocumentWithDatabaseAndReturnFilePath(customer, type, booking, bes);

                var documentBytes = System.IO.File.ReadAllBytes(filepathAndName);

                return documentBytes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public byte[] GetDocumentBLOB(string filepathAndFilename)
        {
            return File.ReadAllBytes(filepathAndFilename);
        }


        public IEnumerable<byte[]> GetAllFilesInADirectory(string filepath)
        {
            List<byte[]> filesToReturn = new List<byte[]>();

            foreach (string file in Directory.EnumerateFiles(filepath, "*.*"))
            {
                filesToReturn.Add(GetDocumentBLOB(file));
            }


            IEnumerable<byte[]> enumToReturn = filesToReturn.ToSafeReadOnlyCollection();

            return enumToReturn;

        }


        public byte[] ConvertWordBytesToPDFBytes(byte[] theBytes)
        {
            byte[] thePDFBytes;
            Stream inputStream = new MemoryStream(theBytes);

            Aspose.Words.Document theDoc = new Aspose.Words.Document(inputStream);

            theDoc.Save(HttpRuntime.AppDomainAppPath + "temp//tempdoc", SaveFormat.Pdf);

            using (var streamReader = new MemoryStream())
            {

                var ok = theDoc.Save(streamReader, SaveFormat.Pdf);

                //copy into array as PDf
                thePDFBytes = streamReader.ToArray();

            }
            return thePDFBytes;
        }

    }
}
