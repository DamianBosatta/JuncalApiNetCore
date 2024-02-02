using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;


namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrden:RepositorioGenerico<JuncalOrden>,IRepositorioJuncalOrdencs
    {
        
        public RepositorioJuncalOrden(JuncalContext db,ILogger logger) : base(db,logger)
        {
            
        }
    
        #region GET REMITO

        /// <summary>
        /// Obtiene una lista de objetos JuncalOrden para el Id de la orden especificado.
        /// Si se proporciona un Id de orden específico, devuelve solo esa orden. De lo contrario, devuelve todas las órdenes.
        /// </summary>
        /// <param name="idOrden">Id de la orden (opcional)</param>
        /// <returns>Lista de objetos JuncalOrden</returns>
        public List<RemitoRespuesta>? GetRemito(int idOrden)
        {
            try { 
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
                         select new RemitoRespuesta
                         {
                             Id = _orden.Id,
                             Remito = _orden.Remito,
                             Observaciones = _orden.Observaciones,
                             IdAceria = _orden.IdAceria,
                             NombreAceria = jaceria.Nombre,
                             DireccionAceria = jaceria.Direccion,
                             CuitAceria = jaceria.Cuit,
                             FechaRemito = _orden.Fecha,
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
                             DireccionProveedor = jdireccionProveedor.Direccion
                         });


            // Filtrar por Id de orden si se proporciona
            query = idOrden == 0 ? query : query.Where(a => a.Id == idOrden);

            return query.ToList();
        }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Ocurrió una excepción en GetRemito(Repositorio Orden)");
       
        throw;
    }
}

        #endregion

        #region GET REMITOS PENDIENTES
        public List<RemitosPendientesRespuesta> GetRemitosPendientes()
        {
            try { 
            
            var query = (from orden in _db.JuncalOrdens.Where(a => a.IdEstado > 1 && a.IdEstado < 4)
                         join ordenMaterial in _db.JuncalOrdenMarterials
                         on orden.Id equals ordenMaterial.IdOrden into materialJoin
                         from material in materialJoin.DefaultIfEmpty()
                         join aceria in _db.JuncalAceria
                         on orden.IdAceria equals aceria.Id into aceriaJoin
                         from aceria in aceriaJoin.DefaultIfEmpty()
                         join estado in _db.JuncalEstados
                         on orden.IdEstado equals estado.Id into estadoJoin
                         from estado in estadoJoin.DefaultIfEmpty()
                         join contrato in _db.JuncalContratos
                         on orden.IdContrato equals contrato.Id into contratoJoin
                         from contrato in contratoJoin.DefaultIfEmpty()
                         select new RemitosPendientesRespuesta
                         {
                             IdOrden = orden.Id,
                             IdAceria = aceria != null ? aceria.Id : 0,
                             NombreAceria = aceria != null ? aceria.Nombre : string.Empty,
                             IdContrato = contrato != null ? contrato.Id : 0,
                             IdEstado = estado != null ? estado.Id : 0,
                             DescripcionEstado = estado != null ? estado.Nombre : string.Empty,
                             ListaMaterialesOrden = _db.JuncalOrdenMarterials
                                .Where(om => om.IdOrden == orden.Id).ToList()
                         });
            
            return query.ToList();
        }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Ocurrió una excepción en GetRemitosPendientes(Repositorio Juncal Orden)");       
        throw;
    }
}
        #endregion


        public List<ItemReporteAcerias> ReporteAcerias(DateTime fechaReporte)
        {
            var query = from aceria in _db.JuncalAceria
                        join remito in _db.JuncalOrdens
                            on aceria.Id equals remito.IdAceria
                                into remitoJoin
                        from remito in remitoJoin.DefaultIfEmpty()
                        join materialesRemito in _db.JuncalOrdenMarterials
                            on remito.Id equals materialesRemito.IdOrden
                                into materialesRemitoJoin
                        from materialesRemito in materialesRemitoJoin.DefaultIfEmpty()
                        join materiales in _db.JuncalMaterials
                            on materialesRemito.IdMaterial equals materiales.Id
                                into materialesJoin
                        from materiales in materialesJoin.DefaultIfEmpty()
                        join preFacturar in _db.JuncalPreFacturars
                            on remito.Id equals preFacturar.IdOrden
                                into preFacturarJoin
                        from preFacturar in preFacturarJoin.DefaultIfEmpty()
                        join estado in _db.JuncalEstados
                            on remito.IdEstado equals estado.Id
                                into estadoJoin
                        from estado in estadoJoin.DefaultIfEmpty()
                        where remito.Fecha == fechaReporte
                        orderby aceria.Id
                        select new ItemReporteAcerias
                        {
                            IdAceria = aceria.Id,
                            DescripcionAceria = aceria.Nombre,
                            IdEstado = estado.Id,
                            DescripcionEstado = estado.Nombre,
                            IdMaterial = materiales.Id,
                            DescripcionMaterial = materiales.Nombre,
                            KgEnviados = materialesRemito.Peso.ToString(),
                            KgRecibidos = preFacturar == null ? "Aun no se facturó el material" : preFacturar.PesoNeto.ToString()
                        };


            var reporteAceriasList = query.ToList();
            return reporteAceriasList;
        }

    }
}
