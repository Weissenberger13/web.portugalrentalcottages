using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models.ViewModels;

namespace BootstrapVillas.Content
{
    public interface IBookingSyncClient
    {
        Task<IEnumerable<BookingExternalSyncRequest>> GetExternalBookingsAsync();

    }
}
