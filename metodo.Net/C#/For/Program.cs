using System;

class Program
{
    static void Main()
    {
        ForStructure();
        ReplaceStructure();
    }

    static void ForStructure()
    {
        Console.WriteLine("Escreva uma sequência de números separados por espaço: ");
        var sentence = Console.ReadLine();

        var removeSpace = sentence.Split(' '); //remove space

        Console.WriteLine("Estes são os números digitados sem espaço: ");

        for (int i = 0; i < removeSpace.Length; i++)
        {
            Console.Write(removeSpace[i]); //return numbers without space
        }
        Console.ReadKey();
  
    }

    static void ReplaceStructure()
    {
        Console.WriteLine("Escreva uma sequência de números separados por espaço: ");
        var numbers = Console.ReadLine();
     
        var replaceSpace = numbers.Replace(' ', ','); //replace space by comma

        Console.WriteLine("Estes são os números digitados com a substituição do espaço por vírgula: ");

        for (int i = 0; i < replaceSpace.Length; i++)
        {
            Console.Write(replaceSpace[i]); //return replaced spaces by comma
        }
        Console.ReadKey();
    }

}