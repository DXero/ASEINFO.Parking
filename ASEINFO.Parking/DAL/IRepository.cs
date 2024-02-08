using System.Linq.Expressions;

namespace ASEINFO.Parking.DAL
{
    public interface IRepository : IDisposable 
    {
        Task<IEnumerable<T>> GetAll<T>() where T : class;
        Task<T?> Get<T>(Expression<Func<T, bool>> criterio, params String[] incluir) where T : class;

        Task<bool> Exists<T>(Expression<Func<T, bool>> criterio) where T : class;

        Task<Result> Add<T>(T entity) where T : class;
        Task<Result> Update<T>(T entity) where T : class;
        Task<Result> Delete<T>(int id) where T : class;
    }
}
