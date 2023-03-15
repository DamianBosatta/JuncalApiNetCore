namespace JuncalApi.Dto.DtoRequerido
{
    public class MaterialProveedorRequerido
    {
        public int IdMaterial { get; set; }

        public int IdProveedor { get; set; }

        public decimal? Precio { get; set; }

        public DateTime Fecha { get; set; }

        public bool? Activo { get; set; }
    }
}
