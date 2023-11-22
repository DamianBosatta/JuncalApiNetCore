using JuncalApi.Modelos;

namespace JuncalApi.Dto.DtoRequerido
{
    public class FacturarRemitoInternoRequerido
    {
        public JuncalOrdenInterno? OrdenInterno {get;set;}

        public int IdListaPrecio { get;set;}

        public int IdMaterial { get; set; }

        public decimal Peso {  get; set; }

        public bool Cerrar {  get; set; }

        public int IdUsuario { get; set; }

        public string? Observacion { get; set; }
    }
}
