using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalProveedorCuentaCorriente:IRepositorioGenerico<JuncalProveedorCuentaCorriente>
    {
        public List<ProveedorCuentaCorrienteRespuesta> GetProveedorCuentasCorrientes(int idProveedor, bool esMaterial);

    }
}
