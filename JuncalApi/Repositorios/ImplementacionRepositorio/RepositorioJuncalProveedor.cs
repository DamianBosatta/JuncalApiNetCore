using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedor:RepositorioGenerico<JuncalProveedor>,IRepositorioJuncalProveedor
    {
        public RepositorioJuncalProveedor(JuncalContext db) : base(db)
        {
        }
    }
}
