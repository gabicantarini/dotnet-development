using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Type 1 - If you want to contract a new plan.");
        Console.WriteLine("Type 2 - If you want to complain about service.");
        Console.WriteLine("Type 3 - If you want a new invoice.");
        Console.WriteLine("Type 4 - If you want to Exit.");

        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.WriteLine("New plan information.");
                break;
            case "2":
                Console.WriteLine("Tell me about your complain.");
                break;
            case "3":
                Console.WriteLine("Give me your email to send you a new invoice.");
                break;
            case "4":
                Console.WriteLine("See you next time.");
                break;
            default:
                Console.WriteLine("Use a valid option.");
                break;
        }
    }
}