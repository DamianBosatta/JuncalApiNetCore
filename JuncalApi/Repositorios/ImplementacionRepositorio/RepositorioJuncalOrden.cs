using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;


namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrden:RepositorioGenerico<JuncalOrden>,IRepositorioJuncalOrdencs
    {
        public RepositorioJuncalOrden(JuncalContext db) : base(db)
        {
        }
    
        #region GetRemito

        /// <summary>
        /// Obtiene una lista de objetos JuncalOrden para el Id de la orden especificado.
        /// Si se proporciona un Id de orden específico, devuelve solo esa orden. De lo contrario, devuelve todas las órdenes.
        /// </summary>
        /// <param name="idOrden">Id de la orden (opcional)</param>
        /// <returns>Lista de objetos JuncalOrden</returns>
        public List<JuncalOrden>? GetRemito(int idOrden)
        {
            var query = (from _orden in _db.JuncalOrdens.Where(a => a.Isdeleted == false)
                         join aceria in _db.JuncalAceria.Where(a => a.Isdeleted == false) on _orden.IdAceria equals aceria.Id into JoinAceria
                         from jaceria in JoinAceria.DefaultIfEmpty()
                         join contrato in _db.JuncalContratos.Where(a => a.Isdeleted == false) on _orden.IdContrato equals contrato.Id into JoinContrato
                         from jcontrato in JoinContrato.DefaultIfEmpty()
                         join camion in _db.JuncalCamions.Where(a => a.Isdeleted == false) on _orden.IdCamion equals camion.Id into JoinCamion
                         from jcamion in JoinCamion.DefaultIfEmpty()
                         join chofer in _db.JuncalChofers.Where(a => a.Isdeleted == false) on jcamion.IdChofer equals chofer.Id into JoinChofer
                         from jchofer in JoinChofer.DefaultIfEmpty()
                         join transportista in _db.JuncalTransportista.Where(a => a.Isdeleted == false) on jcamion.IdTransportista equals transportista.Id into JoinTransportista
                         from jtransportista in JoinTransportista.DefaultIfEmpty()
                         join acoplado in _db.JuncalAcoplados.Where(a => a.Isdeleted == false) on _orden.IdAcoplado equals acoplado.Id into JoinAcoplado
                         from jacoplado in JoinAcoplado.DefaultIfEmpty()
                         join estado in _db.JuncalEstados.Where(a => a.Isdeleted == false) on _orden.IdEstado equals estado.Id into JoinEstado
                         from jestado in JoinEstado.DefaultIfEmpty()
                         join proveedor in _db.JuncalProveedors.Where(a => a.Isdeleted == false) on _orden.IdProveedor equals proveedor.Id into JoinProveedor
                         from jproveedor in JoinProveedor.DefaultIfEmpty()
                         join direccionProveedor in _db.JuncalDireccionProveedors on _orden.IdDireccionProveedor equals direccionProveedor.Id into JoinDireccionProveedor
                         from jdireccionProveedor in JoinDireccionProveedor.DefaultIfEmpty()
                         select new JuncalOrden
                         {
                             Id = _orden.Id,
                             Remito = _orden.Remito,
                             Observaciones = _orden.Observaciones,
                             IdAceria = _orden.IdAceria,
                             NombreAceria = jaceria.Nombre,
                             DireccionAceria = jaceria.Direccion,
                             CuitAceria = jaceria.Cuit,
                             CodigoProveedorAceria = jaceria.CodProveedor,
                             IdContrato = jcontrato.Id,
                             NumeroContrato = jcontrato.Numero,
                             IdCamion = _orden.IdCamion,
                             PatenteCamion = jcamion.Patente,
                             IdChofer = jchofer.Id,
                             NombreChofer = jchofer.Nombre,
                             ApellidoChofer = jchofer.Apellido,
                             LicenciaChofer = jchofer.Dni,
                             IdTransportista = jtransportista.Id,
                             NombreTransportista = jtransportista.Nombre,
                             IdAcoplado = _orden.IdAcoplado,
                             PatenteAcoplado = jacoplado.Patente,
                             IdEstado = _orden.IdEstado,
                             DescripcionEstado = jestado.Nombre,
                             IdProveedor = _orden.IdProveedor,
                             NombreProveedor = jproveedor.Nombre,
                             IdDireccionProveedor = _orden.IdDireccionProveedor,
                             DireccionProveedor = jdireccionProveedor.Direccion,
                         });

            // Filtrar por Id de orden si se proporciona
            query = idOrden == 0 ? query : query.Where(a => a.Id == idOrden);

            return query.ToList();
        }

        #endregion 

    }
}
