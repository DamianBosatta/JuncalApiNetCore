using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalAceriaMaterial:RepositorioGenerico<JuncalAceriaMaterial>,IRepositorioJuncalAceriaMaterial
    {
        public RepositorioJuncalAceriaMaterial(JuncalContext db) : base(db)
        {
        }
    }
}
