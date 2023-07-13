using JuncalApi.Modelos;

namespace JuncalApi.Dto.DtoRespuesta
{
    public class RemitosPendientesRespuesta
    {
        public int IdOrden { get; set; }

        public int IdAceria { get; set; }

        public string NombreAceria { get; set; }

        public int IdContrato { get; set; }

        public int IdEstado { get; set; }

        public string DescripcionEstado { get; set; }

        public List<JuncalOrdenMarterial> ListaMaterialesOrden { get; set; }


    }
}
