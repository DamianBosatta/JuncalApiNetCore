using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalOrdencs:IRepositorioGenerico<JuncalOrden>
    {
        public ItemRemito GetRemito(int id);

    }
}
