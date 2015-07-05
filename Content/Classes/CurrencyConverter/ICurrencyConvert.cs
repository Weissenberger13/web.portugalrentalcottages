using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootstrapVillas.Models;

namespace BootstrapVillas.Content.Classes.CurrencyConverter
{
    public interface ICurrencyConvert
    {
        CurrencyEnum CurrencyType { get; set; }
        decimal ExchangeRate { get; set; }

        decimal Convert(decimal amountToConvert);

    }
}
