using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCamion:RepositorioGenerico<JuncalCamion>,IRepositorioJuncalCamion
    {
        public RepositorioJuncalCamion(JuncalContext db) : base(db)
        {
        }
    }
}
