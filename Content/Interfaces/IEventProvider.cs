using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;
using BootstrapVillas.Content.Classes;

namespace BootstrapVillas.Content.Interfaces
{

    /// <summary>
    /// This provides the 'event chain' used to collect details
    ///
    /// </summary>
    public interface IEventProvider
    {
       
        //members
        List<BookingDataCollectionEventType.Types> theEventsToComplete { get; set; }
        bool IsBookingComplete { get; set; }
        
        //methods
        List<BookingDataCollectionEventType.Types> CheckRemainingEvents();

        BookingDataCollectionEventType.Types CheckNextEvent();

        bool CheckIfBookingComplete();

        bool EventComplete_RemoveFromRemainingEvents(BookingDataCollectionEventType.Types theTypeToRemove);

        bool GenerateNecessaryEvents(IDatasetBase generateEventsForThisType);

    }

}


    

