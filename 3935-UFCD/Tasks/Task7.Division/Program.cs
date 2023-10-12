using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task7.Division
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrimeiraOpcao();
            SegundaOpcao();

        }

        static void PrimeiraOpcao()
        {
            Console.WriteLine("PRIMEIRA OPÇÃO!");
            Console.Write("Qual o dividendo e o divisor? ");
            string numbers = Console.ReadLine();

            string[] values = numbers.Split(new char[] { ' ', ',' }); //replace empty space by comma

            int divisor = Convert.ToInt32(values[0]); //TryParse converts string in int
            int divising = Convert.ToInt32(values[1]); //TryParse converts string in int


            double result = (double)divisor / divising;
            int resultadoArredondado = (int)Math.Round(result);
            Console.WriteLine($"O Resultado da divisão inteira é: {resultadoArredondado}");
            Console.ReadKey();

        }
            static void SegundaOpcao()
        {

            Console.WriteLine("\nSEGUNDA OPÇÃO!");

            Console.Write("Qual o dividendo e o divisor? ");
            string input = Console.ReadLine();

            string[] amount = input.Split(new char[] { ' ', ',' }); //replace empty space by comma

            if (amount.Length != 2)
            {
                Console.WriteLine("Entrada inválida. Digite um número válido."); //double check if there are values
                Console.ReadLine();
            }

            if (int.TryParse(amount[0], out int divisor) && int.TryParse(amount[1], out int divising)) //TryParse converts string in int
            {

                double resultado = (double)divisor / divising;
                int resultadoArredondado = (int)Math.Round(resultado);
                Console.WriteLine($"Resultado da divisão inteira é: {resultadoArredondado}");
                Console.ReadLine();

            }
            else
            {
                Console.WriteLine("Entrada inválida. Digite um número válido.");
                Console.ReadLine();
            }
        }
    }
}
