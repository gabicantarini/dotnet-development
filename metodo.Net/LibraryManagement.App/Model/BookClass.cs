using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.App.Model
{
    internal class BookClass
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublishYear { get; set; }
    }
}
