class Program
{
    static void Main()
    {
        ForeachStructure();
    }

    static void ForeachStructure()
    {
        Console.WriteLine("Escreva uma sequência de números separados por espaço: ");
        var numbers = Console.ReadLine();

        Console.WriteLine("Estes são os números digitados usando foreach: ");

        foreach(var number in numbers)
        {
            Console.Write(number);
        }
    }
}