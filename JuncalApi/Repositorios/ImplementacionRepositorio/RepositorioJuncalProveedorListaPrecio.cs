using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorListaPrecio:RepositorioGenerico<JuncalProveedorListaprecio>, IRepositorioJuncalProveedorListaPrecio
    {
        public RepositorioJuncalProveedorListaPrecio(JuncalContext db) : base(db)
        {
        }
    }
}
