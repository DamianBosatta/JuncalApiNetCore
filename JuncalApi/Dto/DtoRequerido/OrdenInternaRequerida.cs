namespace JuncalApi.Dto.DtoRequerido
{
    public class OrdenInternaRequerida
    {
     
       public int IdAceria { get; set; }

        public int IdContrato { get; set; }

        public string Remito { get; set; } = null!;

        public int IdCamion { get; set; }

        public int IdEstadoInterno { get; set; }

        public DateTime Fecha { get; set; }

        public string Observaciones { get; set; } = null!;

        public int IdDireccionProveedor { get; set; }

        public int IdProveedor { get; set; }

        public int IdAcoplado { get; set; }
    }
}
