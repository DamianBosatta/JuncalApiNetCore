using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCcMovimientoRemito:RepositorioGenerico<JuncalCcMovimientoRemito>,IRepositorioJuncalCcMovimientoRemito
    {
        public RepositorioJuncalCcMovimientoRemito(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
