using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCuentasCorrientesTipo : RepositorioGenerico<JuncalCuentasCorrientesTipo>, IRepositorioJuncalCuentasCorrientesTipo
    {
        public RepositorioJuncalCuentasCorrientesTipo(JuncalContext db) : base(db)
        {
        }
    
    }
}
