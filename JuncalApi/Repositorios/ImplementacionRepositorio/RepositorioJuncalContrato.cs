using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalContrato: RepositorioGenerico<JuncalContrato>, IRepositorioJuncalContrato
    {
        public RepositorioJuncalContrato(JuncalContext db) : base(db)
        {
        }

        public List<ItemContrato> GetContratos()
        {

            var query = from contrato in _db.JuncalContratos.Where(a=>a.Isdeleted==false)
                        join aceria in _db.JuncalAceria.Where(a=>a.Isdeleted==false)
                        on contrato.IdAceria equals aceria.Id
                        select new { contrato, aceria };

            List<ItemContrato> listContrato = new List<ItemContrato>();

            foreach(var q in query)
            {
                listContrato.Add(new ItemContrato(q.contrato, q.aceria));

            }

            return listContrato;

        }
    }
}
