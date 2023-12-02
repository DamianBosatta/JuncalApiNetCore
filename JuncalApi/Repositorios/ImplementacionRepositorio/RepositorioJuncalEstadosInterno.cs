using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalEstadosInterno : RepositorioGenerico<JuncalEstadosInterno>, IRepositorioJuncalEstadosInterno
    {
        public RepositorioJuncalEstadosInterno(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }
    
    }
}
