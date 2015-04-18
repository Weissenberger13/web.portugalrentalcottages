using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace BootstrapVillas.Content.Classes
{
    public static class DataTableHelper
    {
        /// <summary>
        /// Replaces all DateTime columns with strings and parses to just date dd/MM/yyyy
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static void Convert<T>(this DataColumn column, Func<object, T> conversion)
        {
            foreach (DataRow row in column.Table.Rows)
            {

                if (row[column].ToString() != "")
                {
                    var test = row[column].ToString();
                    
                    row[column] = conversion(row[column]);
                }
            }
        }


    }
}