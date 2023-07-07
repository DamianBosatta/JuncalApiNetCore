using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalOrdenMarterial:IRepositorioGenerico<JuncalOrdenMarterial>
    {

        public List<ItemDataMateriales> GetDatosMaterialesAndRemitoExcel(int idAceria, List<string> remito);
    }
}
