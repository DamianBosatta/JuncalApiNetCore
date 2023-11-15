using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalProveedorCcMovimiento:IRepositorioGenerico<JuncalProveedorCcMovimiento>
    {

        public List<ProveedorCcMovimientoRespuesta> GetProveedorCcMovimientos(int idProveedor);
    }
}
