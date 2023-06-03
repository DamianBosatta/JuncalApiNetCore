using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrdenMaterialInternoRecibido : RepositorioGenerico<JuncalOrdenMaterialInternoRecibido>, IRepositorioJuncalOrdenMaterialInternoRecibido
    {
        public RepositorioJuncalOrdenMaterialInternoRecibido(JuncalContext db) : base(db)
        {
        }
    }
}
