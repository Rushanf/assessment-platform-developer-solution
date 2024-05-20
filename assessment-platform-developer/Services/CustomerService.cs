using assessment_platform_developer.Application.Commands;
using assessment_platform_developer.Application.Common;
using assessment_platform_developer.Application.Queries;
using SimpleInjector;
using System.Collections.Generic;

namespace assessment_platform_developer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly Container _container;

        public CustomerService(Container container)
        {
            _container = container;
        }

        public List<CustomerBasicResponse> GetAllCustomers()
        {
            var queryHandler = _container.GetInstance<IQueryHandler<GetAllCustomersQuery, List<CustomerBasicResponse>>>();
            return queryHandler.Handle(new GetAllCustomersQuery());
        }

        public CustomerResponse GetCustomer(int id)
        {
            var queryHandler = _container.GetInstance<IQueryHandler<GetCustomerQuery, CustomerResponse>>();
            return queryHandler.Handle(new GetCustomerQuery { ID = id });
        }

        public void CreateCustomer(CreateCustomerCommand command)
        {
            var commandHandler = _container.GetInstance<ICommandHandler<CreateCustomerCommand>>();
            commandHandler.Handle(command);
        }

        public void UpdateCustomer(UpdateCustomerCommand command)
        {
            var commandHandler = _container.GetInstance<ICommandHandler<UpdateCustomerCommand>>();
            commandHandler.Handle(command);
        }

        public void DeleteCustomer(DeleteCustomerCommand command)
        {
            var commandHandler = _container.GetInstance<ICommandHandler<DeleteCustomerCommand>>();
            commandHandler.Handle(command);
        }
    }
}