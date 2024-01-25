using System;

namespace Alfanumerico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string X1, Y1; 

            Console.WriteLine("Valores originais:");
            Console.Write($"Digite o valor da primeira variável (X): ");
            X1 = Console.ReadLine();
            Console.Write($"Digite o valor da segunda variável (Y): ");
            Y1 = Console.ReadLine();

            TrocarValores(ref X1, ref Y1);

            
        }

        static void TrocarValores(ref string X1, ref string Y1)
        {
            string temp = X1;
            X1 = Y1;
            Y1 = temp;

            Console.WriteLine("\nValores trocados:");
            Console.WriteLine($"X = {X1}");
            Console.WriteLine($"Y = {Y1}");

            Console.ReadLine();
        }
    }
}
