using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalContrato: RepositorioGenerico<JuncalContrato>, IRepositorioJuncalContrato
    {
        public RepositorioJuncalContrato(JuncalContext db) : base(db)
        {
        }
    }
}
