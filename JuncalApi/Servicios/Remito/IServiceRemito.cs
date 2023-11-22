using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Servicios.Remito
{
    public interface IServiceRemito
    {
        public List<RemitoRespuesta> GetRemitos(int idOrden);

        public JuncalCuentasCorriente FacturarRemitoInterno(FacturarRemitoInternoRequerido ordenInternoRequerido);

    }
}
