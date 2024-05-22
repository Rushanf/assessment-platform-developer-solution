using assessment_platform_developer.Application.Commands;
using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Queries;
using assessment_platform_developer.Services.Interfaces;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace assessment_platform_developer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly Container _container;

        public CustomerService(Container container)
        {
            _container = container;
        }

        public List<CustomerBasicResponse> GetAllCustomers()
        {
            ICustomerQueryService customerQueryService = new CustomerQueryService(_container);
            return customerQueryService.GetAllCustomers().ToList();
        }

        public CustomerResponse GetCustomer(int id)
        {
            ICustomerQueryService customerQueryService = new CustomerQueryService(_container);
            return customerQueryService.GetCustomer(id);
        }

        public void CreateCustomer(CreateCustomerCommand command)
        {
            ICustomerCommandService customerCommandService = new CustomerCommandService(_container);
            customerCommandService.CreateCustomer(command);
        }

        public void UpdateCustomer(UpdateCustomerCommand command)
        {
            ICustomerCommandService customerCommandService = new CustomerCommandService(_container);
            customerCommandService.UpdateCustomer(command);
        }

        public void DeleteCustomer(DeleteCustomerCommand command)
        {
            ICustomerCommandService customerCommandService = new CustomerCommandService(_container);
            customerCommandService.DeleteCustomer(command);
        }
    }
}