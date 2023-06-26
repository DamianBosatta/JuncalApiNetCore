namespace JuncalApi.Dto.DtoRespuesta
{
    public class AcopladoRespuesta
    {
        public int Id { get; set; }

        public string? Patente { get; set; } 

        public string? Marca { get; set; }

        public string? Año { get; set; }

        public int IdTipo { get; set; }

        public string? TipoAcoplado { get; set; }
    }
}
