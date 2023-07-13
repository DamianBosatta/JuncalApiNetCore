using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalContratoItem:IRepositorioGenerico<JuncalContratoItem>
    {
        public List<JuncalContratoItem> GetContratoItemForIdContrato(int idContrato);
        public decimal GetPrecioMaterial(int idContrato, int idMaterial);


    }
}
