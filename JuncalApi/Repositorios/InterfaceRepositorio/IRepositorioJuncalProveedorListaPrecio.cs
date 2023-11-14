using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalProveedorListaPrecio:IRepositorioGenerico<JuncalProveedorListaprecio>
    {

        public List<ProveedorListaPrecioRespuesta> GetListaPrecioForId(int id);

    }
}
