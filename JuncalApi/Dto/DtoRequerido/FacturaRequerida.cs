namespace JuncalApi.Dto.DtoRequerido
{
    public class FacturaRequerida
    {

        public string Destinatario { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string Cuit { get; set; } = null!;

        public string ContratoNumero { get; set; } = null!;

        public string ContratoNombre { get; set; } = null!;

        public string NumeroFactura { get; set; } = null!;

        public string Fecha { get; set; } = null!;

        public decimal TotalFactura { get; set; }
    }
}
