using assessment_platform_developer.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Application.Common
{
    public interface IQueryHandler<Query, ResultType>
    {
        ResultType Handle(Query query);
    }
}
