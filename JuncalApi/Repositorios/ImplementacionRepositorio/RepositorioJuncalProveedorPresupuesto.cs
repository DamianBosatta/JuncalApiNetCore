using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorPresupuesto : RepositorioGenerico<JuncalProveedorPresupuesto>, IRepositorioJuncalProveedorPresupuesto
    {
        public RepositorioJuncalProveedorPresupuesto(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
