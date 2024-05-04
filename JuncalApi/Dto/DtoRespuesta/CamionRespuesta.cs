using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Dto.DtoRespuesta
{
    public class CamionRespuesta
    {
        public int Id { get; set; }

        public string? Patente { get; set; }

        public string? Marca { get; set; }

        public int Tara { get; set; }

        public int IdTransportista { get; set; }

        public int IdInterno { get; set; }

        public int IdTipoCamion { get; set; }

        public string? NombreTransportista { get; set; }
        
        public string? DescripcionTipoCamion { get; set; }

    }
}
