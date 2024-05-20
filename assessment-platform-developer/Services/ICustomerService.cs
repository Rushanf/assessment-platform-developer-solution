using assessment_platform_developer.Application.Commands;
using assessment_platform_developer.Application.Queries;
using System.Collections.Generic;

namespace assessment_platform_developer.Services
{
    internal interface ICustomerService
    {
        List<CustomerBasicResponse> GetAllCustomers();
        CustomerResponse GetCustomer(int id);
        void CreateCustomer(CreateCustomerCommand command);
        void UpdateCustomer(UpdateCustomerCommand command);
        void DeleteCustomer(DeleteCustomerCommand command);
    }
}
