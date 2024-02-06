namespace ASEINFO.Parking.Clases
{
    public abstract class Vehiculo
    {
        public String Placa { get; set; }

        public Vehiculo(String placa)
        {
            this.Placa = placa;
        }
    }
}
