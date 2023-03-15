namespace JuncalApi.Dto.DtoRespuesta
{
    public class MaterialProveedorRespuesta
    {
        public int Id { get; set; }

        public int IdMaterial { get; set; }

        public int IdProveedor { get; set; }

        public decimal? Precio { get; set; }

        public DateTime Fecha { get; set; }

        public bool? Activo { get; set; }
    }
}
