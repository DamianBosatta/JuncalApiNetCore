namespace JuncalApi.Dto.DtoRequerido
{
    public class ProveedorListaPrecioRequerido
    {
       
        public int? IdProveedor { get; set; }

        public string Nombre { get; set; }

        public DateTime? FechaVigencia { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public int? IdUsuario { get; set; }

        public bool? Activo { get; set; }
    }
}
