using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;
using Microsoft.EntityFrameworkCore;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalNotificacione : RepositorioGenerico<JuncalNotificacione>, IRepositorioJuncalNotificacion
    {
        public RepositorioJuncalNotificacione(JuncalContext db) : base(db)
        {
        }

    }
}
