namespace JuncalApi.Dto.DtoRequerido
{
    public class CamionRequerido
    {
      
        public string Patente { get; set; } = string.Empty;

        public string? Marca { get; set; }

        public int? Tara { get; set; }

        public int? IdTransportista { get; set; }

        public int? IdInterno { get; set; }

        public int? IdTipoCamion { get; set; }
    }
}
