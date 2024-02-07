namespace ASEINFO.Parking.DAL
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);

        Task<bool> Exists(int id);

        Task<Result> Add(T entity);
        Task<Result> Update(T entity);
        Task<Result> Delete(int id);
    }
}
