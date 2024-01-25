using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContarNumero
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int Conta = 0;
            string Nome;

            Console.Write("Digite um nome ou ZZZ para terminar: ");
            Nome = Console.ReadLine().ToUpper();

            while (Nome != "ZZZ")
            {
                Conta = ContaIniciais(Nome, Conta);
                Console.Write("Digite um nome ou ZZZ para terminar: ");
                Nome = Console.ReadLine().ToUpper();
            }

            Console.WriteLine($"Há {Conta} nome(s) começado(s) por A, B ou C");
            Console.ReadLine();
        }

        static int ContaIniciais(string nome, int contador)
        {
            if (nome.StartsWith("A") || nome.StartsWith("B") || nome.StartsWith("C"))
            {
                contador++;
            }
            return contador;
        }
    }
}
