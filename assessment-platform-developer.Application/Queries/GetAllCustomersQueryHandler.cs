using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Application.Queries
{
    public class GetAllCustomersQueryHandler : IQueryHandler<GetAllCustomersQuery, List<CustomerResponse>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<CustomerResponse> Handle()
        {
            var result = _customerRepository.GetAll();
            List<CustomerResponse> customers = new List<CustomerResponse>(); 

            foreach(var customer  in result)
            {
                CustomerResponse cust = new CustomerResponse();
                cust.Name = customer.Name;
                //cust.Email = customer.Email;
                //cust.Phone = customer.Phone;    
                //cust.City = customer.City;  
                //cust.Country = customer.Country;
               
                customers.Add(cust);
            }
            return customers;
        }
    }
}
