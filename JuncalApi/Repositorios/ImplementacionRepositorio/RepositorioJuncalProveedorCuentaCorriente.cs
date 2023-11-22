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

        public List<ProveedorCuentaCorrienteRespuesta> GetProveedorCuentasCorrientes(int idProveedor)
        {
            var query = from proveedorCc in _db.JuncalProveedorCuentaCorrientes.Where(a => a.Isdeleted == false)
                        join tipoMovimiento in _db.JuncalCcTiposMovimientos
                        on proveedorCc.IdTipoMovimiento equals tipoMovimiento.Id into ProveedorMovimientosJoin
                        from _ProveedorMovimientos in ProveedorMovimientosJoin.DefaultIfEmpty()
                        join material in _db.JuncalMaterials.Where(a => a.Isdeleted == false)
                        on proveedorCc.IdMaterial equals material.Id into MaterialesJoin
                        from _Materiales in MaterialesJoin.DefaultIfEmpty()
                        join usuario in _db.JuncalUsuarios.Where(a => a.Isdeleted == false)
                        on proveedorCc.IdUsuario equals usuario.Id into UsuarioJoin
                        from _Usuario in UsuarioJoin.DefaultIfEmpty()
                        

                        select new ProveedorCuentaCorrienteRespuesta
                        {
                            Id = proveedorCc.Id,
                            IdTipoMovimiento = proveedorCc.IdTipoMovimiento,
                            IdUsuario = proveedorCc.IdUsuario,
                            Fecha = proveedorCc.Fecha,
                            Importe = proveedorCc.Importe,
                            Peso = proveedorCc.Peso,
                            IdMaterial = proveedorCc.IdMaterial,
                            Observacion = proveedorCc.Observacion,                           
                            IdProveedor = proveedorCc.IdProveedor ,
                            NombreTipoMovimiento = _ProveedorMovimientos.Descripcion,
                            NombreMaterial= _Materiales.Nombre,
                            NombreUsuario=_Usuario.Nombre+" "+_Usuario.Apellido,
                            IdOrden=proveedorCc.IdRemito

                        };

            if (idProveedor != 0)
            {
                query = query.Where(a => a.IdProveedor == idProveedor);
            }

            return query.ToList();
        }
    }
}
