using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalPreFacturar : RepositorioGenerico<JuncalPreFacturar>, IRepositorioJuncalPreFactura
    {
        public RepositorioJuncalPreFacturar(JuncalContext db) : base(db)
        {
        }

        public List<IGrouping<int, ItemFacturado>> GetAgrupamientoFacturacion(List<JuncalPreFacturar> listaPreFacturados)
        {

            var query = (from preFacturar in listaPreFacturados
                         join orden in _db.JuncalOrdens.Where(a => a.Isdeleted == false)
                         on preFacturar.IdOrden equals orden.Id into ordenGroup
                         from orden in ordenGroup.DefaultIfEmpty()
                         join contrato in _db.JuncalContratos.Where(a => a.Isdeleted == false)
                         on orden?.IdContrato equals contrato.Id into contratoGroup
                         from contrato in contratoGroup.DefaultIfEmpty()
                         join aceria in _db.JuncalAceria.Where(a => a.Isdeleted == false)
                         on orden?.IdAceria equals aceria.Id into aceriaGroup
                         from aceria in aceriaGroup.DefaultIfEmpty()
                         select new ItemFacturado
                         {
                             IdOrden = preFacturar.IdOrden,
                             IdAceria= aceria.Id,
                             Contrato = contrato != null ? contrato.Numero : null,
                             Remito = preFacturar.Remito,
                             Aceria = aceria != null ? aceria.Nombre : null,
                             PesoEnviado = preFacturar.Peso,
                             PesoRecibido = preFacturar.PesoNeto,
                             ListaMateriales = _db.JuncalOrdenMarterials
                             .Where(a => a.IdOrden == preFacturar.IdOrden && !a.Isdeleted).ToList()
                         }).GroupBy(a => a.IdAceria).ToList();



            return query;


        }

    }
}
