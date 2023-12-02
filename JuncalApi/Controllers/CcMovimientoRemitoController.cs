using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CcMovimientoRemitoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CcMovimientoRemitoController> _logger;

        public CcMovimientoRemitoController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<CcMovimientoRemitoController> logger)
        {

            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CcMovimientoRemitoRespuesta>>> GetCcMovimientoRemito()
        {
            try
            {
                var ListaCcMovimientoRemito = _uow.RepositorioJuncalCcMovimientoRemito.GetAll();

                if (ListaCcMovimientoRemito.Any())
                {
                    List<CcMovimientoRemitoRespuesta> ListaCcMovimientoRemitoRespuesta = _mapper.Map<List<CcMovimientoRemitoRespuesta>>(ListaCcMovimientoRemito);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaCcMovimientoRemitoRespuesta });
                }

                return Ok(new { success = false, message = "La Lista Está Vacía", result = new List<CcMovimientoRemitoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de CcMovimientoRemito");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdCcMovimientoRemito(int id)
        {
            try
            {
                var CcMovimientoRemito = _uow.RepositorioJuncalCcMovimientoRemito.GetById(id);

                if (CcMovimientoRemito == null)
                {
                    return Ok(new { success = false, message = "No Se Encontró El Movimiento de Remito", result = new CcMovimientoRemitoRespuesta() });
                }

                var CcMovimientoRemitoResp = _mapper.Map<CcMovimientoRemitoRespuesta>(CcMovimientoRemito);

                return Ok(new { success = true, message = "Remito Encontrado", result = CcMovimientoRemitoResp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Movimiento de Remito por ID");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [HttpPost]
        public ActionResult CargarCcMovimientoRemito([FromBody] CcMovimientoRemitoRequerido CcMovimientoRemitoReq)
        {
            try
            {
                var CcMovimientoRemito = _mapper.Map<JuncalCcMovimientoRemito>(CcMovimientoRemitoReq);
                _uow.RepositorioJuncalCcMovimientoRemito.Insert(CcMovimientoRemito);

                var CcMovimientoRemitoRes = _mapper.Map<CcMovimientoRemitoRespuesta>(CcMovimientoRemito);
                return Ok(new { success = true, message = "El Movimiento De Remito Fue Creado Con Éxito", result = CcMovimientoRemitoRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar cargar el Movimiento de Remito");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditCcMovimientoRemito(int id, CcMovimientoRemitoRequerido CcMovimientoRemitoReq)
        {
            try
            {
                var CcMovimientoRemito = _uow.RepositorioJuncalCcMovimientoRemito.GetById(id);

                if (CcMovimientoRemito != null)
                {
                    _mapper.Map(CcMovimientoRemitoReq, CcMovimientoRemito);
                    _uow.RepositorioJuncalCcMovimientoRemito.Update(CcMovimientoRemito);

                    var CcMovimientoRemitoRes = _mapper.Map<CcMovimientoRemitoRespuesta>(CcMovimientoRemito);
                    return Ok(new { success = true, message = "El Movimiento De Remito Ha Sido Actualizado", result = CcMovimientoRemitoRes });
                }

                return Ok(new { success = false, message = "No Se Encontró El Movimiento De Remito", result = new CcMovimientoRemitoRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar editar el Movimiento de Remito");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }


    }
}
