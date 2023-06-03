using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalOrdenInterno:IRepositorioGenerico<JuncalOrdenInterno>
    {
        public ItemRemitoInterno GetRemito(int id);
        public List<ItemRemitoInterno> GetAllRemitos();
    }
}
