using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Common.Mapper;
using assessment_platform_developer.Domain.Entities;
using assessment_platform_developer.Domain.Interfaces;

namespace assessment_platform_developer.Application.Commands
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
    {
        private readonly IWriteRepository<Customer> _customerWriteRepository;

        public UpdateCustomerCommandHandler(IWriteRepository<Customer> customerWriteRepository)
        {
            _customerWriteRepository = customerWriteRepository;
        }

        public void Handle(UpdateCustomerCommand command)
        {
            var customer = Mapper.ConvertToCustomer(command); 
            _customerWriteRepository.Update(customer);
        }
    }
}
