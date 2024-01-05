using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalCuentaCorrientePendiente:IRepositorioGenerico<JuncalCuentaCorrientePendiente>
    {

        public List<CuentaCorrientePendienteRespuesta> GetCCPendiente(int idProveedor);
    }
}
