namespace JuncalApi.Dto.DtoRequerido
{
    public class RemitoHistorialRequerido
    {
        public int IdOrden { get; set; }

        public string Aceria { get; set; } = null!;

        public string Contrato { get; set; } = null!;

        public string Remito { get; set; } = null!;

        public string Camion { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public string Fecha { get; set; } = null!;

        public string Proveedor { get; set; } = null!;

        public string Acoplado { get; set; } = null!;

        public string Observaciones { get; set; } = null!;

        public string DireccionProveedor { get; set; } = null!;

        public DateTime FechaGenerado { get; set; }

    }
}
