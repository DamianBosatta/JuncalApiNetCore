namespace JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido
{
    public class FacturarOrdenRequerido
    {
        public int IdRemito { get; set; }

        public int IdProveedor { get; set; }

        public int IdListaPrecio { get; set; }

        public int IdMaterial {  get; set; }

        public decimal Peso { get; set; }

        public int IdUsuario { get; set; }

        public string Observacion { get; set; } = string.Empty;

    }
}
