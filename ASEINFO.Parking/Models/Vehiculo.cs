using System.ComponentModel.DataAnnotations;

namespace ASEINFO.Parking.Models
{
    public class Vehiculo
    {
        [Key]
        [StringLength(20, ErrorMessage = "El campo no puede exceder 20 caracteres")]
        public required String VehiculoId { get; set; }// Se guarda el numero de placa

        [Required]
        public int TipoVehiculoId { get; set; }

        public required TipoVehiculo TipoVehiculo { get; set; }

        public List<Estancia> Estancias { get; set; }
    }
}
