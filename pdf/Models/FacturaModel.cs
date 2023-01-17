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
        public string FormaDePago { get; set; }

        public string producto { get; set; }
        public string cantidad { get; set;}
        public string vlrUnitario { get; set;}
        public string subtotal { get; set; }
        public string iva { get; set; }
        public string totalPro { get; set; }


    }
}
