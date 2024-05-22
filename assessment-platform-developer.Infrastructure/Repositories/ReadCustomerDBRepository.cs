using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;
using assessment_platform_developer.Infrastructure.DataStore;
using System.Collections.Generic;
using System.Linq;

namespace assessment_platform_developer.Infrastructure.Repositories
{
    public class ReadCustomerDBRepository : IReadRepository<Customer>
    {
        CustomerDbContext _dbContext;
		public ReadCustomerDBRepository()
		{
			_dbContext = new CustomerDbContext();
		}

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customers;
        }

		public Customer Get(int id)
		{
			return _dbContext.Customers.FirstOrDefault(c => c.ID == id);
		}
    }
}
