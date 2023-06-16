using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalOrdenMarterial:IRepositorioGenerico<JuncalOrdenMarterial>
    {

        public ItemDataMateriales DataMaterial(int idAceria, int idOrden, string codigoMateria);
    }
}
