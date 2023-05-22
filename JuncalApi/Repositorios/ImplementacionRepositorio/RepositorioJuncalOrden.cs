using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrden:RepositorioGenerico<JuncalOrden>,IRepositorioJuncalOrdencs
    {
        public RepositorioJuncalOrden(JuncalContext db) : base(db)
        {
        }

        public ItemRemito GetRemito(int id)
        {
            var query = from _orden in _db.JuncalOrdens.Where(a => a.Id == id && a.Isdeleted ==false)
                        join aceria in _db.JuncalAceria.Where(a=>a.Isdeleted==false) on _orden.IdAceria equals aceria.Id
                        join contrato in _db.JuncalContratos.Where(a=>a.Isdeleted==false ) on _orden.IdContrato equals contrato.Id
                        join camion in _db.JuncalCamions.Where(a => a.Isdeleted == false) on _orden.IdCamion equals camion.Id
                        join chofer in _db.JuncalChofers.Where(a => a.Isdeleted == false) on camion.IdChofer equals chofer.Id
                        join transportista in _db.JuncalTransportista.Where(a => a.Isdeleted == false) on camion.IdTransportista equals transportista.Id
                        join acoplado in _db.JuncalAcoplados.Where(a => a.Isdeleted == false) on _orden.IdAcoplado equals acoplado.Id
                        join estado in _db.JuncalEstados.Where(a => a.Isdeleted == false) on _orden.IdEstado equals estado.Id
                        join proveedor in _db.JuncalProveedors.Where(a => a.Isdeleted == false) on _orden.IdProveedor equals proveedor.Id
                        join direccionProveedor in _db.JuncalDireccionProveedors on _orden.IdDireccionProveedor equals direccionProveedor.Id
                        select new ItemRemito( _orden, aceria, contrato, camion, chofer, transportista, acoplado, estado, proveedor, direccionProveedor );


                       var respuesta = query is null ? new ItemRemito() : query.SingleOrDefault();

             return respuesta;
            

          

        }
    }
}
