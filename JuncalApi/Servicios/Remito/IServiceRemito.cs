using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Servicios.Remito
{
    public interface IServiceRemito
    {
        public List<RemitoResponse> GetRemitos(int idOrden);

    }
}
