using AutoMapper;
using JuncalApi.Modelos.Item;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido;

namespace JuncalApi.Servicios.Facturar
{
    public class FacturarServicio : IFacturarServicio
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public FacturarServicio(IUnidadDeTrabajo uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }

        public void Facturacion(List<AgrupacionPreFacturar> listPreFacturar, out List<int> ordenesFacturadas, out int cantidadMaterialesFacturados)
        {
            cantidadMaterialesFacturados = 0;
            ordenesFacturadas = new List<int>();

            List<int> idOrdenes = ObtenerListaDeIdOrden(listPreFacturar);
            List<ReferenciaMaterialesEnviados> listaReferenciaMaterialesEnviados = ObtenerListaIdOrdenesMateriales(listPreFacturar);

            List<JuncalOrdenMarterial> listaMaterialesFacturar = ObtenerMaterialesPorIdOrdenes(idOrdenes);
            FacturarMaterialesEnviados(listaMaterialesFacturar, listaReferenciaMaterialesEnviados, listPreFacturar, out listaMaterialesFacturar, out cantidadMaterialesFacturados);

            List<JuncalOrden> listaOrdenes = ObtenerOrdenesPorIdOrdenes(idOrdenes);
            ordenesFacturadas = PasarOrdenesAFacturado(listaMaterialesFacturar, listaOrdenes);
        }

        #region METODOS PRIVADOS

        private List<int> ObtenerListaDeIdOrden(List<AgrupacionPreFacturar> listPreFacturar)
        {
            return listPreFacturar
                .SelectMany(agrupacion => agrupacion.referencia)
                .Select(remito => remito.IdOrden)
                .ToList();
        }

        private List<ReferenciaMaterialesEnviados> ObtenerListaIdOrdenesMateriales(List<AgrupacionPreFacturar> listPreFacturar)
        {
            return listPreFacturar
                .SelectMany(referencia => referencia.referencia)
                .SelectMany(materialesEnviados => materialesEnviados.MaterialesEnviados)
                .ToList();
        }

        private List<JuncalOrdenMarterial> ObtenerMaterialesPorIdOrdenes(List<int> idOrdenes)
        {
            return _uow.RepositorioJuncalOrdenMarterial.ObtenerMaterialesPorListaDeOrdenes(idOrdenes);
        }
        private void FacturarMaterialesEnviados(List<JuncalOrdenMarterial> listaMaterialesFacturar, List<ReferenciaMaterialesEnviados> listaReferenciaMaterialesEnviados, List<AgrupacionPreFacturar> listPreFacturar, out List<JuncalOrdenMarterial> listaMaterialesFacturarActualizada, out int cantidadMaterialesFacturados)
        {
            // Inicialización de variables
            cantidadMaterialesFacturados = 0;
            listaMaterialesFacturarActualizada = listaMaterialesFacturar;

            // Iteración sobre cada orden de material en la lista
            foreach (var ordenMaterial in listaMaterialesFacturar)
            {
                // Búsqueda de referencia de materiales enviados
                var referenciaMaterialesEnviados = listaReferenciaMaterialesEnviados.FirstOrDefault(referencia => referencia.idMaterial == ordenMaterial.IdMaterial);

                // Procesamiento si se encuentra una referencia
                if (referenciaMaterialesEnviados != null)
                {
                    // Obtención de la orden de material
                    var om = _uow.RepositorioJuncalOrdenMarterial.GetById(ordenMaterial.Id);

                    // Actualización de propiedades de la orden de material
                    om.FechaFacturado = DateTime.Now;
                    om.FacturadoParcial = true;

                    // Actualización de la orden de material en el repositorio
                    if (_uow.RepositorioJuncalOrdenMarterial.Update(om))
                    {
                        // Búsqueda y actualización de objetos relacionados en pre-facturas
                        var materialEncontrado = listaMaterialesFacturarActualizada.Find(x => x.Id == om.Id);
                        materialEncontrado.FacturadoParcial = true;

                        // Construcción de lista de referencias encontradas en pre-facturas
                        var referenciasEncontradas = listPreFacturar
                            .SelectMany(p => p.referencia
                                .Where(r => r.IdOrden == om.IdOrden && r.MaterialesEnviados.Any(me => me.idMaterial == om.IdMaterial))
                                .Select(r => new { Referencia = r, IdUsuario = p.idUsuario })
                            )
                            .ToList();

                        // all facturas
                        List<Factura> facturas = listPreFacturar.SelectMany(p => p.factura).ToList();

                        // Creación e inserción de una nueva factura

                        foreach(var objFactura in facturas)
                        {
                            JuncalFactura facturaInsert = new JuncalFactura // foreach para cargar el objeto factura 
                            {
                                Destinatario = objFactura.Destinatario,
                                Direccion = objFactura.Direccion,
                                Cuit = objFactura.Cuit,
                                ContratoNumero = objFactura.ContratoNumero,
                                ContratoNombre = objFactura.ContratoNombre,
                                NumeroFactura = objFactura.NumeroFactura,
                                Fecha = objFactura.Fecha,  // Asigna el valor correcto de la fecha
                                NombreUsuario = objFactura.NombreUsuario
                            };

                            bool ResultInsertFactura = _uow.RepositorioJuncalFactura.Insert(facturaInsert);


                            if (ResultInsertFactura)
                            {
                                // Búsqueda de la factura recién insertada 
                                var facturaId = _uow.RepositorioJuncalFactura.GetByCondition(a => a.NumeroFactura == facturaInsert.NumeroFactura);

                                foreach(var obj in objFactura.listaMateriales)
                                {
                                   
                                    JuncalFacturaMateriale insertMaterial = new JuncalFacturaMateriale();
                                    insertMaterial.NombreMaterial = obj.NombreMaterial;
                                    insertMaterial.Peso = obj.Peso;
                                    insertMaterial.SubTota= obj.SubTotal;
                                    insertMaterial.IdFactura = facturaId.Id;
                                    _uow.RepositorioJuncalFacturaMateriale.Insert(insertMaterial);


                                }
                            }

                        }

                   
                                                                               
                           
                        // Búsqueda y procesamiento de referencias encontradas en pre-facturas
                            foreach (var referenciaEncontrada in referenciasEncontradas)
                            {
                                var referencia = referenciaEncontrada.Referencia;
                                var idUsuario = referenciaEncontrada.IdUsuario;

                                // Iteración sobre los materiales enviados en la referencia
                                foreach (var materialesEnviados in referencia.MaterialesEnviados)
                                {
                                    // Obtención de la pre-factura
                                    var prefactura = _uow.RepositorioJuncalPreFactura.GetById(materialesEnviados.idPrefactura);
                          

                                    // Actualización de propiedades de la pre-factura
                                    prefactura.FechaFacturado = DateTime.Now;
                                    prefactura.Facturado = true;
                                    prefactura.IdUsuarioFacturacion = idUsuario;

                                    // Búsqueda de la referencia en la lista de pre-facturas
                                    var referenciaPreFacturar = listPreFacturar.FirstOrDefault(p => p.referencia.Contains(referencia));

                                    // Actualización de la orden de material con el número de factura de la referencia
                                    om.NumFactura = referenciaPreFacturar?.num_factura;
                                    _uow.RepositorioJuncalOrdenMarterial.Update(om);

                                    // Actualización de la pre-factura en el repositorio
                                    bool response = _uow.RepositorioJuncalPreFactura.Update(prefactura);

                                    // Incremento del contador de materiales facturados
                                    cantidadMaterialesFacturados += response ? 1 : 0;
                                }
                            }
                        }
                    }
                }
            }
        


        private List<JuncalOrden> ObtenerOrdenesPorIdOrdenes(List<int> idOrdenes)
        {
            return _uow.RepositorioJuncalOrden.GetAllByCondition(jo => idOrdenes.Contains(jo.Id)).ToList();
        }

        private List<int> PasarOrdenesAFacturado(List<JuncalOrdenMarterial> listaMaterialesFacturar, List<JuncalOrden> listaOrdenes)
        {
            List<int> ordenesFacturadas = new List<int>();

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
                        orden.IdEstado = 4;
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


        #endregion


    }
}

