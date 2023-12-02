using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;


namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalPreFacturar : RepositorioGenerico<JuncalPreFacturar>, IRepositorioJuncalPreFactura
    {
        public RepositorioJuncalPreFacturar(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region GET ALL PRE FACTURAR
        public List<JuncalPreFacturar> GetAllPreFacturar()
        {



            var query = (from listaPreFacturarada in _db.JuncalPreFacturars
                         join orden in _db.JuncalOrdens.Where(a => a.Isdeleted == false)
                         on listaPreFacturarada.IdOrden equals orden.Id
                         join contrato in _db.JuncalContratos
                         on orden.IdContrato equals contrato.Id
                         join aceria in _db.JuncalAceria
                         on orden.IdAceria equals aceria.Id
                         join materialAceria in _db.JuncalAceriaMaterials
                         on listaPreFacturarada.IdMaterialRecibido equals materialAceria.Id
                         join usuario in _db.JuncalUsuarios
                         on listaPreFacturarada.IdUsuarioFacturacion equals usuario.Id into usuarioJoin
                         from usuario in usuarioJoin.DefaultIfEmpty() // Left join
                         join _proveedor in _db.JuncalProveedors
                         on orden.IdProveedor equals _proveedor.Id into proveedorJoin
                         from proveedor in proveedorJoin.DefaultIfEmpty()

                         let contratoVigente = _db.JuncalContratos
                            .Where(c => c.Numero == contrato.Numero && listaPreFacturarada.FechaExcel <= c.FechaVencimiento && listaPreFacturarada.FechaExcel >= c.FechaVigencia)
                            .OrderBy(c => listaPreFacturarada.FechaExcel-c.FechaVigencia)
                            .FirstOrDefault()
                         let contratoItem = _db.JuncalContratoItems.FirstOrDefault(ci => ci.IdContrato == contratoVigente.Id && ci.IdMaterial == listaPreFacturarada.IdMaterialRecibido)
                         let ordenMaterial = _db.JuncalOrdenMarterials.FirstOrDefault(om => om.IdOrden == listaPreFacturarada.IdOrden)
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
                             NumeroContrato = contratoVigente != null ? contratoVigente.Numero : "-1",
                             NombreContrato = contratoVigente != null ? contratoVigente.Nombre : string.Empty,
                             NombreMaterial = materialAceria.Nombre,
                             PrecioMaterial = contratoItem != null ? contratoItem.Precio : 0,
                             NombreUsuario = usuario != null ? usuario.Nombre : string.Empty,
                             IdUsuarioFacturacion = usuario != null ? usuario.Id : null,
                             Facturado = listaPreFacturarada.Facturado,
                             FechaFacturado = listaPreFacturarada.FechaFacturado,
                             NumeroFactura = ordenMaterial.NumFactura,
                             FechaExcel = listaPreFacturarada.FechaExcel,
                             Proveedor= proveedor
                         }).ToList();

            return query;

        }
        #endregion

    }

}

   

