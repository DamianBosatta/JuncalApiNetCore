using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{   [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaCorrientePendienteController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ProveedorCuentaCorrienteController> _logger;

        public CuentaCorrientePendienteController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ProveedorCuentaCorrienteController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentaCorrientePendienteRespuesta>>> GetCcPendiente(int idProveedor) 
        {
            try
            {
                var ListaCcPendiente = _uow.RepositorioJuncalCuentaCorrientePendiente.GetCCPendiente(idProveedor);



                if (ListaCcPendiente.Any())
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaCcPendiente });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<CuentaCorrientePendienteRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetCcPendiente");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [HttpPost]
        public ActionResult CargarCcPendiente([FromBody] CuentaCorrientePendienteRequerido CCPendienteRequerido)
        {
            try
            {
                var CCPendiente = _mapper.Map<JuncalCuentaCorrientePendiente>(CCPendienteRequerido);
              
                _uow.RepositorioJuncalCuentaCorrientePendiente.Insert(CCPendiente);
              

                return Ok(new { success = true, message = "La CC Pendiente Fue Creada Con Éxito", result = 200 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CargarCcPendiente");
                return StatusCode(500, "Error interno del servidor");
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> EditCcPendiente(int id, CuentaCorrientePendienteRequerido CcPendienteReq)
        {
            try
            {
                var CcPendiente = _uow.RepositorioJuncalCuentaCorrientePendiente.GetById(id);

                if (CcPendiente != null)
                {
                    CcPendiente = _mapper.Map<JuncalCuentaCorrientePendiente>(CcPendienteReq);
                    _uow.RepositorioJuncalCuentaCorrientePendiente.Update(CcPendiente);              

                    return Ok(new { success = true, message = "La CC Pendiente Ha Sido Actualizada", result = 200 });
                }

                return Ok(new { success = false, message = "No Se Encontró La CC Pendiente", result = 204 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en EditCcPendiente");
                return StatusCode(500, "Error interno del servidor");
            }
        }

    }
}
