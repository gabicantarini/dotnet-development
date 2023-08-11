using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTime
{

    // Datetime
    // Intervalo de valor 01/01/0001 00:00:00 a 31/12/9999 11:59:59

    // Most important Properties
    // Date: refers to any date
    // Day, Month, Year, Hour, Minute, Second
    // Now: Gets hour and date
    // Today: Gets date
    // DayOfWeek, DayOfYear: gets partial day acording to the day of week or year
    internal class Program
    {
        static void Main(string[] args)
        {

            var now = DateTime.Now;
            var Today = DateTime.Today;
        }
    }
}
