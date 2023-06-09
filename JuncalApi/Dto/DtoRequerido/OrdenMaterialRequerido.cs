﻿namespace JuncalApi.Dto.DtoRequerido
{
    public class OrdenMaterialRequerido
    {  
        public int Id { get; set; }
        
        public int? IdOrden { get; set; }

        public int? IdMaterial { get; set; }

        public decimal? Peso { get; set; }

        public OrdenMaterialRequerido(int? idOrden, int? idMaterial, decimal? peso)
        {
          
            IdOrden = idOrden ==0? null:idOrden;
            IdMaterial = idMaterial==0?null:idMaterial;
            Peso = peso is null ? 0 : peso;
        }
    }
}
