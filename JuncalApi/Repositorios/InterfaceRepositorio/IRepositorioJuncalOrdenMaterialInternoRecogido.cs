using JuncalApi.Modelos;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalOrdenMaterialInternoRecogido:IRepositorioGenerico<JuncalOrdenMaterialInternoRecogido>
    {

        public List<JuncalOrdenMaterialInternoRecogido> listaMaterialesRecogidos(int idOrdenInterna);
      
    }
}
