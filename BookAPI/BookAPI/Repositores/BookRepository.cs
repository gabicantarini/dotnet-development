using BookAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI.Repositores
{
    public class BookRepository : IBookRepository
    {
        public readonly BookContext _context;

        public BookRepository(BookContext context)
        {
            _context = context;
        }

        public async Task<Book> Create(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync(); //async permite fazer várias operações ao mesmo tempo
            //await para esperar a fila e depois executar essa ação
            //sempre usamos async com await
            return book;
        }

        public async Task Delete(int id)
        {
            var bookToDelete = await _context.Book.FindAsync(id);
            _context.Book.Remove(bookToDelete);
            await _context.SaveChangesAsync();
        }

        public Task Delete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.Book.ToListAsync();         

        }

        public async Task<Book> Get(int id)
        {
            return await _context.Book.FindAsync(id);
        }

        public async Task Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
