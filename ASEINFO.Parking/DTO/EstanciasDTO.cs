namespace ASEINFO.Parking.DTO
{
    public class EstanciasDTO
    {
        public String Placa { get; set; }

        public String TipoVehiculo { get; set; }

        public DateTime Entrada { get; set; }

        public DateTime? Salida { get; set; }

        public int? Minutos { get; set; }

        public decimal Pago { get; set; }

        public bool Activo { get; set; }

    }
}
