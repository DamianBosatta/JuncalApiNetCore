namespace JuncalApi.Dto.DtoRespuesta
{
    public class OrdenMaterialInternoRecogidoRespuesta
    {
        public int Id { get; set; }

        public int IdOrdenInterno { get; set; }

        public int IdMaterial { get; set; }

        public decimal Peso { get; set; }

        public string? NombreMaterial { get; set; }

    }
}
