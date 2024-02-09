using ASEINFO.Parking.BLL;
using ASEINFO.Parking.DAL;
using ASEINFO.Parking.DTO;
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
            _context = context;
            estacionamiento = new Estacionamiento(context);      
        }

        [HttpGet("All")]
        public async Task<ActionResult<List<Estancia>>> Obtener()
        {
            return await estacionamiento.ListarEstancias();

        }

        [HttpPost("RegistrarEntrada")]
        public async Task<ActionResult<String>> RegistrarEntrada(String placa)
        {
            var respuesta = await estacionamiento.RegistrarEntrada(placa);

            if(respuesta.Code  == Result.Type.Success)
            {
                return Ok(respuesta.Message);
            }
            else
            {
                return BadRequest(respuesta.Message);
            }
        }

        [HttpPost("RegistrarSalida")]
        public async Task<ActionResult<String>> RegistrarSalida(String placa)
        {
            var respuesta = await estacionamiento.RegistrarSalida(placa);

            if (respuesta.Code == Result.Type.Success)
            {
                return Ok(respuesta.Message);
            }
            else
            {
                return BadRequest(respuesta.Message);
            }
        }

        [HttpPost("AltaVehiculoOficial")]
        public async Task<ActionResult<String>> DarAltaVehiculoOficial(String placa)
        {
            var respuesta = await estacionamiento.DarDeAltaVehiculoOficial(placa);

            if(respuesta.Code == Result.Type.Success)
            {
                return Ok($"Vehiculo Oficia con placa {placa} agregado exitosamente");
            }
            else if(respuesta.Code == Result.Type.Duplicate)
            {
                return BadRequest($"El vehiculo no pudo ser creado debido a que la placa {placa} ya existe");
            }
            else
            {
                return Conflict(respuesta.Message);
            }
            
        }

        [HttpPost("AltaVehiculoResidente")]
        public async Task<ActionResult<String>> DarAltaVehiculoResidente(String placa)
        {
            var respuesta = await estacionamiento.DarDeAltaVehiculoResidente(placa);

            if (respuesta.Code == Result.Type.Success)
            {
                return Ok($"Vehiculo Residente con placa {placa} agregado exitosamente");
            }
            else if (respuesta.Code == Result.Type.Duplicate)
            {
                return BadRequest($"El vehiculo no pudo ser creado debido a que la placa {placa} ya existe");
            }
            else
            {
                return Conflict(respuesta.Message);
            }

        }

        [HttpPut("ComienzaMes")]
        public async Task<ActionResult<String>> ComienzaMes()
        {
            var respuesta = await estacionamiento.ComienzaMes();

            if (respuesta.Code == Result.Type.Success)
            {
                return Ok(respuesta.Message);
            }
            else
            {
                return BadRequest(respuesta.Message);
            }
        }


        [HttpGet("PagosResidentes")]
        public async Task<ActionResult<String>> PagosResidentes()
        {
            var respuesta = await estacionamiento.PagosResidentes();

            if (respuesta.Code == Result.Type.Success)
            {
                return Ok((List<PagosResidentesDTO>)respuesta.Objeto);
            }
            else
            {
                return BadRequest(respuesta.Message);
            }
        }
    }
}
