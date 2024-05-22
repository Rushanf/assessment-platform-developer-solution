using assessment_platform_developer.Application.Commands;
using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Services.Interfaces;
using SimpleInjector;

namespace assessment_platform_developer.Services
{
    public class CustomerCommandService : ICustomerCommandService
    {
        private readonly Container _container;

        public CustomerCommandService(Container container)
        {
            _container = container;
        }
        public void CreateCustomer(CreateCustomerCommand command)
        {
            ICommandHandler<CreateCustomerCommand> commandHandler = _container.GetInstance<ICommandHandler<CreateCustomerCommand>>();
            commandHandler.Handle(command);
        }

        public void UpdateCustomer(UpdateCustomerCommand command)
        {
            ICommandHandler<UpdateCustomerCommand> commandHandler = _container.GetInstance<ICommandHandler<UpdateCustomerCommand>>();
            commandHandler.Handle(command);
        }

        public void DeleteCustomer(DeleteCustomerCommand command)
        {
            ICommandHandler<DeleteCustomerCommand> commandHandler = _container.GetInstance<ICommandHandler<DeleteCustomerCommand>>();
            commandHandler.Handle(command);
        }
    }
}