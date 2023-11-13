using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCcTipoMovimiento:RepositorioGenerico<JuncalCcTiposMovimiento>, IRepositorioJuncalCcTipoMovimiento
    {
        public RepositorioJuncalCcTipoMovimiento(JuncalContext db) : base(db)
        {
        }
    }
}
