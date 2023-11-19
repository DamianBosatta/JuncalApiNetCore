using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorCuentaCorriente : RepositorioGenerico<JuncalProveedorCuentaCorriente>, IRepositorioJuncalProveedorCuentaCorriente
    {
        public RepositorioJuncalProveedorCuentaCorriente(JuncalContext db) : base(db)
        {
        }

        public List<ProveedorCuentaCorrienteRespuesta> GetProveedorCcMovimientos(int idProveedor)
        {
            var query = from proveedorCc in _db.JuncalProveedorCuentaCorrientes.Where(a => a.Isdeleted == false)
                        join tipoMovimiento in _db.JuncalCcTiposMovimientos
                        on proveedorCc.IdTipoMovimiento equals tipoMovimiento.Id into ProveedorMovimientosJoin
                        from _ProveedorMovimientos in ProveedorMovimientosJoin.DefaultIfEmpty()
                        select new ProveedorCuentaCorrienteRespuesta
                        {
                            Id = proveedorCc.Id,
                            IdTipoMovimiento = proveedorCc.IdTipoMovimiento,
                            IdUsuario = proveedorCc.IdUsuario,
                            Fecha = proveedorCc.Fecha,
                            Importe = proveedorCc.Importe,
                            NombreTipo = _ProveedorMovimientos.Descripcion,
                            IdProveedor = proveedorCc.IdProveedor 
                            
                           
                        };

            if (idProveedor != 0)
            {
                query = query.Where(a => a.IdProveedor == idProveedor);
            }

            return query.ToList();
        }
    }
}
