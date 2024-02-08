using ASEINFO.Parking.DAL;
using ASEINFO.Parking.Models;
using Microsoft.EntityFrameworkCore;


namespace ASEINFO.Parking.BLL
{
    public class Estacionamiento
    {
        private readonly IRepository repository;
        public Estacionamiento(DbContext context)
        {
            repository = Factory.GetRepository(context);
        }
        public async Task<Vehiculo/*IEnumerable<Vehiculo>*/> DarDeAltaVehiculoOficial(String placa){

            //return await repository.GetAll<Vehiculo>();

            var r = await repository.Get<Vehiculo>(x => x.VehiculoId==1);

            return r;
            
        }
    }
}
