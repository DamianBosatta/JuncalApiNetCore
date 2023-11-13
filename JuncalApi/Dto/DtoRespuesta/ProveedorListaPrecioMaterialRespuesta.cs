namespace JuncalApi.Dto.DtoRespuesta
{
    public class ProveedorListaPrecioMaterialRespuesta
    {
        public int Id { get; set; }
        public int? IdProveedorListaprecios { get; set; }

        public int? IdMaterialJuncal { get; set; }

        public string? Nombre { get; set; }

        public decimal? Precio { get; set; }
        
    }
}
