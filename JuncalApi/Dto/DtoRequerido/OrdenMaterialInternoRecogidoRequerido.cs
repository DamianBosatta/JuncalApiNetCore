namespace JuncalApi.Dto.DtoRequerido
{
    public class OrdenMaterialInternoRecogidoRequerido
    {
        public int? IdOrdenInterno { get; set; }

        public int? IdMaterial { get; set; }

        public decimal? Peso { get; set; }

      

        public OrdenMaterialInternoRecogidoRequerido(int? idOrdenInterno, int? idMaterial, decimal? peso)
        {
            IdOrdenInterno = idOrdenInterno == 0 ? null : idOrdenInterno;
            IdMaterial = idMaterial == 0 ? null : idMaterial;
            Peso = peso is null ? 0 : peso;
           
        }
    }
}
