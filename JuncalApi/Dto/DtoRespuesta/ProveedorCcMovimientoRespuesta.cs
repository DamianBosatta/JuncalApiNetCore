namespace JuncalApi.Dto.DtoRespuesta
{
    public class ProveedorCcMovimientoRespuesta
    {
        public int Id { get; set; }

        public int? IdProveedor { get; set; }

        public int? IdTipo { get; set; }

        public int? IdUsuario { get; set; }

        public DateTime? Fecha { get; set; }

        public decimal? Importe { get; set; }

        public string? NombreTipo { get; set; } = string.Empty;
    }
}
