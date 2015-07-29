using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models.ViewModels;

namespace BootstrapVillas.Content.BookingSyncClient
{
    public class BookingSyncClient :IBookingSyncClient
    {
        private HttpClient _client;
        public BookingSyncClient(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress =  new Uri(baseAddress);

        }

        public BookingSyncClient(string baseAddress, HttpClient client)
        {
            _client = client;
            client.BaseAddress = new Uri(baseAddress);

        }


        public async Task<IEnumerable<BookingExternalSyncRequest>> GetExternalBookingsAsync()
        {
           //client.BaseAddress = new Uri(masterURL);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await _client.GetAsync("api/products/1");
            if (response.IsSuccessStatusCode)
            {
                List<BookingExternalSyncRequest> externals = await response.Content.ReadAsAsync<List<BookingExternalSyncRequest>> ();

                return externals; 
            }

            return new List<BookingExternalSyncRequest>();
        }
    }
}
