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
    public class ProveedorListaPreciosMaterialesController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ProveedorListaPreciosMaterialesController> _logger;

        public ProveedorListaPreciosMaterialesController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ProveedorListaPreciosMaterialesController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorListaPrecioMaterialRespuesta>>> GetProveedorListaPrecioMaterial()
        {
            try
            {
                var ListaProveedorListaPrecioMaterial = _uow.RepositorioJuncalProveedorListaPreciosMateriales.GetListaPreciosMateriales();

                if (ListaProveedorListaPrecioMaterial.Any())
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaProveedorListaPrecioMaterial });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<ProveedorListaPrecioMaterialRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetProveedorListaPrecioMaterial");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("ProveedorListaPrecio/{id?}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorListaPrecioMaterialRespuesta>>> GetProveedorListaPrecioMaterial(int idProveedorListaPrecio)
        {
            try
            {
                var ListaProveedorListaPrecioMaterial = _uow.RepositorioJuncalProveedorListaPreciosMateriales.GetListaPreciosMateriales()
                    .Where(a => a.IdProveedorListaprecios == idProveedorListaPrecio);

                if (ListaProveedorListaPrecioMaterial.Any())
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaProveedorListaPrecioMaterial });
                }

                return Ok(new { success = false, message = "La Lista Está Vacia", result = new List<ProveedorListaPrecioMaterialRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetProveedorListaPrecioMaterial");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedorListaPrecioMaterial(int id)
        {
            try
            {
                var ProveedorListaPrecioMaterial = _uow.RepositorioJuncalProveedorListaPreciosMateriales.GetById(id);

                if (ProveedorListaPrecioMaterial is null)
                {
                    return Ok(new { success = false, message = "No Se Encontró La Lista de precio de materiales del Proveedor", result = new ProveedorListaPrecioMaterialRespuesta() });
                }

                var ProveedorListaPrecioMaterialRes = _mapper.Map<ProveedorListaPrecioMaterialRespuesta>(ProveedorListaPrecioMaterial);

                return Ok(new { success = true, message = "Lista de precio de materiales del Proveedor Encontrada", result = ProveedorListaPrecioMaterialRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetByIdProveedorListaPrecioMaterial");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public ActionResult CargarProveedorListaPrecioMaterial([FromBody] List<ProveedorListaPrecioMaterialRequerido> ProveedorListaPrecioMaterialReq)
        {
            try
            {
                if (!ProveedorListaPrecioMaterialReq.Any())
                {
                    return Ok(new { success = false, message = "Lista de precio de materiales llegó vacía", result = new List<ProveedorListaPrecioMaterialRespuesta>() });
                }

                var ProveedorListaPrecioMaterial = _mapper.Map<List<JuncalProveedorListapreciosMateriale>>(ProveedorListaPrecioMaterialReq);

                _uow.RepositorioJuncalProveedorListaPreciosMateriales.InsertRange(ProveedorListaPrecioMaterial);

                var ProveedorListaPrecioMaterialRes = _mapper.Map<List<ProveedorListaPrecioMaterialRespuesta>>(ProveedorListaPrecioMaterial);

                return Ok(new { success = true, message = "La lista de precio de Materiales de Proveedor Fue Creada Con Exito", result = ProveedorListaPrecioMaterialRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CargarProveedorListaPrecioMaterial");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedorListaPrecioMaterial(int id, ProveedorListaPrecioMaterialRequerido ProveedorListaPrecioMaterialReq)
        {
            try
            {
                var ProveedorListaPrecioMaterial = _uow.RepositorioJuncalProveedorListaPreciosMateriales.GetById(id);

                if (ProveedorListaPrecioMaterial != null)
                {
                    ProveedorListaPrecioMaterial = _mapper.Map<JuncalProveedorListapreciosMateriale>(ProveedorListaPrecioMaterialReq);
                    _uow.RepositorioJuncalProveedorListaPreciosMateriales.Update(ProveedorListaPrecioMaterial);

                    var ProveedorListaPrecioMaterialRes = _mapper.Map<ProveedorListaPrecioMaterialRespuesta>(ProveedorListaPrecioMaterial);

                    return Ok(new { success = true, message = "La Lista de precio de material Del Proveedor Ha Sido Actualizada", result = ProveedorListaPrecioMaterialRes });
                }

                return Ok(new { success = false, message = "No Se Encontró la lista de precio de material Del Proveedor", result = new ProveedorListaPrecioMaterialRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en EditProveedorListaPrecioMaterial");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}