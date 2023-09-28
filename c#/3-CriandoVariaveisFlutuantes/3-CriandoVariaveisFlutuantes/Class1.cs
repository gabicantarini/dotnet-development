namespace _7_Condicionais
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Executando projeto 7 - Condicionais");

            int idadeJoao = 16;
            int quantidadePessoas = 2;

            if (idadeJoao >= 18)
            {
                Console.WriteLine("João possui mais de 18 anos de idade. Pode entrar.");

            }
            else
            {
                Console.WriteLine("João não possui mais de 18 anos de idade. Não pode entrar.")
                   }
            Console.ReadLine();
        }
    }
}
}