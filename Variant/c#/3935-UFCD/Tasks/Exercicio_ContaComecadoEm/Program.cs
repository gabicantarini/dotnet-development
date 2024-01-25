using System;

class Program
{
    
   
static int ContaIniciais(string nome, int contador)
    {
        char primeiraLetra = nome[0];
        if (primeiraLetra == 'A' || primeiraLetra == 'B' || primeiraLetra == 'C')
        {
            contador++;
        }

        return contador;
    }


    static void Main()
    {
        int Conta = 0;
        string Nome;

        Console.Write("Digite um nome ou ZZZ para terminar: ");
        Nome = Console.ReadLine().ToUpper();//toUpper valida maiusculas e minuculas

        while (Nome != "ZZZ")
        {
            Conta = ContaIniciais(Nome, Conta);
            Console.Write("Digite um nome ou ZZZ para terminar: ");
            Nome = Console.ReadLine().ToUpper();
         }

            Console.WriteLine("Há " + Conta + " nome(s) começado(s) por A, B ou C");
            Console.ReadKey();
    }
}