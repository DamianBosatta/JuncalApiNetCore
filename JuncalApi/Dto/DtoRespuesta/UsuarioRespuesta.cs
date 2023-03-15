namespace JuncalApi.Dto.DtoRespuesta
{
    public class UsuarioRespuesta
    {
        public int Id { get; set; }

        public string Usuario { get; set; } = null!;  

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public int Dni { get; set; }

        public string Email { get; set; } = null!;
   
        public int? IdRol { get; set; }

    }
}
