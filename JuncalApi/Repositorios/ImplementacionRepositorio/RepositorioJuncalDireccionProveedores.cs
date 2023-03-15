using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalDireccionProveedores : RepositorioGenerico<JuncalDireccionProveedor>, IRepositorioJuncalDireccionProveedor
    {
        public RepositorioJuncalDireccionProveedores(JuncalContext db) : base(db)
        {
        }
    }
}
