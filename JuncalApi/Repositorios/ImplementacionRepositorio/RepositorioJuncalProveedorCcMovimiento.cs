using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorCcMovimiento:RepositorioGenerico<JuncalProveedorCcMovimiento>, IRepositorioJuncalProveedorCcMovimiento
    {
        public RepositorioJuncalProveedorCcMovimiento(JuncalContext db) : base(db)
        {
        }

        public List<ProveedorCcMovimientoRespuesta> GetProveedorCcMovimientos(int idProveedor)
        {
            var query = (from proveedorCcMovimientos in _db.JuncalProveedorCcMovimientos.Where(a => a.Isdeleted == false)
                        join tipoMovimiento in _db.JuncalCcTiposMovimientos
                        on proveedorCcMovimientos.IdTipo equals tipoMovimiento.Id into ProveedorMovimientosJoin
                        from _ProveedorMovimientos in ProveedorMovimientosJoin.DefaultIfEmpty()
                        select new ProveedorCcMovimientoRespuesta
                        {
                            Id = proveedorCcMovimientos.Id,
                            IdTipo = proveedorCcMovimientos.IdTipo,
                            IdUsuario = proveedorCcMovimientos.IdUsuario,
                            Fecha = proveedorCcMovimientos.Fecha,
                            Importe = proveedorCcMovimientos.Importe,
                            NombreTipo = _ProveedorMovimientos.Descripcion,
                        

                          });

                         
                       
                return idProveedor==0? query.ToList():query.Where(a=>a.IdProveedor== idProveedor).ToList();
           
            
              
            

            

        }
    }
}
