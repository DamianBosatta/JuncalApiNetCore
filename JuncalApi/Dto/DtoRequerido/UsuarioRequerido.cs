namespace JuncalApi.Dto.DtoRequerido
{
    public class UsuarioRequerido
    {
        public string? Usuario { get; set; } = string.Empty;
       
        public string? Password { get; set; } = String.Empty;

        public string? Nombre { get; set; } = string.Empty ;

        public string? Apellido { get; set; } = string.Empty;

        public int? Dni { get; set; }

        public string? Email { get; set; } = string.Empty;
       
        public int? IdRol { get; set; }

        public UsuarioRequerido(string? usuario, string? password, string? nombre, string? apellido,
        int? dni, string? email, int? idRol)
        {
            Usuario = usuario is null ? string.Empty:usuario;
            Password = password is null ? string.Empty:password;
            Nombre = nombre is null ? string.Empty:nombre;
            Apellido = apellido is null ? string.Empty:apellido;
            Dni = dni==0?null:dni;
            Email = email is null ? string.Empty:email;
            IdRol = idRol==0? null :idRol;
        }
    }
}
