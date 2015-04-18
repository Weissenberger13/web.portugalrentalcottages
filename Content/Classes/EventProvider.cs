using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;
using BootstrapVillas.Content.Interfaces;

namespace BootstrapVillas.Content.Classes
{
    public class EventProvider : IEventProvider
    {
        public EventProvider(IDatasetBase aTypeOfSetToGenerateEventsFor)
        {
            this.IsBookingComplete = false;
            this.theEventsToComplete = new List<BookingDataCollectionEventType.Types>();
            GenerateNecessaryEvents(aTypeOfSetToGenerateEventsFor);
        }




        public List<BookingDataCollectionEventType.Types> theEventsToComplete
        {get; set;}

        public bool IsBookingComplete {get; set;}
       

        public List<BookingDataCollectionEventType.Types> CheckRemainingEvents()
        {
            return this.theEventsToComplete;
        }

        public BookingDataCollectionEventType.Types CheckNextEvent()
        {
            return this.theEventsToComplete.FirstOrDefault();
        }

        public bool CheckIfBookingComplete()
        {
            if (this.IsBookingComplete.Equals(true)){return true;}
            else {return false;}
        }

        public bool EventComplete_RemoveFromRemainingEvents(BookingDataCollectionEventType.Types theTypeToRemove)
        {
            this.theEventsToComplete.Remove(theTypeToRemove);
            return true;
        }

        public bool GenerateNecessaryEvents(IDatasetBase aType)
        {

            if (aType is IBookingSet)
            {
                this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.Customer);
                this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.Payment);
                this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.Booking);
                this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.BookingParticipant);
            }
            else if (aType is IBookingExtraSelectionSet)
            {
                this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.Customer);
                this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.Payment);
                this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.BookingExtraSelection);
                this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.BookingExtraParticipant);
            }
            else if (aType is ICustomerSet)
            {
               this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.Customer);
               this.theEventsToComplete.Add(BookingDataCollectionEventType.Types.Payment);
            }
            else
            { return false; }
            //add more later?
            return true;

        }
    }
}