namespace JuncalApi.Dto.DtoRequerido
{
    public class RemitosReclamadoRequerido
    {


        public int IdEstadoReclamo { get; set; }

        public int IdRemito { get; set; }

        public DateTime? Fecha { get; set; }

        public string? Observacion { get; set; }

        public DateTime? FechaReclamo { get; set; }

        public string? ObservacionReclamo { get; set; }

        public DateTime? FechaFinalizado { get; set; }

        public string? ObservacionFinalizado { get; set; }

        public int? IdUsuarioReclamo { get; set; }

        public int? IdUsuarioIngreso { get; set; }

        public int? IdUsuarioFinalizado { get; set; }

        public int IdAceria { get; set; }
    }
}
