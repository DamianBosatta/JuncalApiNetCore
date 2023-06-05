namespace JuncalApi.Dto.DtoRequerido
{
    public class ContratoRequerido
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Numero { get; set; }

        public DateTime? FechaVigencia { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public int? IdAceria { get; set; }

        public bool? Activo { get; set; }    

        public decimal? ValorFlete { get; set; }


        public ContratoRequerido(string nombre, string? numero, DateTime? fechaVigencia,
        DateTime? fechaVencimiento, int? idAceria, bool? activo, decimal? valorFlete)
        {
            Nombre = nombre is null ? string.Empty:nombre;
            Numero = numero is null ? string.Empty:numero;
            FechaVigencia = fechaVigencia is null ? new DateTime(): fechaVigencia ;
            FechaVencimiento = fechaVencimiento is null ? new DateTime():fechaVencimiento;
            IdAceria = idAceria==0?null:idAceria;
            Activo = activo is null ? false: activo;
            ValorFlete = valorFlete is null ? 0 : valorFlete;
        }
    }
}
