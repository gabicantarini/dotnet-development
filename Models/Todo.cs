using System;

namespace Paginacao.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; } // diz se está concluido ou nao
        public DateTime CreatedAt { get; set; }
    }
}
