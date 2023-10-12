using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6.NameLastName
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string fullName;

            Console.Write("Digite o seu nome completo: ");
            fullName = Console.ReadLine();

            var names = fullName.Split(' ');
            string firstName = names[0];//[0] returns string first position
            string lastName = fullName.Substring(fullName.LastIndexOf(" "));
            //Substring: retuns the string from the indexOf position
            //LastIndexOf: returns last index position

            Console.WriteLine("Primeiro nome: " + firstName);
            Console.Write("Último nome: " + lastName);


            Console.ReadLine();
        }
    }
}
