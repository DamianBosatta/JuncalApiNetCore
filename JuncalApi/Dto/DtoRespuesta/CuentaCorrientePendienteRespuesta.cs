using JuncalApi.Modelos;

namespace JuncalApi.Dto.DtoRespuesta
{
    public class CuentaCorrientePendienteRespuesta
    {
        public int Id { get; set; }

        public int? IdProveedor { get; set; }

        public JuncalProveedor? Proveedor { get; set; }

        public int? IdMaterial { get; set; }

        public string Material { get; set; } = string.Empty;

        public int? IdRemito { get; set; }

        public string Remito { get; set; } = string.Empty;

        public int? IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;    

        public decimal? Kg { get; set; }

        public bool? Pendiente { get; set; }


    }
}
