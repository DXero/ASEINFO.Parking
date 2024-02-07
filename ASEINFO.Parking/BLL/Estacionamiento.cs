using ASEINFO.Parking.DAL;
using ASEINFO.Parking.Models;


namespace ASEINFO.Parking.BLL
{
    public class Estacionamiento
    {
        private readonly IRepository logica;
        public Estacionamiento(IRepository logica)
        {
            this.logica = logica;
        }
        public async Task<IEnumerable<Vehiculo>> DarDeAltaVehiculoOficial(String numero){
            //var context = Repository();
            /*using(var context = AppDbContext())
            {
                return await context.GetAll<Vehiculo>();
            }*/
            

            return await logica.GetAll<Vehiculo>();
            
        }
    }
}
