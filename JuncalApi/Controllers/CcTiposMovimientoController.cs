using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CcTiposMovimientoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CcTiposMovimientoController> _logger;

        public CcTiposMovimientoController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<CcTiposMovimientoController> logger)
        {

            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CcTiposMovimientoRespuesta>>> GetCcTiposMovimiento()
        {
            try
            {
                var ListaCcTiposMovimiento = _uow.RepositorioJuncalCcTipoMovimiento.GetAll();

                if (ListaCcTiposMovimiento.Any())
                {
                    List<CcTiposMovimientoRespuesta> ListaCcTiposMovimientoRespuesta = _mapper.Map<List<CcTiposMovimientoRespuesta>>(ListaCcTiposMovimiento);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaCcTiposMovimientoRespuesta });
                }

                return Ok(new { success = false, message = "La Lista Está Vacía", result = new List<CcTiposMovimientoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de CcTiposMovimiento");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }


        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdCcTiposMovimiento(int id)
        {
            try
            {
                var CcTiposMovimiento = _uow.RepositorioJuncalCcTipoMovimiento.GetById(id);

                if (CcTiposMovimiento == null)
                {
                    return Ok(new { success = false, message = "No Se Encontró El Tipo De Movimiento", result = new CcTiposMovimientoRespuesta() });
                }

                var CcMovimientoRemitoResp = _mapper.Map<CcMovimientoRemitoRespuesta>(CcTiposMovimiento);
                return Ok(new { success = true, message = "Tipo de Movimiento Encontrado", result = CcMovimientoRemitoResp });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Tipo de Movimiento por ID");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [HttpPost]
        public ActionResult CargarCcTiposMovimiento([FromBody] CcTiposMovimientoRequerido CcTiposMovimientoReq)
        {
            try
            {
                var CcTiposMovimiento = _mapper.Map<JuncalCcTiposMovimiento>(CcTiposMovimientoReq);
                _uow.RepositorioJuncalCcTipoMovimiento.Insert(CcTiposMovimiento);

                var CcTiposMovimientoRes = _mapper.Map<CcTiposMovimientoRespuesta>(CcTiposMovimiento);
                return Ok(new { success = true, message = "El Tipo de Movimiento Fue Creado Con Éxito", result = CcTiposMovimientoRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar cargar el Tipo de Movimiento");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditCcTiposMovimiento(int id, CcTiposMovimientoRequerido CcTiposMovimientoReq)
        {
            try
            {
                var CcTiposMovimiento = _uow.RepositorioJuncalCcTipoMovimiento.GetById(id);

                if (CcTiposMovimiento != null)
                {
                    _mapper.Map(CcTiposMovimientoReq, CcTiposMovimiento);
                    _uow.RepositorioJuncalCcTipoMovimiento.Update(CcTiposMovimiento);

                    var CcTiposMovimientoRes = _mapper.Map<CcTiposMovimientoRespuesta>(CcTiposMovimiento);
                    return Ok(new { success = true, message = "El Tipo De Movimiento Ha Sido Actualizado", result = CcTiposMovimientoRes });
                }

                return Ok(new { success = false, message = "No Se Encontró El Tipo De Movimiento", result = new CcTiposMovimientoRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar editar el Tipo de Movimiento");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }



    }
}
