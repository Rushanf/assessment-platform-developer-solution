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
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly Container _container;

        public CustomerQueryService(Container container)
        {
            _container = container;
        }

        public List<CustomerBasicResponse> GetAllCustomers()
        {
            IQueryHandler<GetAllCustomersQuery, List<CustomerBasicResponse>> queryHandler = _container.GetInstance<IQueryHandler<GetAllCustomersQuery, List<CustomerBasicResponse>>>();
            return queryHandler.Handle(new GetAllCustomersQuery());
        }

        public CustomerResponse GetCustomer(int id)
        {
            IQueryHandler<GetCustomerQuery, CustomerResponse> queryHandler = _container.GetInstance<IQueryHandler<GetCustomerQuery, CustomerResponse>>();
            return queryHandler.Handle(new GetCustomerQuery { ID = id });
        }
    }
}