using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
	{
		// Assuming you have a DbContext named 'context'
		private static List<Customer> customers = new List<Customer>();

		public IEnumerable<Customer> GetAll()
		{
			return customers;
		}

		public Customer Get(int id)
		{
			return customers.FirstOrDefault(c => c.ID == id);
		}

		public void Add(Customer customer)
		{
			customer.ID = customers.Count()+1;
			customers.Add(customer);
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

		public void Delete(int id)
		{
			var customer = customers.FirstOrDefault(c => c.ID == id);
			if (customer != null)
			{
				customers.Remove(customer);
			}
		}
	}
}
