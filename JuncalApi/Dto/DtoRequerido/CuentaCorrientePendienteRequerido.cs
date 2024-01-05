namespace JuncalApi.Dto.DtoRequerido
{
    public class CuentaCorrientePendienteRequerido
    {
      

        public int? IdProveedor { get; set; }

        public int? IdMaterial { get; set; }

        public int? IdRemito { get; set; }

        public int? IdUsuario { get; set; }

        public decimal? Kg { get; set; }

        public bool? Pendiente { get; set; }
    }
}
