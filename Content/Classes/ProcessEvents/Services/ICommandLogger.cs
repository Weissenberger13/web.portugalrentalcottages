using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Aspose.Email.Logging;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.ProcessEvents.Services
{
    public interface ICommandLogger
    {
        int Log(long EventID, EventCommand commandToLoganditsResults);

    }
}
