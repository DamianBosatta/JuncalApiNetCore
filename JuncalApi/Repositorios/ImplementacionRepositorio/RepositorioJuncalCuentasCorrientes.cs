using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCuentasCorrientes : RepositorioGenerico<JuncalCuentasCorriente>, IRepositorioJuncalCuentasCorrientes
    {
        public RepositorioJuncalCuentasCorrientes(JuncalContext db) : base(db)
        {
        }

    }
}
