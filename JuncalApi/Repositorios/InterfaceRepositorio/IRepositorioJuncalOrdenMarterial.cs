using JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalOrdenMarterial:IRepositorioGenerico<JuncalOrdenMarterial>
    {

        public List<ItemDataMateriales> GetDatosMaterialesAndRemitoExcel(int idAceria, List<string> remito, List<string>listaCodigos);
      
        public List<JuncalOrdenMarterial> ObtenerMaterialesPorListaDeOrdenes(List<int> idOrdenes);

    }
}
