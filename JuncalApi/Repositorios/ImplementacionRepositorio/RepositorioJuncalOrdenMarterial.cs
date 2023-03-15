using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrdenMarterial:RepositorioGenerico<JuncalOrdenMarterial>,IRepositorioJuncalOrdenMarterial
    {
        public RepositorioJuncalOrdenMarterial(JuncalContext db) : base(db)
        {
        }
    }
}
