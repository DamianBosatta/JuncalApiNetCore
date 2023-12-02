using JuncalApi.Dto.DtoRequerido.DtoFacturarOrden;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Servicios.Remito
{
    public interface IServiceRemito
    {
        public List<RemitoRespuesta> GetRemitos(int idOrden);

  

    }
}
