using assessment_platform_developer.Application.Common.Mapper;
using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace assessment_platform_developer.Application.Commands
{
    public class DeleteCustomerCommandHandler: ICommandHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Handle(DeleteCustomerCommand command)
        {
            _customerRepository.Delete(command.ID);
        }
    }
}
