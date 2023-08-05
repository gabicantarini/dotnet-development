using System;

class Program
{
    static void Main()
    {
        ForStructure();
    }

    static void ForStructure()
    {
        Console.WriteLine("Escreva uma sequência de números separados por espaço: ");
        var sentence = Console.ReadLine();

        var sentenceSum = sentence.Split(' '); //remove space

        Console.WriteLine("Estes são os números digitados: ");

        for (int i = 0; i < sentenceSum.Length; i++)
        {
            Console.Write(sentenceSum[i]); //return numbers without space
        }
        Console.ReadKey();
    }

}