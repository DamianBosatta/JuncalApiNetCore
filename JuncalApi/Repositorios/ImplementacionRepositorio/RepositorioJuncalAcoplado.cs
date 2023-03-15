using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalAcoplado:RepositorioGenerico<JuncalAcoplado>,IRepositorioJuncalAcoplado
    {
        public RepositorioJuncalAcoplado(JuncalContext db) : base(db)
        {
        }
    }
}
