using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8.BirthDate
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Introduza a data de nascimento no formato DD/MM/AAAA (Ex: 10/11/2023): ");
            string dateBirth = Console.ReadLine();

            if (DateTime.TryParseExact(dateBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bornDay))
            {
                // Calcula a idade
                int age = AgeCalc(bornDay, DateTime.Today);
                var weekDay = bornDay.DayOfWeek;

                Console.WriteLine($"Idade: {age} anos");
                Console.WriteLine($"Dia da Semana: {weekDay}.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Formato de data inválido. Use o formato DD/MM/AAAA.");
                Console.ReadLine();
            }
        }

        static int AgeCalc(DateTime bornDay, DateTime currentDate)
        {
            int age = currentDate.Year - bornDay.Year;
            if (bornDay > currentDate.AddYears(-age)) age--;

            return age;
        }
    }
}
