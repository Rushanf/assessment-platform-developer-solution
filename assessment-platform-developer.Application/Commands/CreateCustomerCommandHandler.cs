using assessment_platform_developer.Application.Common;
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
            var customer = new Customer
            {
                Name = command.Name,
                City = command.City,
                Address = command.Address,  
                ContactEmail = command.ContactEmail,
                ContactName = command.ContactName,
                ContactNotes = command.ContactNotes,
                ContactPhone = command.ContactPhone,
                ContactTitle = command.ContactTitle,
                Country = command.Country,
                Email = command.Email,
                ID = command.ID,
                Notes = command.Notes,
                Phone = command.Phone,
                State = command.State,
                Zip = command.Zip   
            };

            _customerRepository.Add(customer);
        }
    }
}
