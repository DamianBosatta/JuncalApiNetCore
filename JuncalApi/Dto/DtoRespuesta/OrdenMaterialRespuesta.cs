﻿namespace JuncalApi.Dto.DtoRespuesta
{
    public class OrdenMaterialRespuesta
    {
        public int Id { get; set; }

        public int IdOrden { get; set; }

        public int IdMaterial { get; set; }

        public decimal? Peso { get; set; }
    }
}
