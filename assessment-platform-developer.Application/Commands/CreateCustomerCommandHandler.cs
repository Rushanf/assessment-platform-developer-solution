using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Common.Mapper;
using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Application.Commands
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Handle(CreateCustomerCommand command)
        {
            var customer = Mapper.ConvertToCustomer(command);
            _customerRepository.Add(customer);
        }
    }
}
