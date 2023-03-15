using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalTipoCamion:RepositorioGenerico<JuncalTipoCamion>,IRepositorioJuncalTipoCamion
    {
        public RepositorioJuncalTipoCamion(JuncalContext db) : base(db)
        {
        }
    }
}
