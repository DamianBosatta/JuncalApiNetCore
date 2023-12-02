using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;


namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalProveedorCuentaCorriente : RepositorioGenerico<JuncalProveedorCuentaCorriente>, IRepositorioJuncalProveedorCuentaCorriente
    {
        public RepositorioJuncalProveedorCuentaCorriente(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region GET PROVEEDOR CUENTAS CORRIENTES
        public List<ProveedorCuentaCorrienteRespuesta> GetProveedorCuentasCorrientes(int idProveedor)
        {
            var result = new List<ProveedorCuentaCorrienteRespuesta>();

            var query = from proveedorCc in _db.JuncalProveedorCuentaCorrientes.Where(a => a.Isdeleted == false)
                        join tipoMovimiento in _db.JuncalCcTiposMovimientos
                            on proveedorCc.IdTipoMovimiento equals tipoMovimiento.Id into ProveedorMovimientosJoin
                        from _ProveedorMovimientos in ProveedorMovimientosJoin.DefaultIfEmpty()
                        join material in _db.JuncalProveedorListapreciosMateriales.DefaultIfEmpty()
                            on proveedorCc.IdMaterial equals material.Id into MaterialesJoin
                        from _Materiales in MaterialesJoin.DefaultIfEmpty()
                        join usuario in _db.JuncalUsuarios.Where(a => a.Isdeleted == false)
                            on proveedorCc.IdUsuario equals usuario.Id into UsuarioJoin
                        from _Usuario in UsuarioJoin.DefaultIfEmpty()
                        join listaPrecio in _db.JuncalProveedorListaprecios
                            on _Materiales.IdProveedorListaprecios equals listaPrecio.Id into ListaPrecioJoin
                        from _ListaPrecio in ListaPrecioJoin.DefaultIfEmpty()
                        join remitoInterno in _db.JuncalOrdenInternos
                            on proveedorCc.IdRemitoInterno equals remitoInterno.Id into RemitoInternoJoin
                        from _RemitoInterno in RemitoInternoJoin.DefaultIfEmpty()
                        join remitoExterno in _db.JuncalOrdens
                            on proveedorCc.IdRemitoExterno equals remitoExterno.Id into RemitoExternoJoin
                        from _RemitoExterno in RemitoExternoJoin.DefaultIfEmpty()
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
                            IdProveedor = proveedorCc.IdProveedor,
                            NombreTipoMovimiento = _ProveedorMovimientos.Descripcion,
                            NombreMaterial = _Materiales.Nombre,
                            PrecioMaterial = (decimal)_Materiales.Precio,
                            NombreLista = _ListaPrecio.Nombre,
                            NombreUsuario = _Usuario.Nombre + " " + _Usuario.Apellido,
                            IdOrdenInterno = proveedorCc.IdRemitoInterno,
                            IdOrdenExterno = proveedorCc.IdRemitoExterno,
                            NumeroRemito = _RemitoExterno != null ? _RemitoExterno.Remito : _RemitoInterno != null ? _RemitoInterno.Remito : null
                        };
            result = query.ToList();

            decimal totalCredito = 0;
            decimal totalDebito = 0;

            foreach (var item in result)
            {
                decimal importe = Convert.ToDecimal(item.Importe);

                if (importe > 0)
                {
                    totalCredito += importe;
                }
                else
                {
                    totalDebito += Math.Abs(importe);
                }

                item.Credito = totalCredito;
                item.Debito = totalDebito;
                item.SaldoTotal = totalCredito - totalDebito;
            }

            if (idProveedor != 0)
            {
                result = result.Where(a => a.IdProveedor == idProveedor).ToList();
            }

            return result;
        }
        #endregion
    }
}

