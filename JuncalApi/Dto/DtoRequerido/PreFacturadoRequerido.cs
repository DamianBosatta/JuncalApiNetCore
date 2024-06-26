﻿namespace JuncalApi.Dto.DtoRequerido
{
    public class PreFacturadoRequerido
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

        public int? IdUsuarioFacturacion { get; set; }
      
        public DateTime? FechaFacturado { get; set; }

        public DateTime? FechaExcel { get; set; }
    }
}
