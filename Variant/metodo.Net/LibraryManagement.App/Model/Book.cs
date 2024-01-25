using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.App.Model
{
    internal class Book
    {
        public Book(int id, string title, string author, string isbn, int publishYear) {

            Id = id;
            Title = title;
            Author = author;
            Isbn = isbn;
            PublishYear = publishYear;
        }

        public Book()
        {
        }

        public int Id { get; private set; }
        public string Title { get; set; }

        public string Author { get; set; }
        public string Isbn { get; set; }
        public int PublishYear { get; set; }


       public List<Book> books = new List<Book>();

        public void PressKey()
        {
            Console.WriteLine("Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        public void BookRegister()
        {
            Book book = new Book();
            Console.Clear();
            Console.WriteLine("******CADASTRAR UM LIVRO******\n");
            Console.Write("Digite o Id do livro a cadastrar: ");
            book.Id = int.Parse(Console.ReadLine()!);
            //book.Id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o título do livro a cadastrar: ");
            book.Title = Console.ReadLine();

            Console.Write("Digite o nome do autor do livro a cadastrar: "); 
            book.Author = Console.ReadLine();

            Console.Write("Digite o isbn do livro a cadastrar: ");
            book.Isbn = Console.ReadLine();

            Console.Write("Digite o ano de publicação do livro a cadastrar: ");
            book.PublishYear = int.Parse(Console.ReadLine()!);
            //book.PublishYear = Convert.ToInt32(Console.ReadLine());
        }

        public void BookConsult()
        {
            Console.Clear();
            Console.WriteLine("******CONSULTAR UM LIVRO******");
            foreach (var book in books)
            {
                Console.WriteLine($"Id: {book.Id}");
                Console.WriteLine($"Título: {book.Title}");
                Console.WriteLine($"Autor: {book.Author}");
                Console.WriteLine($"ISBN: {book.Isbn}");
                Console.WriteLine($"Ano de publicação: {book.PublishYear}\n");
            };

            PressKey();

        }

        public void BookRemove()
        {
            Console.Clear();
            Console.WriteLine("******REMOVER UM LIVRO******");


        }

    }




}
