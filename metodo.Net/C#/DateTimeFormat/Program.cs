using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeFormat
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region DataTime Formatação

            var formats = new string[] { "d", "D", "f", "F", "g", "G", "m", "o", "r", "s", "t", "T", "u", "U", "Y" };

            DateTime now = DateTime.Now; // Criando uma instância de DateTime

            foreach (var format in formats) 
            {
                Console.WriteLine("Format date {0}: {1}", format, now.ToString(format));
                Console.WriteLine($"Format date {format} : {now.ToString(format)}");

            }

            Console.WriteLine("Diferentes maneiras de formatar manualmente.");
            Console.WriteLine($"Data no fomrato d: {now:d}");
            Console.WriteLine($"Data no fomrato MM-dd-yyyy: {now:MM-dd-yyyy}");
            var dateFormat = now.ToString("MM-dd-yyyy");
            var dateFormatBr = now.ToString("MM/dd/yyyy HH:mm:ss");

            #endregion DataTime Formatação

            Console.ReadKey();
        }

    }
}
