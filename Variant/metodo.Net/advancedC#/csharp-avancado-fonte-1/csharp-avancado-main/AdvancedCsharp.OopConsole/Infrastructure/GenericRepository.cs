using System;
using System.Diagnostics;
using AdvancedCsharp.Shared;
using Microsoft.EntityFrameworkCore;

namespace AdvancedCsharp.OopConsole.Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(T obj)
        {
            Console.WriteLine($"Current Thread {Thread.CurrentThread.ManagedThreadId}");
            _dbSet.Add(obj);
        }

        public async Task InsertAsync(T obj)
        {
            Console.WriteLine($"Current Thread {Thread.CurrentThread.ManagedThreadId}");
            await _dbSet.AddAsync(obj);
        }

        public void Update(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T? entityToDelete = _dbSet.Find(id);

            if (entityToDelete is null)
                return;

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // No Await will result in blocking call
        //public async Task<T?> GetByIdAsync(int id)
        //{
        //    return _dbSet.Find(id);
        //}
    }
}

