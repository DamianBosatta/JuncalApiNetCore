using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRequerido.DtoFacturarOrden;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Codigos_Utiles;
using JuncalApi.Servicios.Facturar;
using JuncalApi.Servicios.Remito;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IFacturarServicio _serviceFacturar;
        private readonly IServiceRemito _serviceRemito;
        private readonly ILogger<OrdenController> _logger;

        public OrdenController(IUnidadDeTrabajo uow, IMapper mapper, IFacturarServicio serviceFacturar, IServiceRemito serviceRemito, ILogger<OrdenController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _serviceFacturar = serviceFacturar;
            _serviceRemito = serviceRemito;
            _logger = logger;
        }

   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoRespuesta>>> GetOrdenes()
        {
            try
            {
                var ListaOrdenes = _uow.RepositorioJuncalOrden.GetRemito(0);

                if (ListaOrdenes.Count() > 0 && ListaOrdenes != null)
                {
                    return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = ListaOrdenes });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoRespuesta>() });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Ordenes," +
                " A Continuacion Encontraras Mas Informacion -> ->");              
                throw new InvalidOperationException("Excepcion Al Obtener Lista En GetOrdenes(Controller Orden)");
            }
        }

        [HttpGet("api/pendientes")]
        public async Task<ActionResult<IEnumerable<RemitosPendientesRespuesta>>> GetPendientes()
        {
            try
            {
                var ListaOrdenesPendientes = _uow.RepositorioJuncalOrden.GetRemitosPendientes();

                if (ListaOrdenesPendientes.Count() > 0)
                {
                    return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = ListaOrdenesPendientes });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitosPendientesRespuesta>() });
            }
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Ordenes," +
              " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion Al Obtener Lista En GetPendientes(Controller Orden)");
            }
        }

        [HttpPost]
        public ActionResult CargarOrdenes([FromBody] OrdenRequerido ordenReq)
        {
            try
            {
                var orden = _uow.RepositorioJuncalOrden.GetByCondition(c => c.Remito == ordenReq.Remito);

                if (orden is null)
                {
                    JuncalOrden ordenNuevo = _mapper.Map<JuncalOrden>(ordenReq);
                    _uow.RepositorioJuncalOrden.Insert(ordenNuevo);
                    OrdenRespuesta ordenRes = new();
                    _mapper.Map(ordenNuevo, ordenRes);
                    return Ok(new { success = true, message = "La Orden Fue Creada Con Exito", result = ordenRes });
                }

                OrdenRespuesta ordenExiste = new();
                _mapper.Map(orden, ordenExiste);
                return Ok(new { success = false, message = " Ya esta Cargado Ese Numero De Remito ", result = ordenExiste });
            }
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Ordenes," +
             " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion Al Insertar en CargarOrdenes(Controller Orden)");
            }
        }

        //#region  FACTURAR ORDEN

        //[Route("Facturar/")]
        //[HttpPost]
        //public ActionResult FacturarOrdenes([FromBody] List<FacturarOrdenRequerido> listFacturarRequerido)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid || listFacturarRequerido is null || listFacturarRequerido.Count == 0)
        //        {
        //            return BadRequest(new { success = false, message = "Datos de entrada inválidos", result = 400 });
        //        }

        //        var cuentasCorrientes = _serviceFacturar.FacturarRemitoExterno(listFacturarRequerido);

        //        if (cuentasCorrientes == null || cuentasCorrientes.Count == 0)
        //        {
        //            return NotFound(new { success = false, message = "No se encontraron cuentas corrientes", result = 404 });
        //        }

        //        List<int> NumeroRemitosFacturados = new List<int>();
              
        //        foreach (var cuentaCorriente in cuentasCorrientes)
        //        {
        //            var confirmacionInsertCc = InsertarCuentaCorriente(cuentaCorriente);

        //            if (!confirmacionInsertCc)
        //            {
        //                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Ordenes," +
        //         " A Continuacion Encontraras Mas Informacion -> ->");
        //                throw new InvalidOperationException("Excepcion Al Insertar En Cuenta Corriente(Controller Orden)");

        //            }

        //            ActualizarEstadoOrdenInterna((int)cuentaCorriente.IdRemitoExterno);
        //            NumeroRemitosFacturados.Add((int)cuentaCorriente.IdRemitoExterno);
        //        }

        //        decimal totalImporte = (decimal)cuentasCorrientes.Sum(cc => cc.Importe);

        //        return Ok(new { success = true, message = $"Los Remitos fueron facturados por un total de: ${totalImporte} , Los Remitos Facturados fueron " +
        //            $" los numero : ${NumeroRemitosFacturados}", result = 200 });
        //    }
        //    catch (Exception)
        //    {
        //        _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Ordenes," +
        //        " A Continuacion Encontraras Mas Informacion -> ->");
        //        throw new InvalidOperationException("Excepcion Al Insertar Factura(Controller Orden)");
        //    }
        //}


        //private bool InsertarCuentaCorriente(JuncalProveedorCuentaCorriente cuentaCorriente)
        //{
        //    return _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(cuentaCorriente);
        //}

        //private void ActualizarEstadoOrdenInterna(int idRemito)
        //{
        //    var orden = _uow.RepositorioJuncalOrden.GetById(idRemito);
        //    if (orden is not null)
        //    {
        //        orden.IdEstado = CodigosUtiles.Facturado;
        //        _uow.RepositorioJuncalOrden.Update(orden);
        //    }
        //}
        
        //#endregion

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrden(int id)
        {
            try
            {
                var orden = _uow.RepositorioJuncalOrden.GetById(id);

                if (orden != null && orden.Isdeleted == false)
                {
                    orden.Isdeleted = true;
                    _uow.RepositorioJuncalOrden.Update(orden);
                    OrdenRespuesta ordenRes = new();
                    _mapper.Map(orden, ordenRes);

                    return Ok(new { success = true, message = "La Orden Fue Eliminada ", result = ordenRes });
                }

                return Ok(new { success = false, message = "La Orden No Fue Encontrada", result = new OrdenRespuesta() == null });
            }
            catch (Exception)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Ordenes," +
                    " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion Al Desactivar Orden(Controller Orden)");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrden(int id, OrdenRequerido ordenEdit)
        {
            try
            {
                var orden = _uow.RepositorioJuncalOrden.GetById(id);

                if (orden != null && orden.Isdeleted == false)
                {
                    _mapper.Map(ordenEdit, orden);
                    _uow.RepositorioJuncalOrden.Update(orden);
                    OrdenRespuesta ordenRes = new();
                    _mapper.Map(orden, ordenRes);
                    return Ok(new { success = true, message = "La Orden Fue Actualizada", result = ordenRes });
                }

                return Ok(new { success = false, message = "La Orden No Fue Encontrada ", result = new OrdenRespuesta() == null });
            }
            catch (Exception)
            {
             _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Ordenes," +
             " A Continuacion Encontraras Mas Informacion -> ->");
             throw new InvalidOperationException("Excepcion Al Editar Orden(Controller Orden)");
            }
        }

        [HttpGet("remitos/{idOrden}")]
        public async Task<IActionResult> GetRemitoById(int idOrden)
        {
            try
            {
                var orden = await Task.Run(() => _serviceRemito.GetRemitos(idOrden));

                if (orden != null)
                {
                    return Ok(new { success = true, message = "Response Confirmado", result = orden });
                }

                return Ok(new { success = false, message = "No Se Encontro Remito", result = orden });
            }
            catch (Exception ex)
            {
                _logger.LogError("ATENCION!! Capturamos Error En la Controladora De Ordenes," +
                    " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion Al Obtener Remito Por Id(Controller Orden)");
            }
        }
    }
}

