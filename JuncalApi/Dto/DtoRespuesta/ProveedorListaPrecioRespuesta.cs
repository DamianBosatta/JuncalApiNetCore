namespace JuncalApi.Dto.DtoRespuesta
{
    public class ProveedorListaPrecioRespuesta
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 

        public int? IdProveedor { get; set; }

        public DateTime? FechaVigencia { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public int? IdUsuario { get; set; }

        public bool? Activo { get; set; }

        public string? NombreProveedor { get; set; } = string.Empty;

        public string? NombreUsuario { get; set; } = string.Empty;
    }
}
