using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalChofer: RepositorioGenerico<JuncalChofer>,IRepositorioJuncalChofer
    {
        public RepositorioJuncalChofer(JuncalContext db) : base(db)
        {
        }
    }
}
