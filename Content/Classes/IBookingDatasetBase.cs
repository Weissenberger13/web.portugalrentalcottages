using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Interfaces
{

    /// <summary>
    /// base class 
    /// </summary>
    public interface IDatasetBase
    {
        long Id { get; set; }
        IEventProvider theEventsChainToComplete { get; set; }       
        
    }
}