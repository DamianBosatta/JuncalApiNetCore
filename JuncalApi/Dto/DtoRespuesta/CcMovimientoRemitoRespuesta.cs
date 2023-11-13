namespace JuncalApi.Dto.DtoRespuesta
{
    public class CcMovimientoRemitoRespuesta
    {
        public int Id { get; set; }

        public int? IdMovimiento { get; set; }

        public int? IdMaterial { get; set; }

        public decimal? Pesaje1 { get; set; }

        public decimal? Pesaje2 { get; set; }

        public decimal? Finalizado { get; set; }

        public int? IdRemito { get; set; }
    }
}
