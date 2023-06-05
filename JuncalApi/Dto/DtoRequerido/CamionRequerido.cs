namespace JuncalApi.Dto.DtoRequerido
{
    public class CamionRequerido
    {
      
        public string Patente { get; set; } = string.Empty;

        public string? Marca { get; set; }

        public int? Tara { get; set; }

        public int? IdChofer { get; set; }

        public int? IdTransportista { get; set; }

        public int? IdInterno { get; set; }

        public int? IdTipoCamion { get; set; }

        public CamionRequerido(string patente, string? marca, int? tara, int? idChofer, int? idTransportista, int? idInterno, int? idTipoCamion)
        {
            Patente = patente is null ? string.Empty: patente;
            Marca = marca is null ? string.Empty:marca;
            Tara = tara ==0? null : tara;
            IdChofer = idChofer==0 ? null : idChofer;
            IdTransportista = idTransportista == 0 ? null : idTransportista;
            IdInterno = idInterno == 0 ? null : idInterno;
            IdTipoCamion = idTipoCamion == 0 ? null : idTipoCamion;
        }
    }
}
