﻿using System.ComponentModel.DataAnnotations;
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
                return Salida is null ? (decimal)0 : Minutos * Vehiculo.TipoVehiculo.Precio ?? (decimal)0.00;
            }
        }

        public bool Activo { get; set; } = true;

        
        public int VehiculoId { get; set; }

        public required Vehiculo Vehiculo { get; set; }
    }
}
