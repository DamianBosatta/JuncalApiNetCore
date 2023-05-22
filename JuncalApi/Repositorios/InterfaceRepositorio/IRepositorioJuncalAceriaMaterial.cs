using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalAceriaMaterial:IRepositorioGenerico<JuncalAceriaMaterial>
    {
        public List<ItemAceriaMaterial> GetAceriaMaterialesForId(int idAceria);
     
    }
}
