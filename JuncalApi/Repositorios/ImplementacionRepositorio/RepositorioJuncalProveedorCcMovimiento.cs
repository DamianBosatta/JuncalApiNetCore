using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorCcMovimiento:RepositorioGenerico<JuncalProveedorCcMovimiento>, IRepositorioJuncalProveedorCcMovimiento
    {
        public RepositorioJuncalProveedorCcMovimiento(JuncalContext db) : base(db)
        {
        }
    }
}
