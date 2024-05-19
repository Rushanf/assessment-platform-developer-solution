using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Common.Mapper;
using assessment_platform_developer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace assessment_platform_developer.Application.Queries
{
    public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        public GetCustomerQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public CustomerResponse Handle(GetCustomerQuery query)
        {
            var result = _customerRepository.Get(query.ID);
            return Mapper.ConvertToCustomerQueryResult(result);
        }
    }
}
