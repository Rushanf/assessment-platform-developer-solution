using assessment_platform_developer.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Services.Interfaces
{
    public interface ICustomerQueryService
    {
        List<CustomerBasicResponse> GetAllCustomers();
        CustomerResponse GetCustomer(int id);
    }
}
