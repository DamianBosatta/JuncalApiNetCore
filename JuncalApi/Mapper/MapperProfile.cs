using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

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

            #region PROVEEDOR
            CreateMap<ProveedorRequerido, JuncalProveedor>();
            CreateMap<JuncalProveedor, ProveedorRespuesta>();
            #endregion

            #region MATERIAL PROVEEDOR
            CreateMap<MaterialProveedorRequerido, JuncalMaterialProveedor>();
            CreateMap<JuncalMaterialProveedor, MaterialProveedorRespuesta>();
            #endregion

            #region ORDEN
            CreateMap<OrdenRequerido, JuncalOrden>();
            CreateMap<JuncalOrden, OrdenRespuesta>();
            #endregion

            #region ORDEN MATERIAL
            CreateMap<OrdenMaterialRequerido, JuncalOrdenMarterial>();
            CreateMap<JuncalOrdenMarterial, OrdenMaterialRespuesta>();
            #endregion

            #region SUCURSAL
            CreateMap<SucursalRequerida, JuncalSucursal>();
            CreateMap<JuncalSucursal, SucursalRespuesta>();
            #endregion

            #region DIRECCION PROVEEDOR
            CreateMap<DireccionProveedorRequerido, JuncalDireccionProveedor>();
            CreateMap<JuncalDireccionProveedor, DireccionProveedorRespuesta>();
            #endregion


        }



    }
}
