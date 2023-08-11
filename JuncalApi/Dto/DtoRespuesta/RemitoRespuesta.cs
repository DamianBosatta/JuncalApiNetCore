

namespace JuncalApi.Dto.DtoRespuesta
{
    public class RemitoRespuesta
    {


        public int? Id { get; set; } = 0;

        public string? Remito { get; set;} = string.Empty;

        public DateTime? FechaRemito { get; set; } = new DateTime();

        public string? Observaciones { get; set; } = string.Empty;

        public int? IdAceria { get; set; } = 0;

        public string? NombreAceria { get; set; } = string.Empty;

        public string? DireccionAceria { get; set; } = string.Empty;

        public string? CuitAceria { get; set; } = string.Empty;

        public string? CodigoProveedorAceria { get; set; } = string.Empty;

        public int? IdContrato { get; set; } = 0;

        public string? NumeroContrato { get; set; } = string.Empty;

        public int? IdCamion { get; set; } = 0;

        public string? PatenteCamion { get; set; } = string.Empty;

        public int? IdChofer { get; set; } = 0;

        public string? NombreChofer { get; set; } = string.Empty;

        public string? ApellidoChofer { get; set; } = string.Empty;

        public int LicenciaChofer { get; set; } = 0;

        public int? IdTransportista { get; set; } = 0;

        public string? NombreTransportista { get; set; } = string.Empty;

        public int? IdAcoplado { get; set; } = 0;

        public string? PatenteAcoplado { get; set; }= string.Empty;

        public int? IdEstado { get; set; } = 0;

        public string? DescripcionEstado { get; set; } = string.Empty;

        public int? IdProveedor { get; set; } = 0;

        public string? NombreProveedor { get; set; } = string.Empty;

        public int? IdDireccionProveedor { get; set; } = 0;

        public string? DireccionProveedor { get; set; } = string.Empty;


    }
}
