using System;

class Program
{
    static void Main()
    {
        WhileStructure(); // returns function
    }

    static void WhileStructure()
    {
        Console.WriteLine("Escreva uma sequência de números separados por espaço: ");
        var numbers = Console.ReadLine();

        Console.WriteLine("Estes são os números digitados usando while: ");

        var count = 0; //starts to count in 0

        while (count < numbers.Length) //limits the count interation numbers to numbers limit lenght
        {
            Console.Write(numbers[count]); //returns numbers with while repetition structure
            count++;
        }

        Console.ReadKey(); //returns the next function or information pressend by the user

    }

}