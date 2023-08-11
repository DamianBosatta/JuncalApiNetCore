using JuncalApi.Modelos;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Dto.DtoRespuesta
{
    public class PreFacturadoRespuesta
    {
        public int Id { get; set; }

        public int IdOrden { get; set; }

        public int IdMaterialEnviado { get; set; }

        public int IdMaterialRecibido { get; set; }

        public decimal Peso { get; set; }

        public decimal PesoTara { get; set; }

        public decimal PesoBruto { get; set; }

        public decimal PesoNeto { get; set; }

        public bool Facturado { get; set; }

        public string Remito { get; set; } = null!;
      
        public int IdAceria { get; set; } 
     
        public int IdContrato { get; set; }

        public int? IdUsuarioFacturacion { get; set; }

        public JuncalAcerium? Aceria { get; set; }
        
        public string? NumeroContrato { get; set; }
        
        public string? NombreContrato { get; set; }
        
        public string? NombreMaterial { get; set; }
        
        public decimal? PrecioMaterial { get; set; }

        public string? NombreUsuario { get; set; }
    }
}
