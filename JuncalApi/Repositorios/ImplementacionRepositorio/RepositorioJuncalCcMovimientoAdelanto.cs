using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCcMovimientoAdelanto:RepositorioGenerico<JuncalCcMovimeintoAdelanto>,IRepositorioJuncalCcMovimientoAdelanto
    {
        public RepositorioJuncalCcMovimientoAdelanto(JuncalContext db) : base(db)
        {
        }
    }
}
