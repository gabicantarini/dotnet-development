using System;
using AdvancedCsharp.Shared;

namespace AdvancedCsharp.OopConsole.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IGenericRepository<Customer> _genericRepository;

        public CustomerRepository(IGenericRepository<Customer> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int Add(Customer customer)
        {
            _genericRepository.Insert(customer);
            _genericRepository.Save();

            return customer.Id;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _genericRepository.GetAll();
        }

        public Customer? GetCustomerById(int id)
        {
            return _genericRepository.GetById(id);
        }
    }
}

