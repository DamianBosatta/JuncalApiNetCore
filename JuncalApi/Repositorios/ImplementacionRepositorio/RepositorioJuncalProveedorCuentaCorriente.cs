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
        public List<ProveedorCuentaCorrienteRespuesta> GetProveedorCuentasCorrientes(int idProveedor , bool esMaterial)
        {
            // Parte 1: Obtener las cuentas corrientes del proveedor
            var proveedorCcQuery = _db.JuncalProveedorCuentaCorrientes
            .Where(a => a.IdProveedor == idProveedor && a.Isdeleted == false &&
                 a.MaterialBool == esMaterial );

            // Parte 2: Unir con los tipos de movimiento
            var joinedMovimientos = from proveedorCc in proveedorCcQuery
                                    join tipoMovimiento in _db.JuncalCcTiposMovimientos
                                        on proveedorCc.IdTipoMovimiento equals tipoMovimiento.Id into ProveedorMovimientosJoin
                                    from _ProveedorMovimientos in ProveedorMovimientosJoin.DefaultIfEmpty()
                                    select new { proveedorCc, _ProveedorMovimientos };

            // Parte 3: Unir con materiales
            var joinedMateriales = from joined in joinedMovimientos
                                   join material in _db.JuncalProveedorListapreciosMateriales.DefaultIfEmpty()
                                       on joined.proveedorCc.IdMaterial equals material.Id into MaterialesJoin
                                   from _Materiales in MaterialesJoin.DefaultIfEmpty()
                                   select new { joined.proveedorCc, joined._ProveedorMovimientos, _Materiales };

            // Parte 4: Unir con usuarios
            var joinedUsuarios = from joined in joinedMateriales
                                 join usuario in _db.JuncalUsuarios.Where(a => a.Isdeleted == false)
                                     on joined.proveedorCc.IdUsuario equals usuario.Id into UsuarioJoin
                                 from _Usuario in UsuarioJoin.DefaultIfEmpty()
                                 select new { joined.proveedorCc, joined._ProveedorMovimientos, joined._Materiales, _Usuario };

            // Parte 5: Unir con listas de precios
            var joinedListasPrecios = from joined in joinedUsuarios
                                      join listaPrecio in _db.JuncalProveedorListaprecios
                                          on joined._Materiales.IdProveedorListaprecios equals listaPrecio.Id into ListaPrecioJoin
                                      from _ListaPrecio in ListaPrecioJoin.DefaultIfEmpty()
                                      select new { joined.proveedorCc, joined._ProveedorMovimientos, joined._Materiales, joined._Usuario, _ListaPrecio };

            // Parte 6: Unir con órdenes internas
            var joinedOrdenesInternas = from joined in joinedListasPrecios
                                        join remitoInterno in _db.JuncalOrdenInternos
                                            on joined.proveedorCc.IdRemitoInterno equals remitoInterno.Id into RemitoInternoJoin
                                        from _RemitoInterno in RemitoInternoJoin.DefaultIfEmpty()
                                        select new { joined.proveedorCc, joined._ProveedorMovimientos, joined._Materiales, joined._Usuario, joined._ListaPrecio, _RemitoInterno };

            // Parte 7: Unir con órdenes externas
            var joinedOrdenesExternas = from joined in joinedOrdenesInternas
                                        join remitoExterno in _db.JuncalOrdens
                                            on joined.proveedorCc.IdRemitoExterno equals remitoExterno.Id into RemitoExternoJoin
                                        from _RemitoExterno in RemitoExternoJoin.DefaultIfEmpty()
                                        select new { joined.proveedorCc, joined._ProveedorMovimientos, joined._Materiales, joined._Usuario, joined._ListaPrecio, joined._RemitoInterno, _RemitoExterno };

            // Parte 8: Crear el resultado final
            var results = from joined in joinedOrdenesExternas
                          select new ProveedorCuentaCorrienteRespuesta
                          {
                              Id = joined.proveedorCc.Id,
                              MaterialBool = joined.proveedorCc.MaterialBool,
                              IdTipoMovimiento = joined.proveedorCc.IdTipoMovimiento,
                              IdUsuario = joined.proveedorCc.IdUsuario,
                              Fecha = joined.proveedorCc.Fecha,
                              Importe = joined.proveedorCc.Importe,
                              Peso = (double?)joined.proveedorCc.Peso,
                              IdMaterial = joined.proveedorCc.IdMaterial,
                              Observacion = joined.proveedorCc.Observacion,
                              IdProveedor = joined.proveedorCc.IdProveedor,
                              NombreTipoMovimiento = joined._ProveedorMovimientos.Descripcion,
                              NombreMaterial = joined._Materiales.Nombre,
                              PrecioMaterial = (decimal)joined._Materiales.Precio,
                              NombreLista = joined._ListaPrecio.Nombre,
                              NombreUsuario = joined._Usuario.Nombre + " " + joined._Usuario.Apellido,
                              IdOrdenInterno = joined.proveedorCc.IdRemitoInterno,
                              IdOrdenExterno = joined.proveedorCc.IdRemitoExterno,
                              NumeroRemito = joined._RemitoExterno != null ? joined._RemitoExterno.Remito : joined._RemitoInterno != null ? joined._RemitoInterno.Remito : "",
                              Total = (decimal)joined.proveedorCc.Total,
                              
                          };

            var resultList = results.ToList();

            decimal totalCredito = 0;
            decimal totalDebito = 0;
            if (!esMaterial)
            {
                foreach (var item in resultList)
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
            }
            else {
                foreach (var item in resultList)
                {
                    decimal importe = Convert.ToDecimal(item.Peso);
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

            }

            resultList = idProveedor==0? resultList : resultList.Where(a=>a.IdProveedor== idProveedor).ToList();
           
            resultList.Where(a => a.MaterialBool == esMaterial);// si es material entra en true te devuelve cuenta corriente con materiales
                                                                // y si no en dinero.

            return resultList;
        }
        #endregion


    }
}

