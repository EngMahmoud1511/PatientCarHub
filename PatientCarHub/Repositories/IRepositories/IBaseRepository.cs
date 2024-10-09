using System.Linq.Expressions;

namespace PatientCarHub.Repositories.IRepositories
{

    public interface IBaseRepository<T> where T : class
    {

        T Update(T entity);
        Task<T> Add(T entity);
        T HardDelete(T entity);
        T SoftDelete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
    }

}
