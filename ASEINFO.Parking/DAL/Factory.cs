using Microsoft.EntityFrameworkCore;

namespace ASEINFO.Parking.DAL
{
    public class Factory
    {
        public static IRepository GetRepository(DbContext context)
        {
            return new RepositorySQLServer(context);
        }
    }
}
