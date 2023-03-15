using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalRole:RepositorioGenerico<JuncalRole>,IRepositorioJuncalRole
    {
        public RepositorioJuncalRole(JuncalContext db) : base(db)
        {
        }
    }
}
