namespace JuncalApi.Dto.DtoRequerido
{
    public class EstadoRequerido
    {       
        public string Nombre { get; set; } = string.Empty;

        public EstadoRequerido(string nombre)
        {
            Nombre = nombre is null ? string.Empty:nombre;
        }
    }
}
