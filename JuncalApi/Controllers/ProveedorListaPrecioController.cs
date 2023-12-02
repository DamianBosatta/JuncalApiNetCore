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
    public class ProveedorListaPrecioController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ProveedorListaPrecioController> _logger; // Agregar el ILogger

        public ProveedorListaPrecioController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ProveedorListaPrecioController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger; // Inyectar el ILogger en el constructor
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorListaPrecioRespuesta>>> GetProveedorListaPrecio()
        {
            try
            {
                var ListaProveedorListaPrecio = _uow.RepositorioJuncalProveedorListaPrecio.GetAllByCondition(a => a.IsDeleted == false);

                if (ListaProveedorListaPrecio.Any())
                {
                    List<ProveedorListaPrecioRespuesta> ListaProveedorListaPrecioRespuesta = _mapper.Map<List<ProveedorListaPrecioRespuesta>>(ListaProveedorListaPrecio);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaProveedorListaPrecioRespuesta });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<ProveedorListaPrecioRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetProveedorListaPrecio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Proveedor/{id?}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorListaPrecioRespuesta>>> GetProveedorListaPrecioForId(int idProveedor)
        {
            try
            {
                var ListaProveedorListaPrecio = _uow.RepositorioJuncalProveedorListaPrecio.ObtenerListaPrecioPorId(idProveedor);

                if (ListaProveedorListaPrecio.Any())
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaProveedorListaPrecio });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<ProveedorListaPrecioRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetProveedorListaPrecioForId");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedorListaPrecio(int id)
        {
            try
            {
                var ProveedorListaPrecio = _uow.RepositorioJuncalProveedorListaPrecio.GetById(id);

                if (ProveedorListaPrecio is null)
                {
                    return Ok(new { success = false, message = "No Se Encontró La Lista de precio del Proveedor", result = new ProveedorListaPrecioRespuesta() });
                }

                var ProveedorListaPrecioRespuesta = _mapper.Map<ProveedorListaPrecioRespuesta>(ProveedorListaPrecio);

                return Ok(new { success = true, message = "Lista de precio del Proveedor Encontrada", result = ProveedorListaPrecioRespuesta });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetByIdProveedorListaPrecio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public ActionResult CargarProveedorListaPrecio([FromBody] ProveedorListaPrecioRequerido ProveedorListaPrecioReq)
        {
            try
            {
                var ProveedorListaPrecio = _mapper.Map<JuncalProveedorListaprecio>(ProveedorListaPrecioReq);

                _uow.RepositorioJuncalProveedorListaPrecio.Insert(ProveedorListaPrecio);

                var ProveedorListaPrecioRes = _mapper.Map<ProveedorListaPrecioRespuesta>(ProveedorListaPrecio);

                return Ok(new { success = true, message = " La Lista de precio del Proveedor Fue Creada Con Éxito ", result = ProveedorListaPrecioRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CargarProveedorListaPrecio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditProveedorListaPrecio(int id, ProveedorListaPrecioRequerido ProveedorListaPrecioReq)
        {
            try
            {
                var ProveedorListaPrecio = _uow.RepositorioJuncalProveedorListaPrecio.GetById(id);

                if (ProveedorListaPrecio != null)
                {
                    ProveedorListaPrecio = _mapper.Map<JuncalProveedorListaprecio>(ProveedorListaPrecioReq);
                    _uow.RepositorioJuncalProveedorListaPrecio.Update(ProveedorListaPrecio);

                    var ProveedorListaPrecioRes = _mapper.Map<ProveedorListaPrecioRespuesta>(ProveedorListaPrecio);

                    return Ok(new { success = true, message = " La Lista de precio Del Proveedor Ha Sido Actualizada ", result = ProveedorListaPrecioRes });
                }

                return Ok(new { success = false, message = "No Se Encontró La Lista de precio Del Proveedor", result = new ProveedorListaPrecioRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en EditProveedorListaPrecio");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteProveedorListaPrecio(int id)
        {
            try
            {
                var ProveedorListaPrecio = _uow.RepositorioJuncalProveedorListaPrecio.GetById(id);

                if (ProveedorListaPrecio != null)
                {
                    ProveedorListaPrecio.IsDeleted = true;
                    _uow.RepositorioJuncalProveedorListaPrecio.Update(ProveedorListaPrecio);

                    var ProveedorListaPrecioRes = _mapper.Map<ProveedorListaPrecioRespuesta>(ProveedorListaPrecio);

                    return Ok(new { success = true, message = " La Lista de precio Fue Eliminada ", result = ProveedorListaPrecioRes });
                }

                return Ok(new { success = false, message = "No Se Encontró La Lista de precio Del Proveedor", result = new ProveedorListaPrecioRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en DeleteProveedorListaPrecio");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
