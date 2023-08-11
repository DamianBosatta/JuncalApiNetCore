using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRequerido;
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

        public List<JuncalPreFacturar> GetAllPreFacturar()
        {

         

 var query = (from listaPreFacturarada in _db.JuncalPreFacturars
             where listaPreFacturarada.IsDelete == false && listaPreFacturarada.Facturado == false
             join orden in _db.JuncalOrdens.Where(a => a.Isdeleted == false)
             on listaPreFacturarada.IdOrden equals orden.Id
             join aceria in _db.JuncalAceria
             on orden.IdAceria equals aceria.Id
             join contrato in _db.JuncalContratos
             on orden.IdContrato equals contrato.Id
             join materialAceria in _db.JuncalAceriaMaterials
             on listaPreFacturarada.IdMaterialRecibido equals materialAceria.Id
             join usuario in _db.JuncalUsuarios
             on listaPreFacturarada.IdUsuarioFacturacion equals usuario.Id
             let contratoItem = _db.JuncalContratoItems.FirstOrDefault(ci => ci.IdContrato == contrato.Id && ci.IdMaterial == listaPreFacturarada.IdMaterialRecibido)
             select new JuncalPreFacturar
             {
                 IdOrden = listaPreFacturarada.IdOrden,
                 IdMaterialEnviado = listaPreFacturarada.IdMaterialEnviado,
                 IdMaterialRecibido = listaPreFacturarada.IdMaterialRecibido,
                 Peso = listaPreFacturarada.Peso,
                 PesoTara = listaPreFacturarada.PesoTara,
                 PesoBruto = listaPreFacturarada.PesoBruto,
                 PesoNeto = listaPreFacturarada.PesoNeto,
                 Remito = listaPreFacturarada.Remito,
                 IdAceria = orden.IdAceria,
                 IdContrato = (int)orden.IdContrato,
                 Id = listaPreFacturarada.Id,
                 Aceria = aceria,
                 NumeroContrato = contrato.Numero,
                 NombreContrato = contrato.Nombre,
                 NombreMaterial = materialAceria.Nombre,
                 PrecioMaterial = contratoItem != null ? contratoItem.Precio : 0,
                 NombreUsuario = usuario.Nombre ?? string.Empty

             }).ToList();

            return query;

        }
     

    }

   
}
