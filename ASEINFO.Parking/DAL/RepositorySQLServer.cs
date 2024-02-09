

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace ASEINFO.Parking.DAL
{
    public class RepositorySQLServer : IRepository
    {
        private readonly DbContext _context;
        public RepositorySQLServer(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        // Errores que se pueden producir: Listado vacio, Exception
        public async Task<Result> GetAll<T>(params String[] incluir) where T : class
        {
            try
            {
                var lista = await _context.Set<T>().ToListAsync();

                

                if (lista.Count == 0)
                {
                    return new Result()
                    {
                        Code = Result.Type.NoContent,
                        Message = "Listado vacio",
                        Objeto = lista
                    };
                }

                if (incluir.Length == 0)
                {
                    return new Result()
                    {
                        Code = Result.Type.Success,
                        Message = "Listado con datos",
                        Objeto = await _context.Set<T>().ToListAsync()
                    };
                }
                else
                {
                    var temp = _context.Set<T>().Include(incluir[0]);
                    for (int i = 1; i < incluir.Length; i++)
                    {
                        temp = temp.Include(incluir[i]);
                    }

                    return new Result()
                    {
                        Code = Result.Type.Success,
                        Message = "Listado con datos con datos relacionados",
                        Objeto = await temp.ToListAsync()
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result() { Code = Result.Type.Error, Message = ex.Message };
            }
            
        }

        public async Task<Result> GetAll<T>(Expression<Func<T, bool>> criterio, params String[] incluir) where T : class
        {
            try
            {
                if (incluir.Length == 0)
                {
                    return new Result()
                    {
                        Code = Result.Type.Success,
                        Message = "Entidad encontrada",
                        Objeto = await _context.Set<T>().Where(criterio).ToListAsync()
                    };
                }
                else
                {
                    var temp = _context.Set<T>().Include(incluir[0]);
                    for (int i = 1; i < incluir.Length; i++)
                    {
                        temp = temp.Include(incluir[i]);
                    }

                    return new Result()
                    {
                        Code = Result.Type.Success,
                        Message = "Entidad encontrada con datos relacionados",
                        Objeto = await temp.Where(criterio).ToListAsync()
                    };
                }
                
            }
            catch (Exception ex)
            {
                return new Result() { Code = Result.Type.Error, Message = ex.Message };
            }

        }

        // Errores que se pueden producir: La busqueda no existe, Exception
        public async Task<Result> Get<T>(Expression<Func<T, bool>> criterio, params String[] incluir) where T : class
        {
            try
            {
                bool existe = await _context.Set<T>().AnyAsync(criterio);
                if (!existe)
                    return new Result() { Code = Result.Type.NotFound, Message = "Entidad no existe" };
                else if (incluir.Length == 0)
                {
                    return new Result()
                    {
                        Code = Result.Type.Success,
                        Message = "Entidad encontrada",
                        Objeto = await _context.Set<T>().FirstAsync(criterio)
                    };
                }
                else
                {
                    var temp = _context.Set<T>().Include(incluir[0]);
                    for (int i = 1; i < incluir.Length; i++)
                    {
                        temp = temp.Include(incluir[i]);
                    }

                    return new Result()
                    {
                        Code = Result.Type.Success,
                        Message = "Entidad encontrada con datos relacionados",
                        Objeto = await temp.FirstAsync(criterio)
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result() { Code = Result.Type.Error, Message = ex.Message };
            }
            
        }

        public async Task<bool> Exists<T>(Expression<Func<T, bool>> criterio) where T : class
        {
            return await _context.Set<T>().AnyAsync(criterio);
        }

        // Errores que se pueden producir: Exception
        public async Task<Result> Add<T>(T entity) where T : class
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                int r = await _context.SaveChangesAsync();
                if(r > 0)
                {
                    return new Result() { Code = Result.Type.Success, Message = "Guardado exitosamente", Objeto = entity };
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

        // Errores que se pueden producir: Exception
        public async Task<Result> Update<T>(T entity) where T : class
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                int r = await _context.SaveChangesAsync();

                if (r > 0)
                {
                    return new Result() { Code = Result.Type.Success, Message = "Modificado exitosamente", Objeto = entity };
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

        // Errores que se pueden producir: Exception
        public async Task<Result> Delete<T>(int id) where T : class
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
                        return new Result() { Code = Result.Type.Success, Message = "Eliminado exitosamente", Objeto = entity };
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
