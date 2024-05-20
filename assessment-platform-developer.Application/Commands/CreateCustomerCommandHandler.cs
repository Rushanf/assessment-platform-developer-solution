using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Common.Mapper;
using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;

namespace assessment_platform_developer.Application.Commands
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly IWriteRepository<Customer> _customerWriteRepository;

        public CreateCustomerCommandHandler(IWriteRepository<Customer> customerWriteRepository)
        {
            _customerWriteRepository = customerWriteRepository;
        }

        public void Handle(CreateCustomerCommand command)
        {
            var customer = Mapper.ConvertToCustomer(command);
            _customerWriteRepository.Add(customer);
        }
    }
}
