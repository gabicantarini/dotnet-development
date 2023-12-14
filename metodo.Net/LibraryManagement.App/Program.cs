using LibraryManagement.App.Model;
using System.Collections.Generic;
using System.ComponentModel.Design;
using static System.Reflection.Metadata.BlobBuilder;


Book book = new Book();
User user = new User();

while (true)
{
    Console.WriteLine("********************MENU INICIAL DE GERENCIAMENTO DE LIVROS************************\n");
    Console.WriteLine("1. Cadastrar livro.");
    //validar dados
    Console.WriteLine("2. Consultar todos os livros.");
    Console.WriteLine("3. Consultar um livro.");
    Console.WriteLine("4. Remover livro.");
    Console.WriteLine("5. Cadastrar usuário."); //plus
    Console.WriteLine("6. Cadastrar empréstimo."); //plus
    Console.WriteLine("0. Sair.\n\n");
    Console.Write("Digite a opção escolhida: ");

    try
    {
        var menuOption = Console.ReadLine();

        switch (menuOption)
        {
            case "1":
                book.BookRegister();
                break;

            case "2":
                book.BookConsult();
                break;

            case "3":

                break;

            case "4":
                book.BookRemove();
                break;

            case "0":
                break;

            default:
                Console.WriteLine("Opção Inválida");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.Clear();
        Console.WriteLine($"ERRO {ex}");
        Console.Clear();

    }
}


