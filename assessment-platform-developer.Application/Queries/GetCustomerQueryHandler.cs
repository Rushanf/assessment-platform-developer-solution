using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Common.Mapper;
using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace assessment_platform_developer.Application.Queries
{
    public class GetCustomerQueryHandler : IQueryHandler<GetCustomerQuery, CustomerResponse>
    {
        private readonly IReadRepository<Customer> _customerReadRepository;
        public GetCustomerQueryHandler(IReadRepository<Customer> customerReadRepository)
        {
            _customerReadRepository = customerReadRepository;
        }

        public CustomerResponse Handle(GetCustomerQuery query)
        {
            var result = _customerReadRepository.Get(query.ID);
            return Mapper.ConvertToCustomerQueryResult(result);
        }
    }
}
