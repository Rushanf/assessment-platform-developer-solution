﻿using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;

namespace assessment_platform_developer.Application.Commands
{
    public class DeleteCustomerCommandHandler: ICommandHandler<DeleteCustomerCommand>
    {
        private readonly IWriteRepository<Customer> _customerWriteRepository;

        public DeleteCustomerCommandHandler(IWriteRepository<Customer> customerWriteRepository)
        {
            _customerWriteRepository = customerWriteRepository;
        }

        public void Handle(DeleteCustomerCommand command)
        {
            _customerWriteRepository.Delete(command.ID);
        }
    }
}
