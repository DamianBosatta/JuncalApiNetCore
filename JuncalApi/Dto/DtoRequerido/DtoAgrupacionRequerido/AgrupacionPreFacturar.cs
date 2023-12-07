namespace JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido
{
    public class AgrupacionPreFacturar
    {
        public int IdAceria { get; set; } = 0;

        public int IdContrato { get; set; } = 0;

        public string num_factura { get; set; } = string.Empty;

        public int idUsuario { get; set; } = 0;

        public List<Agrupacion> agrupacion { get; set; }= new List<Agrupacion>();

        public List<Referencia> referencia { get; set; } = new List<Referencia>();

        public Factura factura { get; set; } = new Factura();

        public List<FacturarOrdenRequerido> ListFacturarOrdenRequeridos { get; set; } = new List<FacturarOrdenRequerido>(); 

    }
}
