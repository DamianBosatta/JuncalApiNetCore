using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCcMovimientoConciliacion:RepositorioGenerico<JuncalCcMovimientoConciliacion>,IRepositorioJuncalCcMovimientoConciliacion
    {
        public RepositorioJuncalCcMovimientoConciliacion(JuncalContext db) : base(db)
        {
        }
    }
}
