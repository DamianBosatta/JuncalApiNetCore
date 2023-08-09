﻿using AutoMapper;
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

        private void FacturarMaterialesEnviados(List<JuncalOrdenMarterial> listaMaterialesFacturar, List<ReferenciaMaterialesEnviados> listaReferenciaMaterialesEnviados, List<AgrupacionPreFacturar> listPreFacturar, out List<JuncalOrdenMarterial> listaMaterialesFacturarActualizada, out int cantidadMaterialesFacturados )
        {
            cantidadMaterialesFacturados = 0;
            listaMaterialesFacturarActualizada = listaMaterialesFacturar;

            foreach (var ordenMaterial in listaMaterialesFacturar)
            {
                if (listaReferenciaMaterialesEnviados.Any(referencia => referencia.idMaterial == ordenMaterial.IdMaterial))
                {
                    var om = _uow.RepositorioJuncalOrdenMarterial.GetById(ordenMaterial.Id);
                    om.FacturadoParcial = true;
                    bool respuesta = _uow.RepositorioJuncalOrdenMarterial.Update(om);

                    if (respuesta)
                    {
                        var materialEncontrado = listaMaterialesFacturarActualizada.Find(x => x.Id == om.Id);
                        if (materialEncontrado != null)
                        {
                            materialEncontrado.FacturadoParcial = true;
                        }

                        var referenciasEncontradas = listPreFacturar
                            .SelectMany(p => p.referencia)
                            .Where(r => r.IdOrden == om.IdOrden && r.MaterialesEnviados.Any(me => me.idMaterial == om.IdMaterial))
                            .ToList();

                        foreach (var referencia in referenciasEncontradas)
                        {
                            foreach (var materialesEnviados in referencia.MaterialesEnviados)
                            {
                                var prefactura = _uow.RepositorioJuncalPreFactura.GetById(materialesEnviados.idPrefactura);
                                prefactura.Facturado = true;
                                bool response = _uow.RepositorioJuncalPreFactura.Update(prefactura);


                                cantidadMaterialesFacturados = response is false ? cantidadMaterialesFacturados : cantidadMaterialesFacturados + 1;
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

