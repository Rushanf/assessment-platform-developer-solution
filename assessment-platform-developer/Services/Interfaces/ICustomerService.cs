using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Services.Interfaces
{
    //Interface segragation
    public interface ICustomerService : ICustomerCommandService, ICustomerQueryService
    {
    }
}
