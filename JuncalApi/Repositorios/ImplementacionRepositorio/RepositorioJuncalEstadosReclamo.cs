using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalEstadosReclamo:RepositorioGenerico<JuncalEstadosReclamo>,IRepositorioJuncalEstadosReclamo
    {
        public RepositorioJuncalEstadosReclamo(JuncalContext db) : base(db)
        {
        }
    }
}
