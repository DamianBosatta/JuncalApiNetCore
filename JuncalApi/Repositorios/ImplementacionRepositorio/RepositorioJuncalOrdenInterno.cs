using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrdenInterno : RepositorioGenerico<JuncalOrdenInterno>, IRepositorioJuncalOrdenInterno
    {
        public RepositorioJuncalOrdenInterno(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region GET REMITO

        public ItemRemitoInterno GetRemito(int id)
        {
            var query = from _orden in _db.JuncalOrdenInternos.Where(a => a.Id == id && a.Isdeleted == false)
                        join aceria in _db.JuncalAceria.Where(a => a.Isdeleted == false) on _orden.IdAceria equals aceria.Id into JoinAceria
                        from aceria in JoinAceria.DefaultIfEmpty()
                        join contrato in _db.JuncalContratos.Where(a => a.Isdeleted == false) on _orden.IdContrato equals contrato.Id into JoinContrato
                        from contrato in JoinContrato.DefaultIfEmpty()
                        join camion in _db.JuncalCamions.Where(a => a.Isdeleted == false) on _orden.IdCamion equals camion.Id into JoinCamion
                        from camion in JoinCamion.DefaultIfEmpty()
						join chofer in _db.JuncalChofers.Where(a => a.Isdeleted == false) on _orden.IdChofer equals chofer.Id into JoinChofer
						from chofer in JoinChofer.DefaultIfEmpty()
						join transportista in _db.JuncalTransportista.Where(a => a.Isdeleted == false) on camion.IdTransportista equals transportista.Id into JoinTransportista
                        from transportista in JoinTransportista.DefaultIfEmpty()
                        join acoplado in _db.JuncalAcoplados.Where(a => a.Isdeleted == false) on _orden.IdAcoplado equals acoplado.Id into JoinAcoplado
                        from acoplado in JoinAcoplado.DefaultIfEmpty()
                        join estado in _db.JuncalEstadosInternos.Where(a => a.Isdeleted == false) on _orden.IdEstadoInterno equals estado.Id into JoinEstado
                        from estado in JoinEstado.DefaultIfEmpty()
                        join proveedor in _db.JuncalProveedors.Where(a => a.Isdeleted == false) on _orden.IdProveedor equals proveedor.Id into JoinProveedor
                        from proveedor in JoinProveedor.DefaultIfEmpty()
                        join direccionProveedor in _db.JuncalDireccionProveedors on _orden.IdDireccionProveedor equals direccionProveedor.Id into JoinDireccionProveedor
                        from direccionProveedor in JoinDireccionProveedor.DefaultIfEmpty()
                        select new {_orden, JoinAceria = aceria, JoinContrato=contrato,JoinCamion= camion,JoinTransportista=transportista, JoinAcoplado= acoplado,JoinEstado = estado, JoinProveedor=proveedor,JoinDireccionProveedor= direccionProveedor,JoinChofer=chofer};


            ItemRemitoInterno? remitoInterno = query.Select(result => new ItemRemitoInterno
            {
            RemitoOrden=result._orden,
            Aceria=result.JoinAceria,
            Contrato=result.JoinContrato,
            Camion=result.JoinCamion,      
            Chofer=result.JoinChofer,
            Transportista=result.JoinTransportista,
            Acoplado= result.JoinAcoplado,
            Estado=result.JoinEstado,
            Proveedor=result.JoinProveedor,
            DireccionProveedores=result.JoinDireccionProveedor

             
            }).FirstOrDefault();

            return remitoInterno;
        }
        #endregion

        #region GET ALL REMITOS

        public List<ItemRemitoInterno> GetAllRemitos()
        {
            var query = from _orden in _db.JuncalOrdenInternos.Where(a => a.Isdeleted == false)
                        join aceria in _db.JuncalAceria.Where(a => a.Isdeleted == false) on _orden.IdAceria equals aceria.Id into JoinAceria
                        from aceria in JoinAceria.DefaultIfEmpty()
						join chofer in _db.JuncalChofers.Where(a => a.Isdeleted == false) on _orden.IdChofer equals chofer.Id into JoinChofer
						from chofer in JoinChofer.DefaultIfEmpty()
						join contrato in _db.JuncalContratos.Where(a => a.Isdeleted == false) on _orden.IdContrato equals contrato.Id into JoinContrato
                        from contrato in JoinContrato.DefaultIfEmpty()
                        join camion in _db.JuncalCamions.Where(a => a.Isdeleted == false) on _orden.IdCamion equals camion.Id into JoinCamion
                        from camion in JoinCamion.DefaultIfEmpty()                    
                        join transportista in _db.JuncalTransportista.Where(a => a.Isdeleted == false) on camion.IdTransportista equals transportista.Id into JoinTransportista
                        from transportista in JoinTransportista.DefaultIfEmpty()
                        join acoplado in _db.JuncalAcoplados.Where(a => a.Isdeleted == false) on _orden.IdAcoplado equals acoplado.Id into JoinAcoplado
                        from acoplado in JoinAcoplado.DefaultIfEmpty()
                        join estado in _db.JuncalEstadosInternos.Where(a => a.Isdeleted == false) on _orden.IdEstadoInterno equals estado.Id into JoinEstado
                        from estado in JoinEstado.DefaultIfEmpty()
                        join proveedor in _db.JuncalProveedors.Where(a => a.Isdeleted == false) on _orden.IdProveedor equals proveedor.Id into JoinProveedor
                        from proveedor in JoinProveedor.DefaultIfEmpty()
                        join direccionProveedor in _db.JuncalDireccionProveedors on _orden.IdDireccionProveedor equals direccionProveedor.Id into JoinDireccionProveedor
                        from direccionProveedor in JoinDireccionProveedor.DefaultIfEmpty()
                        select new { _orden, JoinAceria = aceria, JoinContrato = contrato,JoinChofer=chofer ,JoinCamion = camion, JoinTransportista = transportista, JoinAcoplado = acoplado, JoinEstado = estado, JoinProveedor = proveedor, JoinDireccionProveedor = direccionProveedor };


            List<ItemRemitoInterno> listaRemitos = new List<ItemRemitoInterno>();


            foreach (var remito in query)
            {
                ItemRemitoInterno itemRemito = new ItemRemitoInterno(remito._orden, remito.JoinAceria, remito.JoinContrato, remito.JoinCamion,
                remito.JoinTransportista, remito.JoinAcoplado, remito.JoinEstado, remito.JoinProveedor, remito.JoinDireccionProveedor,remito.JoinChofer);

                listaRemitos.Add(itemRemito);


            }




            return listaRemitos;
        }
        #endregion
    }
}
