using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;
using assessment_platform_developer.Infrastructure.DataStore;
using System.Collections.Generic;
using System.Linq;

namespace assessment_platform_developer.Infrastructure.Repositories
{
    public class WriteCustomerRepository : IWriteRepository<Customer>
    {
		// Assuming you have a DbContext named 'context'
		List<Customer> customers = DataBase.customers;

        public void Add(Customer customer)
        {
            customer.ID = DataBase.customers.Count()+1;
			customers.Add(customer);
        }

        public void Delete(int id)
        {
            var customer = customers.FirstOrDefault(c => c.ID == id);
			if (customer != null)
			{
				customers.Remove(customer);
			}
        }

        public void Update(Customer customer)
        {
            var existingCustomer = customers.FirstOrDefault(c => c.ID == customer.ID);
			if (existingCustomer != null)
			{
				existingCustomer.Name = customer.Name;
				existingCustomer.Email = customer.Email;
				existingCustomer.Phone = customer.Phone;	
				existingCustomer.City = customer.City;
				existingCustomer.Address = customer.Address;
				existingCustomer.State = customer.State;
				existingCustomer.Notes = customer.Notes;
				existingCustomer.Country = customer.Country;
				existingCustomer.Zip = customer.Zip;

				existingCustomer.ContactName = customer.ContactName;
				existingCustomer.ContactEmail = customer.ContactEmail;
				existingCustomer.ContactPhone = customer.ContactPhone;
			}
        }
    }
}
