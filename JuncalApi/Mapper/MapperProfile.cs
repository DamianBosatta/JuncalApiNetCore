using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;


namespace JuncalApi.Mapper
{
    public class MapperProfile:Profile
    {

        public MapperProfile()
        {
            #region CAMION
            CreateMap<CamionRequerido, JuncalCamion>();
            CreateMap<JuncalCamion,CamionRespuesta >();
          
            #endregion

            #region ACOPLADO
            CreateMap<AcopladoRequerido, JuncalAcoplado>();
            CreateMap<JuncalAcoplado, AcopladoRespuesta>();
            //CreateMap<ItemAcoplado, AcopladoRespuesta>();
            #endregion

            #region TIPO CAMION
            CreateMap<TipoCamionRequerido, JuncalTipoCamion>();
            CreateMap<JuncalTipoCamion, TipoCamionRespuesta>();
            #endregion

            #region CHOFER
            CreateMap<ChoferRequerido,JuncalChofer>();
            CreateMap<JuncalChofer,ChoferRespuesta>();
            #endregion
            
            #region TRANSPORTISTA
            CreateMap<TransportistaRequerido, JuncalTransportistum>();
            CreateMap<JuncalTransportistum, TransportistaRespuesta>();
            #endregion
         
            #region ACERIA
            CreateMap<AceriaRequerido, JuncalAcerium>();
            CreateMap< JuncalAcerium,AceriaRespuesta>();
            #endregion
          
            #region ACERIA MATERIAL
            CreateMap<AceriaMaterialRequerido, JuncalAceriaMaterial>();
            CreateMap<JuncalAceriaMaterial, AceriaMaterialRespuesta>();
          

            #endregion

            #region MATERIAL
            CreateMap<MaterialRequerido, JuncalMaterial>();
            CreateMap<JuncalMaterial, MaterialRespuesta>();
            #endregion

            #region ROLES
            CreateMap<RolesRequerido, JuncalRole>();
            CreateMap<JuncalRole, RolesRespuesta>();
            #endregion

            #region USUARIO
            CreateMap<UsuarioRequerido, JuncalUsuario>();
            CreateMap<JuncalUsuario, UsuarioRespuesta>();
            #endregion

            #region CONTRATO
            CreateMap<ContratoRequerido, JuncalContrato>();
            CreateMap<JuncalContrato, ContratoRespuesta>();
           


            #endregion

            #region CONTRATO ITEM

            CreateMap<ContratoItemRequerido, JuncalContratoItem>();
            CreateMap<JuncalContratoItem, ContratoItemRespuesta>();
            

            #endregion

            #region ESTADO
            CreateMap<EstadoRequerido, JuncalEstado>();
            CreateMap<JuncalEstado, EstadoRespuesta>();
            #endregion

            #region ESTADO RECLAMO
            CreateMap<EstadosReclamoRequerido, JuncalEstadosReclamo>();
            CreateMap<JuncalEstadosReclamo, EstadosReclamoResponse>();
            #endregion

            #region PROVEEDOR
            CreateMap<ProveedorRequerido, JuncalProveedor>();
            CreateMap<JuncalProveedor, ProveedorRespuesta>();
            #endregion

            #region DIRECCION PROVEEDOR
            CreateMap<DireccionProveedorRequerido, JuncalDireccionProveedor>();
            CreateMap<JuncalDireccionProveedor, DireccionProveedorRespuesta>();
            #endregion

            #region MATERIAL PROVEEDOR
            CreateMap<MaterialProveedorRequerido, JuncalMaterialProveedor>();
            CreateMap<JuncalMaterialProveedor, MaterialProveedorRespuesta>();
            #endregion

            #region ORDEN
            CreateMap<OrdenRequerido, JuncalOrden>();
            CreateMap<JuncalOrden, OrdenRespuesta>();
            //CreateMap<ItemRemito, RemitoResponse>().ReverseMap();
            CreateMap<ItemRemitoInterno, RemitoRespuesta>().ReverseMap();
            #endregion

            #region ORDEN MATERIAL
            CreateMap<OrdenMaterialRequerido, JuncalOrdenMarterial>();
            CreateMap<JuncalOrdenMarterial, OrdenMaterialRespuesta>();
            #endregion

            #region ORDEN MATERIAL INTERNO RECIBIDO
            CreateMap<OrdenMaterialInternoRecibidoRequerido, JuncalOrdenMaterialInternoRecibido>();
            CreateMap<JuncalOrdenMaterialInternoRecibido, OrdenMaterialInternoRecibidoRespuesta>();
            #endregion

            #region ORDEN MATERIAL INTERNO RECOGIDO
            CreateMap<OrdenMaterialInternoRecogidoRequerido, JuncalOrdenMaterialInternoRecogido>();
            CreateMap<JuncalOrdenMaterialInternoRecogido, OrdenMaterialInternoRecogidoRespuesta>();
            #endregion
            
            #region ORDEN INTERNA
            CreateMap<OrdenInternaRequerida, JuncalOrdenInterno>();
            CreateMap<JuncalOrdenInterno, OrdenInternaRespuesta>();
            #endregion

            #region REMITO RECLAMO
            CreateMap<RemitosReclamadoRequerido, JuncalRemitosReclamado>();
            CreateMap<JuncalRemitosReclamado, RemitoReclamadoRespuesta>();
            #endregion

            #region SUCURSAL
            CreateMap<SucursalRequerida, JuncalSucursal>();
            CreateMap<JuncalSucursal, SucursalRespuesta>();
            #endregion

            #region EXCEL CONFIG

            CreateMap<ExcelConfigRequerido, JuncalExcelConfig>();
            CreateMap<JuncalExcelConfig, ExcelConfigRespuesta>();
            #endregion

            #region TIPO ACOPLADO
            CreateMap<TipoAcopladoRequerido, JuncalTipoAcoplado>();
            CreateMap<JuncalTipoAcoplado, TipoAcopladoRespuesta>();
            #endregion

            #region PROVEEDOR PRESUPUESTO
            CreateMap<ProveedorPresupuestoRequerido, JuncalProveedorPresupuesto>();
            CreateMap<JuncalProveedorPresupuesto, ProveedorPresupuestoRespuesta>();

            #endregion

            #region PROVEEDOR PRESUPUESTO MATERIAL
            CreateMap<ProveedorPresupuestoMaterialRequerido, JuncalProveedorPresupuestoMateriale>();
            CreateMap<JuncalProveedorPresupuestoMateriale, ProveedorPresupuestoMaterialRespuesta>();

            #endregion

            #region REMITO ORDEN
            CreateMap<JuncalOrden, RemitoRespuesta>().ReverseMap();
            //CreateMap<JuncalTipoAcoplado, TipoAcopladoRespuesta>();
            #endregion

            #region TIPO PRE FACTURADO
            CreateMap<PreFacturadoRequerido,JuncalPreFacturar>();
            CreateMap<JuncalPreFacturar, PreFacturadoRespuesta>();
            #endregion

            #region TIPO PRE FACTURADO
            CreateMap<FacturaRequerida, JuncalFactura>();
            CreateMap<JuncalFactura, FacturaRespuesta>();
            #endregion


        }

    }
}
