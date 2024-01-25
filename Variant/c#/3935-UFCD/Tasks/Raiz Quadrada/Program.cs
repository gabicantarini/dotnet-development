using System;

namespace Raiz_Quadrada
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RaizQuad(1, 20);
            RaizQuad(25, 50);
            RaizQuad(100, 120);
        }

        private static void RaizQuad(int inf, int sup)
        {
            Console.WriteLine($"Raízes quadradas no intervalo de {inf} a {sup}:");

            for (int i = inf; i <= sup; i++) //raiz 3 intervalos
            {
                double raizQuadrada = Math.Sqrt(i); //raiz quadrada
                Console.WriteLine($"A raiz quadrada de {i} é {raizQuadrada:F2}");
            }

            Console.ReadLine();
        }
    }
}
