namespace JuncalApi.Dto.DtoRespuesta
{
    public class LoginRespuesta
    {

        public int? Id { get; set; }

        public string? Usuario { get; set; } = null!;

        public string? Nombre { get; set; } = null!;

        public string? Apellido { get; set; } = null!;

        public int? Dni { get; set; }

        public string? Email { get; set; } = null!;

        public string? Token { get; set; } 

        public int? IdRol { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? TokenCreated { get; set; }

        public DateTime? TokenExpires { get; set; }





    }
}
