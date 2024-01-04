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

        public int? IdOrdenInterno { get; set; }

        public int? IdOrdenExterno { get; set; }

        public string? Observacion { get; set; } = string.Empty;

        public string? NombreTipoMovimiento { get; set; } = string.Empty;

        public string? NombreMaterial { get; set; }= string.Empty;

        public string NombreUsuario { get; set; }= string.Empty;

        public decimal SaldoTotal {  get; set; }

        public decimal Debito {  get; set; }

        public decimal Credito { get;set; }

        public decimal? PrecioMaterial { get; set; } 

        public string NombreLista {  get; set; } = string.Empty;

        public string NumeroRemito {  get; set; } = string.Empty;

        public bool MaterialBool { get; set; }
    }
}
