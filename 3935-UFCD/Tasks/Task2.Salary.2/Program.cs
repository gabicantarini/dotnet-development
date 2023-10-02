using System;


class Program
{
    public static void Main()
    {
        Console.WriteLine("Payment Roll.");
        Console.Write("\nDigite o salário bruto: ");
        double salary = Convert.ToInt32(Console.ReadLine());

        double socialSecurity = salary * 20 / 100;     
        double salary12 = (salary - (salary * 12 / 100) - socialSecurity);
        double salary15 = (salary - (salary * 15 / 100) - socialSecurity);
        double salary18 = (salary - (salary * 18 / 100) - socialSecurity);

        if (salary < 500)
        {
            Console.WriteLine($"\nO salário líquido é: ${salary}");
        }
        else if (salary > 500 && salary <= 1000)
        {            
            Console.WriteLine($"\nO salário líquido é: ${salary12:F}");
        }
        else if (salary >= 1000 && salary < 1500)
        {            
            Console.WriteLine($"\nO salário líquido é: ${salary15:F}");
        }
        else if (salary > 1500)
        {            
            Console.WriteLine($"\nO salário líquido é: ${salary15:F}");
        }
        else
        {
            Console.WriteLine("Digite um número válido.");
        }

    }
}

