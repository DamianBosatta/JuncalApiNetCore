namespace JuncalApi.Dto.DtoRequerido
{
    public class ChoferRequerido
    {
        public string Nombre { get; set; } = string.Empty;

        public string Apellido { get; set; } = string.Empty;

        public int? Dni { get; set; }

        public string? Telefono { get; set; }

        public ChoferRequerido(string nombre, string apellido, int? dni, string? telefono)
        {
            Nombre = nombre is null ? string.Empty:nombre;
            Apellido = apellido is null ? string.Empty:apellido;
            Dni = dni ==0? null : dni;
            Telefono = telefono is null ? string.Empty:telefono;
        }
    }
}
