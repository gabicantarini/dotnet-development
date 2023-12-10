using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.App.Model
{
    internal class BookClass
    {
        public int Id { get; private set; }
        public string Title { get; set; }

        public string Author { get; set; }
        public string Isbn { get; set; }
        public int PublishYear { get; set; }


        private List<BookClass> _books = new List<BookClass>();

        public void PressKey()
        {
            Console.WriteLine("Digite qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();
        }

        public void BookRegister()
        {
            Console.Clear();
            Console.WriteLine("******CADASTRAR UM LIVRO******");



        }

        public void BookConsult()
        {
            Console.Clear();
            Console.WriteLine("******CONSULTAR UM LIVRO******");



        }

        public void BookRemove()
        {
            Console.Clear();
            Console.WriteLine("******REMOVER UM LIVRO******");


        }

        internal void RemoveBook()
        {
            throw new NotImplementedException();
        }
    }




}
