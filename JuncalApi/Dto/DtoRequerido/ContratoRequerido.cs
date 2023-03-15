namespace JuncalApi.Dto.DtoRequerido
{
    public class ContratoRequerido
    {
        public string Nombre { get; set; } = null!;

        public string? Numero { get; set; }

        public DateTime FechaVigencia { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public int? IdAceria { get; set; }

        public bool Activo { get; set; }    

        public decimal ValorFlete { get; set; }
    }
}
