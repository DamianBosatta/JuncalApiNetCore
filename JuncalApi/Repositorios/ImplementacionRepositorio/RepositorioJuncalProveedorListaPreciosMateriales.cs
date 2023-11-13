using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorListaPreciosMateriales:RepositorioGenerico<JuncalProveedorListapreciosMateriale>, IRepositorioJuncalProveedorListaPreciosMateriales
    {
        public RepositorioJuncalProveedorListaPreciosMateriales(JuncalContext db) : base(db)
        {
        }
    }
}
