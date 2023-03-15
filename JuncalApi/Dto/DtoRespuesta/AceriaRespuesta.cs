namespace JuncalApi.Dto.DtoRespuesta
{
    public class AceriaRespuesta
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? Cuit { get; set; }

        public string? CodProveedor { get; set; }
    }
}
