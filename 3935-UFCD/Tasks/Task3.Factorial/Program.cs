
class Program
{
    static void Main()
    {
        Console.Write("Digite um número inteiro e positivo: ");
        int number = int.Parse(Console.ReadLine());
        //long para resultados com números longos

        long factorial = factorialCalc(number);

        Console.WriteLine($"O fatorial de {number} é {factorial}");
    }

    static long factorialCalc(int n)
    {
        if (n < 0)
        {
            Console.WriteLine("O número deve ser inteiro e positivo.");
        }

        long factorial = 1;
        for (int i = 2; i <= n; i++)
        {
            factorial *= i;
        }
        return factorial;
    }
}

