using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalAcoplado:IRepositorioGenerico<JuncalAcoplado>
    {
        public List<ItemAcoplado> GetAcoplados();

    }
}
