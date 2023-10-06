using System;
using System.Threading.Channels;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Type student grade: ");
        var gradesValue = Console.ReadLine();

        var grade = int.Parse(gradesValue);

        if (grade >= 70)
        {
            Console.WriteLine("\n Aproved!");
        } else if (grade >= 40)
        {
            Console.WriteLine("\n Second Chance.");
        }
        else
        {
            Console.WriteLine("\n Reproved!");
        }
               
    }
}