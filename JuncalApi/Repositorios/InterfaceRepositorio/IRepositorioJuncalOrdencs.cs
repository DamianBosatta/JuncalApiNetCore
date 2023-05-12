using JuncalApi.Modelos;
using JuncalApi.Modelos.Items;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalOrdencs:IRepositorioGenerico<JuncalOrden>
    {
        public ItemOrden GetRemito(int id);


    }
}
