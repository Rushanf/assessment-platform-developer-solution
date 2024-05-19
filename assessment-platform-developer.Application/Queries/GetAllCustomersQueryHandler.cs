using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Application.Queries
{
    public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, List<CustomerBasicResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<CustomerBasicResponse> Handle(GetAllCustomersQuery query)
        {
            var result = _customerRepository.GetAll();
            List<CustomerBasicResponse> customers = new List<CustomerBasicResponse>(); 

            foreach(var customer  in result)
            {
                CustomerBasicResponse cust = new CustomerBasicResponse();
                cust.Name = customer.Name;
                cust.ID = customer.ID;               
                customers.Add(cust);
            }
            return customers;
        }
    }
}
