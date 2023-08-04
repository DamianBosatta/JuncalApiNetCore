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
        public List<JuncalPreFacturar> AgrupamientoPreFacturar(List<JuncalPreFacturar> listaPreFacturar)
        {
            var query = (from listaPreFacturarada in listaPreFacturar
                         join orden in _db.JuncalOrdens.Where(a => a.Isdeleted == false)
                         on listaPreFacturarada.IdOrden equals orden.Id
                         select new JuncalPreFacturar
                         {
                             IdOrden = listaPreFacturarada.IdOrden,
                             IdMaterialEnviado=listaPreFacturarada.IdMaterialEnviado,
                             IdMaterialRecibido = listaPreFacturarada.IdMaterialRecibido,
                             Peso = listaPreFacturarada.Peso,
                             PesoTara = listaPreFacturarada.PesoTara,
                             PesoBruto = listaPreFacturarada.PesoBruto,
                             PesoNeto = listaPreFacturarada.PesoNeto,
                             Remito = listaPreFacturarada.Remito,
                             IdAceria = orden.IdAceria,
                             IdContrato= (int)orden.IdContrato
          
                          }).ToList();


                               var remitosOrdenados = query.OrderBy(r => r.IdAceria)
                                .ThenBy(r => r.IdContrato)
                                .ThenBy(r => r.IdMaterialRecibido);

                               var remitosAgrupados = remitosOrdenados.GroupBy(r => new { r.IdAceria, r.IdContrato, r.IdMaterialRecibido })
                                                  .Select(group => new JuncalPreFacturar
                                                  {
                                                      IdAceria = group.Key.IdAceria,
                                                      IdContrato = group.Key.IdContrato,
                                                      IdMaterialRecibido = group.Key.IdMaterialRecibido,
                                                      Peso = group.Sum(r => r.Peso)
                                                  });

                               return remitosAgrupados.ToList();



        }


        

    }
}
