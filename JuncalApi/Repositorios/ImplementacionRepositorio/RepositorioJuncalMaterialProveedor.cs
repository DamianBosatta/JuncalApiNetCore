using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalMaterialProveedor:RepositorioGenerico<JuncalMaterialProveedor>,IRepositorioJuncalMaterialProveedor
    {
        public RepositorioJuncalMaterialProveedor(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}
