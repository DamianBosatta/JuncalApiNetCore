namespace JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido
{
    public class Referencia
    {
        public int IdOrden { get; set; }

        public List<int> MaterialesEnviados { get; set; }= new List<int>();
    }
}
