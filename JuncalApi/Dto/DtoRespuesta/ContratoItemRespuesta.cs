namespace JuncalApi.Dto.DtoRespuesta
{
    public class ContratoItemRespuesta
    {
        public int Id { get; set; }

        public int IdContrato { get; set; }

        public int IdMaterial { get; set; }

        public decimal Precio { get; set; }

        public string? MaterialDescripcion { get; set; }
    }
}
