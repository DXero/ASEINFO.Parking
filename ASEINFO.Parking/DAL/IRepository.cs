using System.Linq.Expressions;

namespace ASEINFO.Parking.DAL
{
    public interface IRepository : IDisposable 
    {
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<T?> GetById<T>(int id, params String[] incluir) where T : class;

        Task<bool> Exists<T>(Expression<Func<T, bool>> criteria) where T : class;

        Task<Result> Add<T>(T entity) where T : class;
        Task<Result> Update<T>(T entity) where T : class;
        Task<Result> Delete<T>(int id) where T : class;
    }
}
