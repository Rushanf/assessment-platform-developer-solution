using assessment_platform_developer.Domain.Entities;
using System.Data.Entity;

namespace assessment_platform_developer.Infrastructure.DataStore
{
    public class CustomerDbContext : DbContext
    {    
        public CustomerDbContext() : base("Data Source=.;Initial Catalog=Customer;Integrated Security=SSPI;")
        {
        }

        public DbSet<Customer> Customers {get;set;}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
