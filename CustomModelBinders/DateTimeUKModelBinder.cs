using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapVillas.CustomModelBinders
{
    public class DateTimeUKModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

                // Check if the DateTime property being parsed is not null or "" (for JSONO
                if (value.AttemptedValue != null && value.AttemptedValue != "" && value.AttemptedValue.ToString().Count() > 8 && value.AttemptedValue.ToString().Count() < 11)
                
                {
                    // Parse the datetime to UK, because we recieve it dd/MM/yyyy from the POST/GET/client.
                    try
                    {
                        var dt = DateTime.ParseExact(value.AttemptedValue.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        var toChop = value.AttemptedValue.ToString();
                        toChop = toChop.Substring(0, 9);
                        //it's also got a time - 24 hour format standard
                        var dt = DateTime.Parse(toChop, CultureInfo.InvariantCulture);
                        return dt;
                        
                    }
                }

                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
        }
    }
}