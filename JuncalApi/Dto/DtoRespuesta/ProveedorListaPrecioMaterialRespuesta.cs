namespace JuncalApi.Dto.DtoRespuesta
{
    public class ProveedorListaPrecioMaterialRespuesta
    {
        public int Id { get; set; }
        public int? IdProveedorListaprecios { get; set; }

        public int? IdMaterialJuncal { get; set; }

        public string? Nombre { get; set; }

        public decimal? Precio { get; set; }

        public string? NombreMaterial { get; set; } = string.Empty;

        public string? NombreListaPrecio { get; set; } = string.Empty;
    }
}
