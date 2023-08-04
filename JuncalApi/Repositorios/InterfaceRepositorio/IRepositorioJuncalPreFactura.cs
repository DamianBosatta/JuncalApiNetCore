using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalPreFactura:IRepositorioGenerico<JuncalPreFacturar>
    {
        public List<JuncalPreFacturar> AgrupamientoPreFacturar(List<PreFacturadoRequerido> listaPreFacturar);
     
    }

   
}
