using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;
using assessment_platform_developer.Infrastructure.DataStore;
using System.Collections.Generic;
using System.Linq;

namespace assessment_platform_developer.Infrastructure.Repositories
{
    public class ReadCustomerRepository : IReadRepository<Customer>
    {
        // Assuming you have a DbContext named 'context'
        List<Customer> customers = DataBase.customers;

        public IEnumerable<Customer> GetAll()
        {
            return customers;
        }

		public Customer Get(int id)
		{
			return customers.FirstOrDefault(c => c.ID == id);
		}
    }
}
