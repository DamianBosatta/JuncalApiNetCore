using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalEstado:RepositorioGenerico<JuncalEstado>,IRepositorioJuncalEstado
    {
        public RepositorioJuncalEstado(JuncalContext db) : base(db)
        {
        }
    }
}
