﻿using System.Diagnostics.Contracts;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Dto.DtoRespuesta
{
    public class RemitoResponse
    {
       
        public int Id { get; set; }
       
        public string? Remito { get; set; }
       
        public DateTime FechaRemito { get; set; }

        public string? Observacion { get; set; }

        public string? NombreAceria { get; set; }            
       
        public string? DireccionAceria { get; set; }
       
        public string? CuitAceria { get; set; }
      
        public string? CodigoProveedorAceria { get; set; }
       
        public string? NumeroContrato { get; set; }
       
        public string? PatenteCamion { get; set; }
       
        public string? NombreChofer { get; set; }
       
        public int LicenciaChofer { get; set; }
       
        public string? NombreTransportista { get; set; }
      
        public string? PatenteAcoplado { get; set; }
        
        public int IdEstado { get; set; }
        
        public string? DescripcionEstado { get; set; }
       
        public string? NombreProveedor { get; set; }
        
        public string? DireccionProveedor { get; set; }
    }
}