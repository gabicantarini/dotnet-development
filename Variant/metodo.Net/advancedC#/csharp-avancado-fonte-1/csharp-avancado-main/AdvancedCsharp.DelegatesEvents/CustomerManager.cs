using System;
namespace AdvancedCsharp.DelegatesEvents
{
    public class CustomerCreatedEventArgs : EventArgs
    {
        public string CustomerName { get; }

        public CustomerCreatedEventArgs(string customerName)
        {
            CustomerName = customerName;
        }
    }

    public class CustomerManager
    {
        public event EventHandler<CustomerCreatedEventArgs> CustomerCreated;

        public void CreateCustomer(string customerName)
        {
            Console.WriteLine($"Customer {customerName} created!");

            OnCustomerCreated(customerName);
        }

        protected virtual void OnCustomerCreated(string customerName)
        {
            CustomerCreated?.Invoke(this, new CustomerCreatedEventArgs(customerName));
        }
    }

}

