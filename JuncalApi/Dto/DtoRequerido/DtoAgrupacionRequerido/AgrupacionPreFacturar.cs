namespace JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido
{
    public class AgrupacionPreFacturar
    {
        public int IdAceria { get; set; }

        public int IdContrato { get; set; }

        

        public List<Agrupacion> agrupacion { get; set; }= new List<Agrupacion>();

        public List<Referencia> referencia { get; set; } = new List<Referencia>();
    }
}
