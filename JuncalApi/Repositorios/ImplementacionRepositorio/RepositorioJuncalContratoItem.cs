using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalContratoItem:RepositorioGenerico<JuncalContratoItem>,IRepositorioJuncalContratoItem
    {
        public RepositorioJuncalContratoItem(JuncalContext db) : base(db)
        {
        }
    }
}
