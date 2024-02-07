

using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ASEINFO.Parking.DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Set<T>().FindAsync(id) is null ? false : true;
        }

        public async Task<Result> Add(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                int r = await _context.SaveChangesAsync();
                if(r > 0)
                {
                    return new Result() { Code = Result.Type.Success, Message = "Guardado exitosamente" };
                }
                else
                {
                    return new Result() { Code = Result.Type.Warning, Message = "No se pudo almacenar" };
                }
            }
            catch (Exception ex)
            {
                return new Result() { Code = Result.Type.Error, Message = ex.Message.ToString() };
            }
        }

        public async Task<Result> Update(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                int r = await _context.SaveChangesAsync();

                if (r > 0)
                {
                    return new Result() { Code = Result.Type.Success, Message = "Modificado exitosamente" };
                }
                else
                {
                    return new Result() { Code = Result.Type.Warning, Message = "No se pudo modificar" };
                }
            }
            catch (Exception ex)
            {
                return new Result() { Code = Result.Type.Error, Message = ex.Message.ToString() };
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    int r = await _context.SaveChangesAsync();

                    if (r > 0)
                    {
                        return new Result() { Code = Result.Type.Success, Message = "Eliminado exitosamente" };
                    }
                    else
                    {
                        return new Result() { Code = Result.Type.Warning, Message = "No se pudo eliminar" };
                    }
                }
                else
                    return new Result() { Code = Result.Type.NotFound, Message = "No se encontro la entidad" };

            }
            catch (Exception ex)
            {
                return new Result() { Code = Result.Type.Error, Message = ex.Message.ToString() };
            }
        }
        
    }
}
