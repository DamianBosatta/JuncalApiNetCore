namespace JuncalApi.Dto.DtoRequerido
{
    public class ProveedorRequerido
    {       
        public string Nombre { get; set; } = string.Empty;

        public string? Origen { get; set; }

        public ProveedorRequerido(string nombre, string? origen)
        {
            Nombre = nombre is null ? string.Empty: nombre;
            Origen = origen is null ? string.Empty: origen;
        }
    }
}
