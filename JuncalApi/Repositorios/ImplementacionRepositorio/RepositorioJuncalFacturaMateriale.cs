using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalFacturaMateriale : RepositorioGenerico<JuncalFacturaMateriale>, IRepositorioJuncalFacturaMateriale
    {
        public RepositorioJuncalFacturaMateriale(JuncalContext db) : base(db)
        {
        }
    
    }
}
