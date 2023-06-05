namespace JuncalApi.Dto.DtoRequerido
{
    public class OrdenMaterialInternoRecibidoRequerido
    {
        public int? IdOrdenInterno { get; set; }

        public int? IdMaterial { get; set; }

        public decimal? Peso { get; set; }

        public OrdenMaterialInternoRecibidoRequerido(int? _idOrdenInterno, int? _idMaterial,decimal? _peso)
        {
            IdOrdenInterno = _idOrdenInterno == 0 ? null : _idOrdenInterno ;
            IdMaterial = _idMaterial == 0 ? null : _idMaterial ;
            Peso = _peso is null ? 0 : _peso ;

        }


    }
}
