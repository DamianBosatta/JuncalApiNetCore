﻿using JuncalApi.Repositorios.ImplementacionRepositorio;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.UnidadDeTrabajo
{
    public interface IUnidadDeTrabajo:IDisposable
    {
        IRepositorioJuncalAceriaMaterial RepositorioJuncalAceriaMaterial { get; }
        IRepositorioJuncalAcerium RepositorioJuncalAcerium { get; }
        IRepositorioJuncalCamion RepositorioJuncalCamion { get; }
        IRepositorioJuncalChofer RepositorioJuncalChofer { get; }
        IRepositorioJuncalContrato RepositorioJuncalContrato { get; }
        IRepositorioJuncalContratoItem RepositorioJuncalContratoItem { get; }
        IRepositorioJuncalAcoplado RepositorioJuncalAcoplado { get; }
        IRepositorioJuncalEstado RepositorioJuncalEstado { get; }
        IRepositorioJuncalMaterial RepositorioJuncalMaterial { get; }
        IRepositorioJuncalMaterialProveedor RepositorioJuncalMaterialProveedor { get; }
        IRepositorioJuncalOrdencs RepositorioJuncalOrden { get; }
        IRepositorioJuncalOrdenMarterial RepositorioJuncalOrdenMarterial { get; }
        IRepositorioJuncalProveedor RepositorioJuncalProveedor { get; }
        IRepositorioJuncalRole RepositorioJuncalRole { get; }
        IRepositorioJuncalTransportistum RepositorioJuncalTransportistum { get; }
        IRepositorioJuncalUsuario RepositorioJuncalUsuario { get; }
        IRepositorioJuncalTipoCamion RepositorioJuncalTipoCamion { get; }
        IRepositorioJuncalSucursal RepositorioJuncalSucursal { get; }
        IRepositorioJuncalDireccionProveedor RepositorioJuncalDireccionProveedor { get; }       
        IRepositorioJuncalTipoAcoplado RepositorioJuncalTipoAcoplado { get; }
        IRepositorioJuncalOrdenInterno RepositorioJuncalOrdenInterno { get; }
        IRepositorioJuncalOrdenMaterialInternoRecibido RepositorioJuncalOrdenMaterialInternoRecibido { get; }
        IRepositorioJuncalOrdenMaterialInternoRecogido RepositorioJuncalOrdenMaterialInternoRecogido { get; }
        IRepositorioJuncalExcelConfig RepositorioJuncalExcelConfig { get; }
        IRepositorioJuncalRemitosReclamado RepositorioJuncalRemitosReclamado { get; }
        IRepositorioJuncalEstadosReclamo RepositorioJuncalEstadosReclamo { get; }
        IRepositorioJuncalProveedorPresupuesto RepositorioJuncalProveedorPresupuesto { get; }
        IRepositorioJuncalProveedorPresupuestoMaterial RepositorioJuncalProveedorPresupuestoMaterial { get; }
        IRepositorioJuncalPreFactura RepositorioJuncalPreFactura { get; }
        IRepositorioJuncalCuentasCorriente RepositorioJuncalCuentasCorriente { get; }
        IRepositorioJuncalCuentasCorrientesTipo RepositorioJuncalCuentasCorrientesTipo { get; }
        IRepositorioJuncalEstadosInterno RepositorioJuncalEstadosInterno { get; }
        IRepositorioJuncalFactura RepositorioJuncalFactura { get; }
        IRepositorioJuncalFacturaMateriale RepositorioJuncalFacturaMateriale { get; }
        IRepositorioJuncalNotificacion RepositorioJuncalNotificacion { get; }
        IRepositorioJuncalCcMovimientoRemito RepositorioJuncalCcMovimientoRemito {  get; }
        IRepositorioJuncalCcTipoMovimiento RepositorioJuncalCcTipoMovimiento {  get; }
        IRepositorioJuncalProveedorListaPrecio RepositorioJuncalProveedorListaPrecio {  get; }
        IRepositorioJuncalProveedorListaPreciosMateriales RepositorioJuncalProveedorListaPreciosMateriales { get; }
        IRepositorioJuncalProveedorCuentaCorriente RepositorioJuncalProveedorCuentaCorriente {  get; }
    }
}
