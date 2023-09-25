
class Program
{
    static void Main()
    {
        Console.WriteLine("Payment Roll.");
        Console.Write("\nDigite o salário bruto: ");
        double salary = Convert.ToInt32(Console.ReadLine()); ;
        

        if(salary < 500)
        {
            Console.WriteLine("\nO salário líquido é: " + salary);
        } else if (salary >500 && salary <= 1000)
        {
            double salary12 = (salary - (salary * 12/100) - (salary * 20 / 100));
            Console.WriteLine("\nO salário líquido é: " + salary12.ToString("F"));
        } else if (salary >= 1000 && salary < 1500)
        {
            double salary15 = (salary - (salary * 15 / 100) - (salary * 20 / 100));
            Console.WriteLine("\nO salário líquido é: " + salary15.ToString("F"));
        } else if (salary > 1500)
        {
            double salary18 = (salary - (salary * 18 / 100) - (salary * 20 / 100));
            Console.WriteLine("\nO salário líquido é: " + salary18.ToString("F"));
        } else 
        {
            Console.WriteLine("Digite um número válido.");
        }
    }
}