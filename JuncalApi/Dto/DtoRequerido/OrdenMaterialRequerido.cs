namespace JuncalApi.Dto.DtoRequerido
{
    public class OrdenMaterialRequerido
    {  
        public int Id { get; set; }
        
        public int? IdOrden { get; set; }

        public int? IdMaterial { get; set; }

        public decimal? Peso { get; set; }
    }
}
