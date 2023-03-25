using JuncalApi.DataBase;
using JuncalApi.Repositorios.ImplementacionRepositorio;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.UnidadDeTrabajo
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly JuncalContext _context;


        public UnidadDeTrabajo(JuncalContext context)
        {
            _context = context;

            this.RepositorioJuncalAceriaMaterial = new RepositorioJuncalAceriaMaterial(context);
            this.RepositorioJuncalAcerium= new RepositorioJuncalAcerium(context);
            this.RepositorioJuncalCamion= new RepositorioJuncalCamion(context);
            this.RepositorioJuncalChofer=new RepositorioJuncalChofer(context);
            this.RepositorioJuncalContrato= new RepositorioJuncalContrato(context);
            this.RepositorioJuncalContratoItem= new RepositorioJuncalContratoItem(context);
            this.RepositorioJuncalAcoplado=new RepositorioJuncalAcoplado(context);
            this.RepositorioJuncalEstado=new RepositorioJuncalEstado(context);
            this.RepositorioJuncalMaterial=new RepositorioJuncalMaterial(context);
            this.RepositorioJuncalMaterialProveedor=new RepositorioJuncalMaterialProveedor(context);
            this.RepositorioJuncalOrden = new RepositorioJuncalOrden(context);
            this.RepositorioJuncalOrdenMarterial=new RepositorioJuncalOrdenMarterial(context);
            this.RepositorioJuncalProveedor= new RepositorioJuncalProveedor(context);
            this.RepositorioJuncalRole=new RepositorioJuncalRole(context);
            this.RepositorioJuncalTransportistum=new RepositorioJuncalTransportistum(context);
            this.RepositorioJuncalUsuario=new RepositorioJuncalUsuario(context); 
            this.RepositorioJuncalTipoCamion= new RepositorioJuncalTipoCamion(context);
            this.RepositorioJuncalSucursal=new RepositorioJuncalSucursal(context);
            this.RepositorioJuncalDireccionProveedor = new RepositorioJuncalDireccionProveedores(context);
            this.RepositorioJuncalRemitoHistorial=new RepositorioJuncalRemitoHistorial(context);
            this.RepositorioJuncalTipoAcoplado = new RepositorioJuncalTipoAcoplado(context);

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

        public IRepositorioJuncalRemitoHistorial RepositorioJuncalRemitoHistorial { get; private set; }

        public IRepositorioJuncalTipoAcoplado RepositorioJuncalTipoAcoplado { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
