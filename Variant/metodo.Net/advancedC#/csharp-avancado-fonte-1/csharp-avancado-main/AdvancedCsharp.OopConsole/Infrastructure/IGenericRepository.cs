using System;
using AdvancedCsharp.Shared;

namespace AdvancedCsharp.OopConsole.Infrastructure
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        Task<T?> GetByIdAsync(int id);
        void Insert(T obj);
        Task InsertAsync(T obj);
        void Update(T obj);
        void Delete(int id);
        void Save();
    }
}

