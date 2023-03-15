using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalMaterial:RepositorioGenerico<JuncalMaterial>,IRepositorioJuncalMaterial
    {
        public RepositorioJuncalMaterial(JuncalContext db) : base(db)
        {
        }
    }
}
