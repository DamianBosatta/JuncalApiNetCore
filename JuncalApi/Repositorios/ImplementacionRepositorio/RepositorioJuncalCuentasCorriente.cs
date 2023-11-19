using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCuentasCorriente:RepositorioGenerico<JuncalCuentasCorriente>, IRepositorioJuncalCuentasCorriente
    {
        public RepositorioJuncalCuentasCorriente(JuncalContext db) : base(db)
        {
        }

      


    }
}
