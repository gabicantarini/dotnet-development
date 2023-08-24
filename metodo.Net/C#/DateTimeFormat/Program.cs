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
            var format = new string[] { "d", "D", "f", "F", "g", "G", "m", "o", "r", "s", "t", "T", "u", "U", "Y" };

            foreach (var formats in format) 
            {
                Console.WriteLine("Format date {0}: {1}", format, now.ToString(format));
                Console.WriteLine($"Format date {format} : {now.ToString(format)}");

            }
        }
    }
}
