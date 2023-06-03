using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrdenMaterialInternoRecogido : RepositorioGenerico<JuncalOrdenMaterialInternoRecogido>, IRepositorioJuncalOrdenMaterialInternoRecogido
    {
        public RepositorioJuncalOrdenMaterialInternoRecogido(JuncalContext db) : base(db)
        {
        }
    }
}
