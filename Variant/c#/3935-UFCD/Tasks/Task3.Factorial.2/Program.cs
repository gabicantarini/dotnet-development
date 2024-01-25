class Program
{
    static void Main()
    {
        FactorialLoop();
    }

    private static void FactorialLoop()
    {
        double i, number, factorial;
        Console.Write("Informe o número: ");
        number = double.Parse(Console.ReadLine());

        factorial = number;
        for (i = number - 1; i >= 1; i--) //loop para chegar ao valor do fatorial
        {
            Console.WriteLine($"{factorial} * {i}");

            factorial = factorial * i;

            Console.WriteLine($"i={i}");
            Console.WriteLine($"fatorial={factorial}");
            Console.WriteLine("  ");
        }
        Console.WriteLine($"\nFatorial de {number} é {factorial} ");
        Console.ReadLine();
    }
}
