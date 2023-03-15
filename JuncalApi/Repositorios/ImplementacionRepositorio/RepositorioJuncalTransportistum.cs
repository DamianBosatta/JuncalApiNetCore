using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalTransportistum:RepositorioGenerico<JuncalTransportistum>,IRepositorioJuncalTransportistum
    {
        public RepositorioJuncalTransportistum(JuncalContext db) : base(db)
        {
        }
    }
}
