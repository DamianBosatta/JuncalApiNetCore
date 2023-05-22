namespace JuncalApi.Dto.DtoRespuesta
{
    public class AceriaMaterialRespuesta
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public int IdAceria { get; set; }

        public int IdMaterial { get; set; }

        public string Cod { get; set; } = null!;

        public string? MaterialDescripcion { get; set; }
    }
}
