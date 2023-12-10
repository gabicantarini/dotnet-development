using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListNames
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            String[] Nomes = { "Ana","Antonio","Joana","Beatriz","Raul","Vitória"};

            int X = 4; // número de elementos a selecionar
            
            EscreverPrimeirosNomes(Nomes, X);
        }

        static void EscreverPrimeirosNomes(string[] Nomes, int X)
        {
            Console.WriteLine($"Selecionamos os primeiros {X} nomes da lista de {Nomes.Length}:");

            for (int i = 0; i < X && i < Nomes.Length; i++)
            {
                Console.WriteLine(Nomes[i]);
            }

            Console.ReadLine();
        }
    }

}

