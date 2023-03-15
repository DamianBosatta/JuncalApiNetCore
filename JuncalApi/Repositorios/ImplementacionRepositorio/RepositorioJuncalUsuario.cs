using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalUsuario:RepositorioGenerico<JuncalUsuario>,IRepositorioJuncalUsuario
    {
        public RepositorioJuncalUsuario(JuncalContext db) : base(db)
        {
        }
    }
}
