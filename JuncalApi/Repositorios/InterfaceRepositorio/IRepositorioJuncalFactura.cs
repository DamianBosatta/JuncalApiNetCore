using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalFactura : IRepositorioGenerico<JuncalFactura>
    {

        public List<FacturaRespuesta> JuncalFacturaList();

        public FacturaRespuesta GetByNumeroFactura(String numeroFactura);
    }
}
