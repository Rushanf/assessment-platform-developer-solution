using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Common.Mapper;
using assessment_platform_developer.Domain.Interfaces;

namespace assessment_platform_developer.Application.Commands
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Handle(UpdateCustomerCommand command)
        {
            var customer = Mapper.ConvertToCustomer(command); 
            _customerRepository.Update(customer);
        }
    }
}
