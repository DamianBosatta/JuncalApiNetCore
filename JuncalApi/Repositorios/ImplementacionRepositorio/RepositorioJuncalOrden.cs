using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Items;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrden:RepositorioGenerico<JuncalOrden>,IRepositorioJuncalOrdencs
    {
        public RepositorioJuncalOrden(JuncalContext db) : base(db)
        {
        }

        public ItemOrden GetRemito(int id)
        {
            var orden = _db.JuncalOrdens.Where(a=>a.Id==id);

            if (orden != null)
            {
                var query = from _orden in orden
                            join aceria in _db.JuncalAceria on _orden.IdAceria equals aceria.Id
                            join contrato in _db.JuncalContratos on _orden.IdContrato equals contrato.Id
                            join camion in _db.JuncalCamions on _orden.IdCamion equals camion.Id
                            join chofer in _db.JuncalChofers on camion.IdChofer equals chofer.Id
                            join transportista in _db.JuncalTransportista on camion.IdTransportista equals transportista.Id
                            join acoplado in _db.JuncalAcoplados on _orden.IdAcoplado equals acoplado.Id
                            join estado in _db.JuncalEstados on _orden.IdEstado equals estado.Id
                            join proveedor in _db.JuncalProveedors on _orden.IdProveedor equals proveedor.Id
                            join direccionProveedor in _db.JuncalDireccionProveedors on _orden.IdDireccionProveedor equals direccionProveedor.Id
                            select new ItemOrden(_orden, aceria, contrato, camion, chofer, transportista, acoplado, estado, proveedor, direccionProveedor);

                return (ItemOrden)query.SingleOrDefault();

            }

            return new ItemOrden();
          
        }
    }
}
