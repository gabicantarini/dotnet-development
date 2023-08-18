using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMethods
{
    internal class Program
    {
        // Datetime
        // Intervalo de valor 01/01/0001 00:00:00 a 31/12/9999 11:59:59

        // Most important Properties
        // Date: refers to any date
        // Day, Month, Year, Hour, Minute, Second
        // Now: Gets hour and date
        // Today: Gets date
        // DayOfWeek, DayOfYear: gets partial day acording to the day of week or year
        static void Main(string[] args)
        {

            var now = DateTime.Now;
            var Today = DateTime.Today;

            var threeDaysAg = Today.AddDays(-3);
            var sixMonthsLater = Today.AddMonths(6);
            var twoYearsLater = Today.AddYears(2);

            var shortDate = Today.ToShortDateString();
            var longDate = Today.ToLongDateString();

            var shortTime = Today.ToShortTimeString();
            var longTime = Today.ToLongTimeString();

            var date = now.Date;
            var day = now.Day;
            var month = now.Month;
            var year = now.Year;    
            var hour = now.Hour;
            var minute = now.Minute;
            var second = now.Second;

            var dayOfWeek = now.DayOfWeek;

            if (dayOfWeek == DayOfWeek.Sunday || dayOfWeek == DayOfWeek.Saturday)
            {
                Console.WriteLine("É fim de semana!");
            }

            var dayOfYear = now.DayOfYear;


        }
    }
}
