namespace JuncalApi.Dto.DtoRespuesta
{
    public class ProveedorRespuesta
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Origen { get; set; }
    }
}
