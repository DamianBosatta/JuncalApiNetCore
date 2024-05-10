using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos.Item;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JuncalApi.Servicios.Remito;
using JuncalApi.Modelos.Codigos_Utiles;

using Serilog;
using System;
using JuncalApi.Dto.DtoRequerido.DtoFacturarOrden;
using JuncalApi.Servicios.Facturar;

namespace JuncalApi.Controllers
{
    //    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class OrdenInternoController : Controller
    {
        private readonly ILogger<OrdenInternoController> _logger;
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IFacturarServicio _facturar;

        public OrdenInternoController(ILogger<OrdenInternoController> logger, IUnidadDeTrabajo uow, IMapper mapper, IFacturarServicio facturar)
        {
            _logger = logger;
            _uow = uow;
            _mapper = mapper;
            _facturar = facturar;
        }

        [Route("TodosLosRemitos/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoRespuesta>>> GetOrdenes()
        {
            try
            {
                var ListaOrdenes = _uow.RepositorioJuncalOrdenInterno.GetAllRemitos().ToList();

                if (ListaOrdenes.Count() > 0)
                {
                    List<RemitoRespuesta> listaOrdenesRespuesta = _mapper.Map<List<RemitoRespuesta>>(ListaOrdenes);
                    return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = listaOrdenesRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los remitos");
                return StatusCode(500, "Ocurrió un error al obtener los remitos");
            }
        }

        [Route("MaterialesRecogidos/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoRespuesta>>> GetOrdenesRecogidos()
        {
            try
            {
                var ListaOrdenes = _uow.RepositorioJuncalOrdenInterno.GetAllRemitos().Where(a => a.IdEstado == CodigosUtiles.CerradoFacturado).ToList();

                if (ListaOrdenes.Count() > 0)
                {
                    List<RemitoRespuesta> listaOrdenesRespuesta = _mapper.Map<List<RemitoRespuesta>>(ListaOrdenes);
                    return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = listaOrdenesRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los remitos recogidos");
                return StatusCode(500, "Ocurrió un error al obtener los remitos recogidos");
            }
        }

       
        [HttpPost]
        public ActionResult CargarOrdenes([FromBody] OrdenInternaRequerida ordenReq)
        {
            try
            {
                var orden = _uow.RepositorioJuncalOrdenInterno.GetByCondition(c => c.Remito == ordenReq.Remito && c.Isdeleted == false);

                if (orden is null)
                {
                    JuncalOrdenInterno ordenNuevo = _mapper.Map<JuncalOrdenInterno>(ordenReq);
                    _uow.RepositorioJuncalOrdenInterno.Insert(ordenNuevo);
                    OrdenInternaRespuesta ordenRes = new();
                    _mapper.Map(ordenNuevo, ordenRes);
                    return Ok(new { success = true, message = "La Orden Interna Fue Creada Con Exito", result = ordenRes });
                }
                OrdenInternaRespuesta ordenExiste = new();
                _mapper.Map(orden, ordenExiste);
                return Ok(new { success = false, message = " Ya Hay Esta Cargado Ese Numero De Remito ", result = ordenExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar las órdenes internas");
                return StatusCode(500, "Ocurrió un error al cargar las órdenes internas");
            }
        }

        [Route("Facturar/")]
        [HttpPost]
        public ActionResult FacturarOrdenesInternas([FromBody] FacturarRemitoInternoRequerido facturarRequerido)
        {
            try
            {
                if (facturarRequerido is null)
                {
                    return Ok(new { success = false, message = "El Objeto Requerido Ingresó Nulo", result = 204 });
                }

                if (facturarRequerido.Cerrar)
                {
                    var ordenInterna = _uow.RepositorioJuncalOrdenInterno.GetById(facturarRequerido.OrdenInterno.Id);

                    if (ordenInterna is not null)
                    {
                        ordenInterna.IdEstadoInterno = 2;
                        _uow.RepositorioJuncalOrdenInterno.Update(ordenInterna);
                        return Ok(new { success = true, message = "El remito fue cerrado", result = 200 });
                    }
                }
                else
                {
                    var cuentaCorriente = _facturar.FacturarRemitoInterno(facturarRequerido);

                    if (cuentaCorriente != null)
                    {
                        var confirmacionInsertCc = _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(cuentaCorriente);

                        if (confirmacionInsertCc)
                        {
                            var ordenInterna = _uow.RepositorioJuncalOrdenInterno.GetById(facturarRequerido.OrdenInterno.Id);
                            if (ordenInterna is not null)
                            {
                                ordenInterna.IdEstadoInterno = 2;
                                _uow.RepositorioJuncalOrdenInterno.Update(ordenInterna);
                            }
                        }

                        return Ok(new { success = true, message = $"El remito fue facturado por un total de: ${cuentaCorriente.Importe}", result = 200 });
                    }
                    else
                    {
                        var ordenInterna = _uow.RepositorioJuncalOrdenInterno.GetById(facturarRequerido.OrdenInterno.Id);
                        if (ordenInterna is not null)
                        {
                            ordenInterna.IdEstadoInterno = 2;
                            _uow.RepositorioJuncalOrdenInterno.Update(ordenInterna);
                        }
                        return Ok(new { success = true, message = $"El remito fue cerrado", result = 200 });
                    }
                }

                return Ok(new { success = false, message = "Operación no válida", result = 204 });
            }
            catch (Exception ex)
            {
                // Manejar excepciones y devolver un mensaje de error apropiado
                return Ok(new { success = false, message = "Ocurrió un error al procesar la solicitud", result = 500 });
            }
        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrdenInterna(int id)
        {
            try
            {
                var orden = _uow.RepositorioJuncalOrdenInterno.GetById(id);
                if (orden != null && orden.Isdeleted == false)
                {
                    orden.Isdeleted = true;
                    _uow.RepositorioJuncalOrdenInterno.Update(orden);
                    OrdenInternaRespuesta ordenRes = new();
                    _mapper.Map(orden, ordenRes);

                    return Ok(new { success = true, message = "La Orden Fue Eliminada ", result = ordenRes });
                }
                return Ok(new { success = false, message = "La Orden No Fue Encontrado", result = new OrdenInternaRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la orden interna");
                return StatusCode(500, "Ocurrió un error al eliminar la orden interna");
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditOrdenInterna(int id, OrdenInternaRequerida ordenEdit)
        {
            try
            {
                var orden = _uow.RepositorioJuncalOrdenInterno.GetById(id);

                if (orden != null && orden.Isdeleted == false)
                {
                    _mapper.Map(ordenEdit, orden);
                    _uow.RepositorioJuncalOrdenInterno.Update(orden);
                    OrdenInternaRespuesta ordenRes = new();
                    _mapper.Map(orden, ordenRes);
                    return Ok(new { success = true, message = "La Orden Fue Actualizada", result = ordenRes });
                }

                return Ok(new { success = false, message = "La Orden No Fue Encontrada ", result = new OrdenInternaRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la orden interna");
                return StatusCode(500, "Ocurrió un error al editar la orden interna");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRemitoInternoById(int id)
        {
            try
            {
                ItemRemitoInterno orden = _uow.RepositorioJuncalOrdenInterno.GetRemito(id);

                if (orden != null)
                {
                    RemitoRespuesta response = new RemitoRespuesta();
                    _mapper.Map(orden, response);

                    return Ok(new { success = true, message = "Response Confirmado", result = response });
                }

                return Ok(new { success = false, message = "No Se Encontro Remito", result = new RemitoRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el remito interno por ID");
                return StatusCode(500, "Ocurrió un error al obtener el remito interno por ID");
            }
        }
    }
}
