namespace JuncalApi.Dto.DtoRequerido
{
    public class CcMovimientoRemitoRequerido
    {     
        public int? IdMovimiento { get; set; }

        public int? IdMaterial { get; set; }

        public decimal? Pesaje1 { get; set; }

        public decimal? Pesaje2 { get; set; }

        public decimal? Finalizado { get; set; }

        public int? IdRemito { get; set; }
    }
}
