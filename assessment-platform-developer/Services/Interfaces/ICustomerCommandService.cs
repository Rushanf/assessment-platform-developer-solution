using assessment_platform_developer.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Services.Interfaces
{
    public interface ICustomerCommandService
    {
        void CreateCustomer(CreateCustomerCommand command);
        void UpdateCustomer(UpdateCustomerCommand command);
        void DeleteCustomer(DeleteCustomerCommand command);
    }
}
