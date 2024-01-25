using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaiorMenorQue
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numero1 = 3;
            int numero2 = 9;
            int numero3 = 1;

            int maior = MaiorDe3(numero1, numero2, numero3);
            int menor = MenorDe3(numero1, numero2, numero3);

            Console.WriteLine($"Maior : {maior}");
            Console.WriteLine($"Menor : {menor}");

            Console.ReadLine();
        }

        static int MaiorDe3(int X, int Y, int Z)
        {
            if (X < Y)
                X = Y;
            if (X < Z)
                X = Z;
            return X;
        }

        static int MenorDe3(int X, int Y, int Z)
        {
            if (X > Y)
                X = Y;
            if (X > Z)
                X = Z;
            return X;
        }
    }
}
