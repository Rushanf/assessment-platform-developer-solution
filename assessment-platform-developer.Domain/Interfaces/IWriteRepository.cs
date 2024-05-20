

namespace assessment_platform_developer.Domain.Interfaces
{
    public interface IWriteRepository<T> where T : class
    {
        void Add(T t);
		void Update(T t);
		void Delete(int id);
    }
}
