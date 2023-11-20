using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalProveedorListaPreciosMateriales:IRepositorioGenerico<JuncalProveedorListapreciosMateriale>
    {

        public List<ProveedorListaPrecioMaterialRespuesta> GetListaPreciosMateriales();
    }
}
