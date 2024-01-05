using JuncalApi.DataBase;
using JuncalApi.Repositorios.ImplementacionRepositorio;
using JuncalApi.Repositorios.InterfaceRepositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Entity;

namespace JuncalApi.UnidadDeTrabajo
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly JuncalContext _context;
        private readonly ILogger<UnidadDeTrabajo> _logger;
     

        public UnidadDeTrabajo(JuncalContext context, ILogger<UnidadDeTrabajo> logger)
        {
            _context = context;
            _logger = logger;

            this.RepositorioJuncalAceriaMaterial = new RepositorioJuncalAceriaMaterial(context, _logger);
            this.RepositorioJuncalAcerium= new RepositorioJuncalAcerium(context, _logger);
            this.RepositorioJuncalCamion= new RepositorioJuncalCamion(context, _logger);
            this.RepositorioJuncalChofer=new RepositorioJuncalChofer(context, _logger);
            this.RepositorioJuncalContrato= new RepositorioJuncalContrato(context, _logger);
            this.RepositorioJuncalContratoItem= new RepositorioJuncalContratoItem(context, _logger);
            this.RepositorioJuncalAcoplado=new RepositorioJuncalAcoplado(context, _logger);
            this.RepositorioJuncalEstado=new RepositorioJuncalEstado(context, _logger);
            this.RepositorioJuncalMaterial=new RepositorioJuncalMaterial(context, _logger);
            this.RepositorioJuncalMaterialProveedor=new RepositorioJuncalMaterialProveedor(context, _logger);
            this.RepositorioJuncalOrden = new RepositorioJuncalOrden(context, _logger);
            this.RepositorioJuncalOrdenMarterial=new RepositorioJuncalOrdenMarterial(context, _logger);
            this.RepositorioJuncalProveedor= new RepositorioJuncalProveedor(context, _logger);
            this.RepositorioJuncalRole=new RepositorioJuncalRole(context, _logger);
            this.RepositorioJuncalTransportistum=new RepositorioJuncalTransportistum(context, _logger);
            this.RepositorioJuncalUsuario=new RepositorioJuncalUsuario(context, _logger); 
            this.RepositorioJuncalTipoCamion= new RepositorioJuncalTipoCamion(context, _logger);
            this.RepositorioJuncalSucursal=new RepositorioJuncalSucursal(context, _logger);
            this.RepositorioJuncalDireccionProveedor = new RepositorioJuncalDireccionProveedores(context, _logger);           
            this.RepositorioJuncalTipoAcoplado = new RepositorioJuncalTipoAcoplado(context, _logger);
            this.RepositorioJuncalOrdenInterno=new RepositorioJuncalOrdenInterno(context, _logger);
            this.RepositorioJuncalOrdenMaterialInternoRecibido=new RepositorioJuncalOrdenMaterialInternoRecibido(context, _logger);
            this.RepositorioJuncalOrdenMaterialInternoRecogido= new RepositorioJuncalOrdenMaterialInternoRecogido(context, _logger);
            this.RepositorioJuncalExcelConfig=new RepositorioJuncalExcelConfig(context, _logger);
            this.RepositorioJuncalRemitosReclamado = new RepositorioJuncalRemitosReclamado(context, _logger);
            this.RepositorioJuncalEstadosReclamo = new RepositorioJuncalEstadosReclamo(context, _logger);
            this.RepositorioJuncalProveedorPresupuesto = new RepositorioJuncalProveedorPresupuesto(context, _logger);
            this.RepositorioJuncalProveedorPresupuestoMaterial = new RepositorioJuncalProveedorPresupuestoMaterial(context, _logger);
            this.RepositorioJuncalPreFactura = new RepositorioJuncalPreFacturar(context, _logger);         
            this.RepositorioJuncalEstadosInterno= new RepositorioJuncalEstadosInterno(context, _logger);
            this.RepositorioJuncalFactura= new RepositorioJuncalFactura(context, _logger);
            this.RepositorioJuncalFacturaMateriale = new RepositorioJuncalFacturaMateriale(context, _logger);
            this.RepositorioJuncalNotificacion = new RepositorioJuncalNotificacione(context, _logger);
            this.RepositorioJuncalCcMovimientoRemito= new RepositorioJuncalCcMovimientoRemito(context, _logger);  
            this.RepositorioJuncalCcTipoMovimiento= new RepositorioJuncalCcTipoMovimiento(context, _logger);
            this.RepositorioJuncalProveedorListaPrecio= new RepositorioJuncalProveedorListaPrecio(context, _logger); 
            this.RepositorioJuncalProveedorListaPreciosMateriales= new RepositorioJuncalProveedorListaPreciosMateriales(context, _logger);
            this.RepositorioJuncalProveedorCuentaCorriente= new RepositorioJuncalProveedorCuentaCorriente(context, _logger);
            this.RepositorioJuncalCuentaCorrientePendiente = new RepositorioJuncalCuentaCorrientePendiente(context, _logger);

        } 




        public IRepositorioJuncalAceriaMaterial RepositorioJuncalAceriaMaterial { get; private set; }


        public IRepositorioJuncalAcerium RepositorioJuncalAcerium { get; private set; }


        public IRepositorioJuncalCamion RepositorioJuncalCamion { get; private set; }

        public IRepositorioJuncalChofer RepositorioJuncalChofer { get; private set; }

        public IRepositorioJuncalContrato RepositorioJuncalContrato { get; private set; }


        public IRepositorioJuncalContratoItem RepositorioJuncalContratoItem { get; private set; }


        public IRepositorioJuncalAcoplado RepositorioJuncalAcoplado { get; private set; }


        public IRepositorioJuncalEstado RepositorioJuncalEstado { get; private set; }


        public IRepositorioJuncalMaterial RepositorioJuncalMaterial { get; private set; }


        public IRepositorioJuncalMaterialProveedor RepositorioJuncalMaterialProveedor { get; private set; }


        public IRepositorioJuncalOrdencs RepositorioJuncalOrden { get; private set; }


        public IRepositorioJuncalOrdenMarterial RepositorioJuncalOrdenMarterial { get; private set; }


        public IRepositorioJuncalProveedor RepositorioJuncalProveedor { get; private set; }


        public IRepositorioJuncalRole RepositorioJuncalRole { get; private set; }


        public IRepositorioJuncalTransportistum RepositorioJuncalTransportistum { get; private set; }


        public IRepositorioJuncalUsuario RepositorioJuncalUsuario { get; private set; }

        public IRepositorioJuncalTipoCamion RepositorioJuncalTipoCamion { get; private set; }

        public IRepositorioJuncalSucursal RepositorioJuncalSucursal { get; private set; }

        public IRepositorioJuncalDireccionProveedor RepositorioJuncalDireccionProveedor { get; private set; }
   
        public IRepositorioJuncalTipoAcoplado RepositorioJuncalTipoAcoplado { get; private set; }

        public IRepositorioJuncalOrdenInterno RepositorioJuncalOrdenInterno { get; private set; }

        public IRepositorioJuncalOrdenMaterialInternoRecibido RepositorioJuncalOrdenMaterialInternoRecibido { get; private set; }

        public IRepositorioJuncalOrdenMaterialInternoRecogido RepositorioJuncalOrdenMaterialInternoRecogido { get; private set; }

        public IRepositorioJuncalExcelConfig RepositorioJuncalExcelConfig { get; private set; }

        public IRepositorioJuncalRemitosReclamado RepositorioJuncalRemitosReclamado { get; private set; }

        public IRepositorioJuncalEstadosReclamo RepositorioJuncalEstadosReclamo { get; private set; }

        public IRepositorioJuncalProveedorPresupuesto RepositorioJuncalProveedorPresupuesto { get; private set; }

        public IRepositorioJuncalProveedorPresupuestoMaterial RepositorioJuncalProveedorPresupuestoMaterial { get; private set; }

        public IRepositorioJuncalPreFactura RepositorioJuncalPreFactura { get; private set; }

        public IRepositorioJuncalFactura RepositorioJuncalFactura { get; private set; }

        public IRepositorioJuncalFacturaMateriale RepositorioJuncalFacturaMateriale { get; private set; }

        public IRepositorioJuncalEstadosInterno RepositorioJuncalEstadosInterno { get; private set; }

        public IRepositorioJuncalNotificacion RepositorioJuncalNotificacion { get; private set; }

        public IRepositorioJuncalCcMovimientoRemito RepositorioJuncalCcMovimientoRemito { get; private set; }

        public IRepositorioJuncalCcTipoMovimiento RepositorioJuncalCcTipoMovimiento { get; private set; }
        
        public IRepositorioJuncalProveedorListaPrecio RepositorioJuncalProveedorListaPrecio { get; private set; }

        public IRepositorioJuncalProveedorListaPreciosMateriales RepositorioJuncalProveedorListaPreciosMateriales { get; private set; }

        public IRepositorioJuncalProveedorCuentaCorriente RepositorioJuncalProveedorCuentaCorriente { get; private set; }

        public IRepositorioJuncalCuentaCorrientePendiente RepositorioJuncalCuentaCorrientePendiente { get; private set; }
      

        public void Dispose()
        {
           
            _context.Dispose();
        }
    }
}
