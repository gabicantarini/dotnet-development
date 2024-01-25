using System;

class Program
{
    static void ImprimirNomes(string[] nomes, int x)
    {
        if (x > nomes.Length)
        {
            Console.WriteLine("O valor de x é maior do que o número de nomes na lista.");
            return;
        }

        Console.WriteLine($"Selecionamos os {x} primeiros nomes de uma lista de {nomes.Length}:");

        for (int i = 0; i < x; i++)
        {
            Console.WriteLine(nomes[i]);
        }
    }

    static void Main() //por default é private
    {
        string[] nomes = { "Ana", "Antônio", "Beatriz", "Joana", "Raul", "Vitória" };
        int x =4 ;
        ImprimirNomes(nomes, x);
        Console.ReadKey();
    }
}
                    
