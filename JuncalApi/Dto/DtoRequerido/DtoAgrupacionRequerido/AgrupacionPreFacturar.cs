namespace JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido
{
    public class AgrupacionPreFacturar
    {
        public int IdAceria { get; set; }

        public int IdContrato { get; set; }

        public List<Agrupacion> ListaAgrupacion { get; set; }= new List<Agrupacion>();

        public List<Referencia> ListaReferencia { get; set; } = new List<Referencia>();
    }
}
