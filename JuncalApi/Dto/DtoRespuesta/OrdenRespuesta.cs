﻿namespace JuncalApi.Dto.DtoRespuesta
{
    public class OrdenRespuesta
    {
        public int Id { get; set; }

        public int IdAceria { get; set; }

        public int? IdContrato { get; set; }

        public string Remito { get; set; } = null!;

        public int? IdCamion { get; set; }

        public int IdEstado { get; set; }

        public DateTime Fecha { get; set; }

        public int? IdProveedor { get; set; }

        public int? IdAcoplado { get; set; }

        public string Observaciones { get; set; } = null!;
    }
}
