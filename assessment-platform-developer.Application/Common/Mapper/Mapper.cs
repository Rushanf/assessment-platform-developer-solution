using assessment_platform_developer.Application.Commands;
using assessment_platform_developer.Application.Queries;
using assessment_platform_developer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace assessment_platform_developer.Application.Common.Mapper
{
    public static class Mapper
    {
        public static Customer ConvertToCustomer(CustomerBasicCommand command)
        {
            return new Customer
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
        }

        public static CustomerResponse ConvertToCustomerQueryResult(Customer customer)
        {
            return new CustomerResponse
            {
                Name = customer.Name,
                City = customer.City,
                Address = customer.Address,  
                ContactEmail = customer.ContactEmail,
                ContactName = customer.ContactName,
                ContactNotes = customer.ContactNotes,
                ContactPhone = customer.ContactPhone,
                ContactTitle = customer.ContactTitle,
                Country = customer.Country,
                Email = customer.Email,
                ID = customer.ID,
                Notes = customer.Notes,
                Phone = customer.Phone,
                State = customer.State,
                Zip = customer.Zip   
            };
        }
    }
}
