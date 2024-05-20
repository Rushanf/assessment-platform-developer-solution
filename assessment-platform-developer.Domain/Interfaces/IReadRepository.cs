using assessment_platform_developer.Domain.Entities;
using System.Collections.Generic;

namespace assessment_platform_developer.Domain.Interfaces
{
    public interface IReadRepository<T> where T : class
    { 
        IEnumerable<T> GetAll();
		T Get(int id);
    }
}
