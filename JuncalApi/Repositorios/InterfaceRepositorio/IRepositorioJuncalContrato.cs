using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalContrato:IRepositorioGenerico<JuncalContrato>
    {
        public List<ItemContrato> GetContratos();

    }

   
    

    
}
