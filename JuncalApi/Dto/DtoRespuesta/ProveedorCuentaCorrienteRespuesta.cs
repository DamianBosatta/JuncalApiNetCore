namespace JuncalApi.Dto.DtoRespuesta
{
    public class ProveedorCuentaCorrienteRespuesta
    {
        public int Id { get; set; }

        public int? IdProveedor { get; set; }

        public int? IdTipoMovimiento { get; set; }

        public int? IdUsuario { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal? Importe { get; set; }

        public double? Peso { get; set; }

        public int? IdMaterial { get; set; }

        public string Observacion { get; set; } = null!;

        public string? NombreTipoMovimiento { get; set; } = string.Empty;

        public string? NombreMaterial { get; set; }= string.Empty;

        public string NombreUsuario { get; set; }= string.Empty;
    }
}
