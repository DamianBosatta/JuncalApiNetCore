using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalRemitoHistorial:RepositorioGenerico<JuncalRemitoHistorial>,IRepositorioJuncalRemitoHistorial
    {
        public RepositorioJuncalRemitoHistorial(JuncalContext db) : base(db)
        {
        }
    }
}
