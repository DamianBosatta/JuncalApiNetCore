using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalExcelConfig:RepositorioGenerico<JuncalExcelConfig>,IRepositorioJuncalExcelConfig
    {
        public RepositorioJuncalExcelConfig(JuncalContext db) : base(db)
        {
        }
    }
}
