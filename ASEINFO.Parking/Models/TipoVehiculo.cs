using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASEINFO.Parking.Models
{
    public class TipoVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TipoVehiculoId { get; set; }

        [StringLength(75, ErrorMessage = "El maximo son 75 caracteres")]
        public required String Descripcion { get; set; }

        [Column(TypeName="money")]
        public decimal? Precio { get; set; }

        public List<Vehiculo>? Vehiculos { get; set; }
    }
}
