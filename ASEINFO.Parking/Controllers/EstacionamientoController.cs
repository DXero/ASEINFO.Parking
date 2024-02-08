using ASEINFO.Parking.BLL;
using ASEINFO.Parking.DAL;
using ASEINFO.Parking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASEINFO.Parking.Controllers
{
    [ApiController]
    [Route("api/estacionamiento")]
    public class EstacionamientoController : Controller
    {
        private readonly Estacionamiento estacionamiento;
        private readonly AppDbContext _context;
        public EstacionamientoController(AppDbContext context)
        {
            estacionamiento = new Estacionamiento(context);
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> All()
        {
            //var listado = _context.Vehiculos.ToList();
            var listado = await estacionamiento.DarDeAltaVehiculoOficial("P909505");
            
            //var listado = await db.GetAll<Vehiculo>();

            return Ok(listado);
        }
    }
}
