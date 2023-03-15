using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalSucursal :RepositorioGenerico<JuncalSucursal>, IRepositorioJuncalSucursal
    {
        public RepositorioJuncalSucursal(JuncalContext db) : base(db)
        {

        }



    }

    }

