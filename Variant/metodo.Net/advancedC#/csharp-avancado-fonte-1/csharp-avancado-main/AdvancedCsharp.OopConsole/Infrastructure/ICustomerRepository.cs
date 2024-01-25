using System;
using AdvancedCsharp.Shared;

namespace AdvancedCsharp.OopConsole.Infrastructure
{
    public interface ICustomerRepository
    {
        int Add(Customer customer);
        IEnumerable<Customer> GetAllCustomers();
        Customer? GetCustomerById(int id);
    }
}

