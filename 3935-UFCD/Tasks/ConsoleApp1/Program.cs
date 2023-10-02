
class Program
{
    static void Main()
    {
        double length, heigh, area;

        Console.WriteLine("Informe as Área do Retângulo.");
        Console.Write("\nDigite o comprimento: ");
        length = double.Parse(Console.ReadLine());
        Console.Write("\nDigite a altura: ");
        heigh = double.Parse(Console.ReadLine());

        area = length * heigh;
        Console.WriteLine("A área do retângulo é: " + area);
        
    }
}

