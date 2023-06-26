using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalCamion:IRepositorioGenerico<JuncalCamion>
    {
        public List<JuncalCamion> GetCamiones();
    }

   
 
}
