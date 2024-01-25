using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
class Program
{
    //Pythagoras theory: hypotenuse = catetoA2 * catetoB2
    static void Main(string[] args)
    {
        Pythagoras();

    }
    
    static void Pythagoras()
    {
        double catetoA, catetoB, hypotenuse;

        Console.Write("Digite o valor do cateto A: ");
        catetoA = double.Parse(Console.ReadLine());

        Console.Write("Digite o valor do cateto B: ");
        catetoB = double.Parse(Console.ReadLine());
        hypotenuse = Math.Sqrt(catetoA * catetoA + catetoB * catetoB);

        Console.WriteLine("O valor da Hipotenusa é: {0}", hypotenuse);
        Console.ReadLine();

    }
    
}