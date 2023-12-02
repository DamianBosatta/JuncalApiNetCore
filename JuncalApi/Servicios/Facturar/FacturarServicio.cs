using AutoMapper;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido;
using System.Globalization;
using JuncalApi.Modelos.Codigos_Utiles;
using JuncalApi.Dto.DtoRequerido.DtoFacturarOrden;

namespace JuncalApi.Servicios.Facturar
{
    public class FacturarServicio : IFacturarServicio
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<FacturarServicio> _logger;

        public FacturarServicio(IUnidadDeTrabajo uow, IMapper mapper, ILogger<FacturarServicio> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
    
        #region FACTURACION

        public void Facturacion(List<AgrupacionPreFacturar> listPreFacturar, out List<int> ordenesFacturadas, out int cantidadMaterialesFacturados)
        {
            cantidadMaterialesFacturados = 0;
            ordenesFacturadas = new List<int>();

            try
            {
                List<int> idOrdenes = ObtenerListaDeIdOrden(listPreFacturar);
                List<ReferenciaMaterialesEnviados> listaReferenciaMaterialesEnviados = ObtenerListaIdOrdenesMateriales(listPreFacturar);

                List<JuncalOrdenMarterial> listaMaterialesFacturar = ObtenerMaterialesPorIdOrdenes(idOrdenes);
                FacturarMaterialesEnviados(listaMaterialesFacturar, listaReferenciaMaterialesEnviados, listPreFacturar, out listaMaterialesFacturar, out cantidadMaterialesFacturados);

                List<JuncalOrden> listaOrdenes = ObtenerOrdenesPorIdOrdenes(idOrdenes);
                ordenesFacturadas = PasarOrdenesAFacturado(listaMaterialesFacturar, listaOrdenes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método Facturacion(Servicio Facturar): {ErrorMessage}", ex.Message);
                throw;
            }
        }

        #endregion

        #region FACTURAR REMITO INTERNO
        /// <summary>
        /// Factura un remito interno basado en los detalles proporcionados.
        /// </summary>
        /// <param name="ordenInternoRequerido">Detalles necesarios para la facturación del remito interno.</param>
        /// <returns>Objeto JuncalProveedorCuentaCorriente con los detalles de la factura.</returns>
        public JuncalProveedorCuentaCorriente FacturarRemitoInterno(FacturarRemitoInternoRequerido ordenInternoRequerido)
        {
            JuncalProveedorCuentaCorriente cuentaCorriente = new JuncalProveedorCuentaCorriente();
            try
            {
                var listaPrecio = _uow?.RepositorioJuncalProveedorListaPreciosMateriales
                    .GetAllByCondition(a => a.IdProveedorListaprecios == ordenInternoRequerido.IdListaPrecio);

                if (listaPrecio != null && listaPrecio.Any())
                {
                    var precioMaterial = listaPrecio.FirstOrDefault(a => a.Id == ordenInternoRequerido.IdMaterial);

                    if (precioMaterial != null)
                    {
                        decimal dineroFacturado = (decimal)(precioMaterial.Precio * ordenInternoRequerido.Peso);

                        DateTime fechaActual = DateTime.Now;


                        cuentaCorriente = new JuncalProveedorCuentaCorriente
                        {
                            IdProveedor = (int)ordenInternoRequerido.OrdenInterno.IdProveedor,
                            IdTipoMovimiento = CodigosUtiles.Remito,
                            Fecha = fechaActual,
                            Observacion = ordenInternoRequerido.Observacion is null ? "Sin Observacion" : ordenInternoRequerido.Observacion.ToString(),
                            Importe = dineroFacturado,
                            Peso = (double?)ordenInternoRequerido.Peso,
                            IdMaterial = ordenInternoRequerido.IdMaterial,
                            IdUsuario = ordenInternoRequerido.IdUsuario,
                            IdRemitoInterno = ordenInternoRequerido.OrdenInterno.Id
                        };

                        return cuentaCorriente;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al facturar remito interno(Servicio Facturar)");
                throw;
            }

            // Retorno predeterminado si no se cumplen las condiciones anteriores
            return cuentaCorriente;
        }
        #endregion

        #region FACTURAR REMITO EXTERNO
        /// <summary>
        /// Factura un remito externo basado en los detalles proporcionados.
        /// </summary>
        /// <param name="ordenExternoRequerido">Detalles necesarios para la facturación del remito externo.</param>
        /// <returns>Objeto JuncalProveedorCuentaCorriente con los detalles de la factura.</returns>
        public List<JuncalProveedorCuentaCorriente> FacturarRemitoExterno(List<FacturarOrdenRequerido> ordenesExternosRequeridos)
        {
            List<JuncalProveedorCuentaCorriente> cuentasCorrientes = new List<JuncalProveedorCuentaCorriente>();

            foreach (var ordenExternoRequerido in ordenesExternosRequeridos)
            {
                JuncalProveedorCuentaCorriente cuentaCorriente = new JuncalProveedorCuentaCorriente();

                try
                {
                    var listaPrecio = _uow?.RepositorioJuncalProveedorListaPreciosMateriales
                        .GetAllByCondition(a => a.IdProveedorListaprecios == ordenExternoRequerido.IdListaPrecio);

                    if (listaPrecio != null && listaPrecio.Any())
                    {
                        var precioMaterial = listaPrecio.FirstOrDefault(a => a.Id == ordenExternoRequerido.IdMaterial);

                        if (precioMaterial != null)
                        {
                            decimal dineroFacturado = (decimal)(precioMaterial.Precio * ordenExternoRequerido.Peso);

                            DateTime fechaActual = DateTime.Now;

                            cuentaCorriente = new JuncalProveedorCuentaCorriente
                            {
                                IdProveedor = (int)ordenExternoRequerido.IdProveedor,
                                IdTipoMovimiento = CodigosUtiles.Remito,
                                Fecha = fechaActual,
                                Observacion = ordenExternoRequerido.Observacion is null ? "Sin Observacion" : ordenExternoRequerido.Observacion.ToString(),
                                Importe = dineroFacturado,
                                Peso = (double?)ordenExternoRequerido.Peso,
                                IdMaterial = ordenExternoRequerido.IdMaterial,
                                IdUsuario = ordenExternoRequerido.IdUsuario,
                                IdRemitoExterno = ordenExternoRequerido.IdRemito,
                                Isdeleted = false
                            };

                            cuentasCorrientes.Add(cuentaCorriente);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al facturar remito externo(Servicio Facturar)");
                    throw;
                }
            }

            return cuentasCorrientes;
        }
        #endregion

        #region METODOS PRIVADOS

        private List<int> ObtenerListaDeIdOrden(List<AgrupacionPreFacturar> listPreFacturar)
        {
            try
            {
                return listPreFacturar
                    .SelectMany(agrupacion => agrupacion.referencia)
                    .Select(remito => remito.IdOrden)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en ObtenerListaDeIdOrden(Servicio Facturar): {ErrorMessage}", ex.Message);
                throw;
            }
        }

        private List<ReferenciaMaterialesEnviados> ObtenerListaIdOrdenesMateriales(List<AgrupacionPreFacturar> listPreFacturar)
        {
            try
            {
                return listPreFacturar
                    .SelectMany(referencia => referencia.referencia)
                    .SelectMany(materialesEnviados => materialesEnviados.MaterialesEnviados)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en ObtenerListaIdOrdenesMateriales(Servicio Facturar): {ErrorMessage}", ex.Message);
                throw;
            }
        }

        private List<JuncalOrdenMarterial> ObtenerMaterialesPorIdOrdenes(List<int> idOrdenes)
        {
            try
            {
                return _uow.RepositorioJuncalOrdenMarterial.ObtenerMaterialesPorListaDeOrdenes(idOrdenes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en ObtenerMaterialesPorIdOrdenes(Servicio Facturar): {ErrorMessage}", ex.Message);
                throw; 
            }
        }


        private void FacturarMaterialesEnviados(List<JuncalOrdenMarterial> listaMaterialesFacturar, List<ReferenciaMaterialesEnviados> listaReferenciaMaterialesEnviados, List<AgrupacionPreFacturar> listPreFacturar, out List<JuncalOrdenMarterial> listaMaterialesFacturarActualizada, out int cantidadMaterialesFacturados)
        {
            cantidadMaterialesFacturados = 0;
            listaMaterialesFacturarActualizada = listaMaterialesFacturar;
            string numFacturaFacturado = "";
             try
    {

            foreach (var ordenMaterial in listaMaterialesFacturar)
            {
                var referenciaMaterialesEnviados = listaReferenciaMaterialesEnviados.FirstOrDefault(referencia => referencia.idMaterial == ordenMaterial.IdMaterial);

                if (referenciaMaterialesEnviados != null)
                {
                    var om = _uow.RepositorioJuncalOrdenMarterial.GetById(ordenMaterial.Id);
                    

                    if (om!=null)
                    {

                        var referenciasEncontradas = listPreFacturar
                        .SelectMany(p => p.referencia
                            .Where(r => r.IdOrden == om.IdOrden && r.MaterialesEnviados.Any(me => me.idMaterial == om.IdMaterial))
                            .Select(r => new { Referencia = r, IdUsuario = p.idUsuario })
                        )
                        .ToList();


                        
                        var numFactura = "";

                        foreach (var referenciaEncontrada in referenciasEncontradas)
                        {
                            var referencia = referenciaEncontrada.Referencia;
                            var idUsuario = referenciaEncontrada.IdUsuario;
                            var referenciaPreFacturar = listPreFacturar.FirstOrDefault(p => p.referencia.Contains(referencia));
                            numFactura = referenciaPreFacturar?.num_factura;

                            foreach (var materialesEnviados in referencia.MaterialesEnviados)
                            {
                                om.NumFactura = numFactura;
                                om.FechaFacturado = DateTime.Now;
                                om.FacturadoParcial = true;

                                if(_uow.RepositorioJuncalOrdenMarterial.Update(om))
                                { 
                                    var materialEncontrado = listaMaterialesFacturarActualizada.Find(x => x.Id == om.Id);
                                    materialEncontrado.FacturadoParcial = true;

                                    var prefactura = _uow.RepositorioJuncalPreFactura.GetById(materialesEnviados.idPrefactura);
                                    prefactura.FechaFacturado = DateTime.Now;
                                    prefactura.Facturado = true;
                                    prefactura.IdUsuarioFacturacion = idUsuario;

                                    bool response = _uow.RepositorioJuncalPreFactura.Update(prefactura);
                                   cantidadMaterialesFacturados += response ? 1 : 0;
                                }

                                
                            }

                            var FacturaEncontrada = listPreFacturar
                                .Select(p => p.factura)
                                .Where(r => r.NumeroFactura == numFactura)
                                .FirstOrDefault();

                            if(numFacturaFacturado != FacturaEncontrada.NumeroFactura)
                            { 
                                JuncalFactura facturaInsert = new JuncalFactura()
                                {
                                    Destinatario = FacturaEncontrada.Destinatario,
                                    Direccion = FacturaEncontrada.Direccion,
                                    Cuit = FacturaEncontrada.Cuit,
                                    ContratoNumero = FacturaEncontrada.ContratoNumero,
                                    ContratoNombre = FacturaEncontrada.ContratoNombre,
                                    NumeroFactura = FacturaEncontrada.NumeroFactura,
                                    Fecha = FacturaEncontrada.Fecha,
                                    NombreUsuario = FacturaEncontrada.NombreUsuario,
                                    TotalFactura = decimal.Parse(FacturaEncontrada.TotalFactura, CultureInfo.InvariantCulture)
                                };
                                _uow.RepositorioJuncalFactura.Insert(facturaInsert);

                                var FacturaReferencia = _uow.RepositorioJuncalFactura.GetByNumeroFactura(facturaInsert.NumeroFactura);

                                foreach (var material in FacturaEncontrada.listaMateriales)
                                {
                                    JuncalFacturaMateriale materialInsert = new JuncalFacturaMateriale();

                                    materialInsert.NombreMaterial = material.NombreMaterial;
                                    materialInsert.IdFactura = FacturaReferencia.Id;
                                    materialInsert.SubTota = material.SubTotal;
                                    materialInsert.Peso = material.Peso;


                                    _uow.RepositorioJuncalFacturaMateriale.Insert(materialInsert);
                                

                                }
                                numFacturaFacturado = FacturaEncontrada.NumeroFactura;
                            }
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        {
        _logger.LogError(ex, "Se ha producido un error en el método FacturarMaterialesEnviados(Servicio Facturar): {ErrorMessage}", ex.Message);
         throw;
        }
    }




        private List<JuncalOrden> ObtenerOrdenesPorIdOrdenes(List<int> idOrdenes)
        {
            try
            {
                return _uow.RepositorioJuncalOrden.GetAllByCondition(jo => idOrdenes.Contains(jo.Id)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método ObtenerOrdenesPorIdOrdenes(Servicio Facturar): {ErrorMessage}", ex.Message);
                throw; 
            }
        }


        private List<int> PasarOrdenesAFacturado(List<JuncalOrdenMarterial> listaMaterialesFacturar, List<JuncalOrden> listaOrdenes)
        {
            List<int> ordenesFacturadas = new List<int>();

            try
            {
                foreach (var orden in listaOrdenes)
                {
                    var ordenMaterialesParaOrden = listaMaterialesFacturar.Where(o => o.IdOrden == orden.Id).ToList();
                    bool todosFacturados = ordenMaterialesParaOrden.All(om => om.FacturadoParcial);

                    if (todosFacturados)
                    {
                        if (orden != null)
                        {
                            orden.FechaFacturacion = DateTime.Now;
                            orden.Facturado = true;
                            orden.IdEstado = CodigosUtiles.Facturado;
                            bool respuesta = _uow.RepositorioJuncalOrden.Update(orden);

                            if (respuesta)
                            {
                                ordenesFacturadas.Add(orden.Id);
                            }
                        }
                    }
                }

                return ordenesFacturadas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Se ha producido un error en el método PasarOrdenesAFacturado(Servicio Facturar): {ErrorMessage}", ex.Message);
                throw; 
            }
        }


        #endregion


    }
}

