using CustomersService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CustomersService.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerService(ICustomersDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _customers = database.GetCollection<Customer>(settings.CustomersCollectionName);
        }

        public List<Customer> Get() =>
            _customers.Find(c => true).ToList();
        
        public Customer Get(string id) =>
            _customers.Find<Customer>(c => c.Id == id).FirstOrDefault();
            

        public Customer Create(Customer customer)
        {
            _customers.InsertOne(customer);
            return customer;
        }

        public void Update(string id, Customer customer) =>
            _customers.ReplaceOne(c => c.Id == id, customer);

        public void Remove(string id) =>
            _customers.DeleteOne(c => c.Id == id);
    }
}

