namespace JuncalApi.Dto.DtoRequerido
{
    public class ProveedorCuentaCorrienteRequerido
    {

        public int? IdProveedor { get; set; }

        public int? IdTipoMovimiento { get; set; }

        public int? IdUsuario { get; set; }

        public DateTime? Fecha { get; set; }


        public decimal? Importe { get; set; }

        public double? Peso { get; set; }

        public int? IdMaterial { get; set; }

        public string Observacion { get; set; } = null!;
    }
}
