using ASEINFO.Parking.BLL;
using ASEINFO.Parking.DAL;
using ASEINFO.Parking.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASEINFO.Parking.Controllers
{
    [ApiController]
    [Route("api/estacionamiento")]
    public class EstacionamientoController : Controller
    {
        //private readonly AppDbContext _context;
        /*private readonly IRepository db;
        public EstacionamientoController(AppDbContext context)
        {
            //_context = context;
            db = new RepositorySQLServer(context);
        }*/

        private readonly Estacionamiento estacionamiento;
        public EstacionamientoController(IRepository logica)
        {
            //this.logica = logica;
            estacionamiento = new Estacionamiento(logica);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> All()
        {
            //var listado = _context.Vehiculos.ToList();
            var listado = await estacionamiento.DarDeAltaVehiculoOficial("P202");
            
            //var listado = await db.GetAll<Vehiculo>();

            return Ok(listado);
        }
    }
}
