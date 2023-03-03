using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class Catalogo : ICatalogo
    {
        public List<Livro> GetLivros()
        {
            var livros = new List<Livro>();
            livros.Add(new Livro("001", "Quem MExeu na Minha Query?", 12.99m));
            livros.Add(new Livro("002", "Fique rico com c#", 30.99m));
            livros.Add(new Livro("003", "Java para Baixinhos", 25.99m));
            return livros;
        }
    }
}
