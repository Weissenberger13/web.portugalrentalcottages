using BootstrapVillas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BootstrapVillas.Controllers
{
    public class AutocompleteController : ApiController
    {
        private PortugalVillasContext db = new PortugalVillasContext();

    


        public List<AutoCompleteSearch> GetProperties()
        {
            var props = db.Properties.Where(x=>x.Active == true).ToList();
            var autoCompleteProps = new List<AutoCompleteSearch>();

            foreach(var prop in props)
            {

                autoCompleteProps.Add(new AutoCompleteSearch
                    {
                        label = prop.LegacyReference,
                        value = "/Home/FullPropertyResult?PropertyID=" + prop.PropertyID

                    });

            }

            return autoCompleteProps;
        }

    }
}
