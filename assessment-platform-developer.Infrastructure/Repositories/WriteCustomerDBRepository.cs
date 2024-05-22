using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;
using assessment_platform_developer.Infrastructure.DataStore;
using System.Linq;

namespace assessment_platform_developer.Infrastructure.Repositories
{
    public class WriteCustomerDBRepository : IWriteRepository<Customer>
    {
		CustomerDbContext _dbContext;
		public WriteCustomerDBRepository()
		{
			_dbContext = new CustomerDbContext();
		}

        public void Add(Customer customer)
        {	
			customer.ID = _dbContext.Customers.ToList().Count+1;
            _dbContext.Customers.Add(customer);
			_dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.ID == id);
			if (customer != null)
			{
				_dbContext.Customers.Remove(customer);
			}
			
			_dbContext.SaveChanges();
        }

        public void Update(Customer customer)
        {
            var existingCustomer = _dbContext.Customers.FirstOrDefault(c => c.ID == customer.ID);
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
				
			_dbContext.SaveChanges();
			}
        }
    }
}
