using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JuncalApi.Controllers
{
    //    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorCuentaCorrienteController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ProveedorCuentaCorrienteController> _logger; // Agregar el ILogger

        public ProveedorCuentaCorrienteController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ProveedorCuentaCorrienteController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger; // Inyectar el ILogger en el constructor
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorCuentaCorrienteRespuesta>>> GetProveedorCcMovimiento()
        {
            try
            {
                var ListaProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetProveedorCuentasCorrientes(0);

                if (ListaProveedorCcMovimiento.Any())
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaProveedorCcMovimiento });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<ProveedorCuentaCorrienteRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetProveedorCcMovimiento");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Proveedor/{idProveedor?}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorCuentaCorrienteRespuesta>>> GetProveedorCcMovimientoForIdProveedor(int idProveedor)
        {
            try
            {
                var ListaProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetProveedorCuentasCorrientes(idProveedor);



                if (ListaProveedorCcMovimiento.Any())
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaProveedorCcMovimiento });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<ProveedorCuentaCorrienteRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetProveedorCcMovimientoForIdProveedor");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedorCcMovimiento(int id)
        {
            try
            {
                var ProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetById(id);

                if (ProveedorCcMovimiento is null)
                {
                    return Ok(new { success = false, message = "No Se Encontró La C.C Del Proveedor", result = new ProveedorCuentaCorrienteRespuesta() });
                }

                var ProveedorCcMovimientoRespuesta = _mapper.Map<ProveedorCuentaCorrienteRespuesta>(ProveedorCcMovimiento);

                return Ok(new { success = true, message = "C.C Proveedor Encontrada", result = ProveedorCcMovimientoRespuesta });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetByIdProveedorCcMovimiento");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public ActionResult CargarProveedorCcMovimiento([FromBody] ProveedorCuentaCorrienteRequerido ProveedorCcMovimientoReq)
        {
            try
            {
                var ProveedorCcMovimiento = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCcMovimientoReq);

                _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(ProveedorCcMovimiento);

                var ProveedorCcMovimientoRes = _mapper.Map<ProveedorCuentaCorrienteRespuesta>(ProveedorCcMovimiento);

                return Ok(new { success = true, message = "La CC Del Proveedor Fue Creada Con Éxito", result = ProveedorCcMovimientoRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CargarProveedorCcMovimiento");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedorCcMovimiento(int id, ProveedorCuentaCorrienteRequerido ProveedorCcMovimientoReq)
        {
            try
            {
                var ProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetById(id);

                if (ProveedorCcMovimiento != null)
                {
                    ProveedorCcMovimiento = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCcMovimientoReq);
                    _uow.RepositorioJuncalProveedorCuentaCorriente.Update(ProveedorCcMovimiento);

                    var ProveedorCcMovimientoRes = _mapper.Map<ProveedorCuentaCorrienteRespuesta>(ProveedorCcMovimiento);

                    return Ok(new { success = true, message = "La CC Del Proveedor Ha Sido Actualizado", result = ProveedorCcMovimientoRes });
                }

                return Ok(new { success = false, message = "No Se Encontró La CC Del Proveedor", result = new ProveedorCuentaCorrienteRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en EditProveedorCcMovimiento");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
