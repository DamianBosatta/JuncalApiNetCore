using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCuentaCorrientePendiente : RepositorioGenerico<JuncalCuentaCorrientePendiente>, IRepositorioJuncalCuentaCorrientePendiente
    {
        public RepositorioJuncalCuentaCorrientePendiente(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        public List<CuentaCorrientePendienteRespuesta> GetCCPendiente(int idProveedor)
        {
            try { 
            var query = from ccPendiente in _db.JuncalCuentaCorrientePendientes
                        join proveedor in _db.JuncalProveedors
                            on ccPendiente.IdProveedor equals proveedor.Id into proveedorJoin
                        from proveedor in proveedorJoin.DefaultIfEmpty()
                        join material in _db.JuncalAceriaMaterials
                            on ccPendiente.IdMaterial equals material.Id into materialJoin
                        from material in materialJoin.DefaultIfEmpty()
                        join materialJuncal in _db.JuncalMaterials
                            on material.IdMaterial equals materialJuncal.Id into materialJuncalJoin
                        from materialJuncal in materialJuncalJoin.DefaultIfEmpty()
                        join remito in _db.JuncalOrdens
                            on ccPendiente.IdRemito equals remito.Id into remitoJoin
                        from remito in remitoJoin.DefaultIfEmpty()
                        join user in _db.JuncalUsuarios
                            on ccPendiente.IdUsuario equals user.Id into userJoin
                        from user in userJoin.DefaultIfEmpty()
                        select new CuentaCorrientePendienteRespuesta
                        {
                            Id = ccPendiente.Id,
                            IdProveedor = ccPendiente.IdProveedor,
                            NombreProveedor = proveedor != null ? proveedor.Nombre :" Sin Nombre Proveedor",
                            IdMaterial = ccPendiente.IdMaterial,
                            Material = materialJuncal != null ? materialJuncal.Nombre : "Sin Nombre Material",
                            IdRemito = ccPendiente.IdRemito,
                            Remito = remito != null ? remito.Remito : " Sin Remito",
                            IdUsuario = ccPendiente.IdUsuario,
                            NombreUsuario = user != null ? user.Nombre + " " + user.Apellido : "User Sin Nombre",
                            Kg = ccPendiente.Kg,
                            Pendiente = ccPendiente.Pendiente
                        };

                query = idProveedor == 0 ? query : query.Where(a => a.IdProveedor == idProveedor);

               return query.ToList();


        }
        catch (Exception ex)
        {
              _logger.LogError(ex, "Ocurrió una excepción en GetCCPendiente(RepositorioJuncalCuentaCorrientePendiente)");
       
               throw;
            }
        }
    }

}

