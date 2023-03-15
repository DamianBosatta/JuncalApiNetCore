using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalAcerium:RepositorioGenerico<JuncalAcerium>,IRepositorioJuncalAcerium
    {
        public RepositorioJuncalAcerium(JuncalContext db) : base(db)
        {
        }
    }
}
