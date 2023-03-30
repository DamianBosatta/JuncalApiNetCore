namespace JuncalApi.Dto.DtoRequerido
{
    public class UsuarioRequerido
    {
        public string Usuario { get; set; } = string.Empty;
       
        public string? Password { get; set; } = String.Empty;

        public string Nombre { get; set; } = string.Empty ;

        public string Apellido { get; set; } = string.Empty;

        public int Dni { get; set; }

        public string Email { get; set; } = string.Empty;
       
        public int? IdRol { get; set; }

    }
}
