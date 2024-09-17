using System;
using System.Collections.Generic;
using System.Linq;

namespace DDD.Domain
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }

        public Address(string street, string city, string country)
        {
            Street = street;
            City = city;
            Country = country;
        }
    }

    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Address Address { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLogin { get; private set; }
        public List<string> Orders { get; private set; }

        // Private constructor to enforce use of factory
        private Customer() { }

        public void UpdateLastLogin()
        {
            LastLogin = DateTime.UtcNow;
        }

        public void AddOrder(string orderId)
        {
            Orders.Add(orderId);
        }
    }

    // Factory for creating Customer objects
    public class CustomerFactory
    {
        public Customer Create(string name, string email, Address address)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Address = address,
                CreatedAt = DateTime.UtcNow,
                Orders = new List<string>()
            };

            // Additional business logic for customer creation can be added here

            return customer;
        }
    }

    // Repository interface
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        Customer GetById(Guid id);
        void Update(Customer customer);
        void Remove(Guid id);
    }

    // Repository implementation
    public class CustomerRepository : ICustomerRepository
    {

    }

    public class Program
    {
        public static void Main()
        {
            var factory = new CustomerFactory();
            var repository = new CustomerRepository();

            // Creation using Factory
            var newCustomer = factory.Create(
                "John Doe",
                "john@example.com",
                new Address("123 Main St", "Anytown", "USA")
            );

            // Persistence using Repository
            repository.Add(newCustomer);

            // Retrieval using Repository
            var retrievedCustomer = repository.GetById(newCustomer.Id);

            // Modification of domain object
            retrievedCustomer.UpdateLastLogin();
            retrievedCustomer.AddOrder("ORDER001");

            // Update in Repository
            repository.Update(retrievedCustomer);

            // Deletion using Repository
            repository.Remove(newCustomer.Id);
        }
    }
}