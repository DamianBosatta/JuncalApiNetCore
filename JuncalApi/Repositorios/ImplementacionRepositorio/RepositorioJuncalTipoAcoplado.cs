using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalTipoAcoplado:RepositorioGenerico<JuncalTipoAcoplado>,IRepositorioJuncalTipoAcoplado
    {
        public RepositorioJuncalTipoAcoplado(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
