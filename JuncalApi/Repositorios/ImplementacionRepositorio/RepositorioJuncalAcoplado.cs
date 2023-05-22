using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalAcoplado:RepositorioGenerico<JuncalAcoplado>,IRepositorioJuncalAcoplado
    {
        public RepositorioJuncalAcoplado(JuncalContext db) : base(db)
        {
        }

        public List<ItemAcoplado> GetAcoplados()
        {
            var query = from acoplado in _db.JuncalAcoplados.Where(a => a.Isdeleted == false)
                        join tipoAcoplado in _db.JuncalTipoAcoplados
                        on acoplado.IdTipo equals tipoAcoplado.Id
                        select new { acoplado, tipoAcoplado };

            List<ItemAcoplado> listaAcoplados = new List<ItemAcoplado>();

            foreach (var q in query)
            {
                listaAcoplados.Add(new ItemAcoplado(q.acoplado, q.tipoAcoplado));


            }

            return listaAcoplados;

        }
    }
}
