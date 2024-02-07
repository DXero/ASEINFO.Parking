using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASEINFO.Parking.Models
{
    public class Estancia
    {
        [Key]
        public int EstanciaId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime Entrada { get; set; } = DateTime.Now;

        [Column(TypeName = "datetime")]
        public DateTime? Salida { get; set; }

        public int? Minutos { 
            get { 
                return Salida is null ? null : (int)(Salida - Entrada).Value.TotalMinutes;
            } 
        }

        [Column(TypeName = "money")]
        public decimal Pago { get {
                return Minutos is null ? 0 : Minutos * Vehiculo.TipoVehiculo.Precio ?? 0;    
            }
        }

        public bool Activo { get; set; }

        [StringLength(20)]
        public required String VehiculoId { get; set; }

        public Vehiculo Vehiculo { get; set; }
    }
}
