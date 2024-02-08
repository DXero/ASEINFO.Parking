namespace ASEINFO.Parking.DAL
{
    public class Result
    {
        public enum Type { Error = 0, Success = 1, Warning = 2, NotFound = 3, Duplicate = 4, NoContent = 5};

        public Type Code { get; set; }

        public String Message { get; set; }

        public Object Objeto { get; set; }
    }
}
