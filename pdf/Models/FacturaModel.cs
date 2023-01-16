namespace pdf.Models
{
    public class FacturaModel
    {
        public string Cliente { get; set; }
        public int IdCliente { get; set; }
        public string DireccionCliente { get; set; }
        public string CiudadCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public DateTime FechaVence { get; set; }
        public DateTime FechaDePago { get; set; }
        public string Plazo { get; set; }
 
    }
}
