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
    public class RolesController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<RolesController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesRespuesta>>> GetRoles()
        {
            try
            {
                var ListaRoles = _uow.RepositorioJuncalRole.GetAll().ToList();

                if (ListaRoles.Count() > 0)
                {
                    List<RolesRespuesta> listaRolesRespuesta = _mapper.Map<List<RolesRespuesta>>(ListaRoles);
                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaRolesRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RolesRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetRoles");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public ActionResult CargarRoles([FromBody] RolesRequerido RolesReq)
        {
            try
            {
                var roles = _uow.RepositorioJuncalRole.GetByCondition(c => c.Nombre.Equals(RolesReq.Nombre));

                if (roles is null)
                {
                    JuncalRole rolNuevo = _mapper.Map<JuncalRole>(RolesReq);

                    _uow.RepositorioJuncalRole.Insert(rolNuevo);
                    RolesRespuesta rolRes = _mapper.Map<RolesRespuesta>(rolNuevo);
                    return Ok(new { success = true, message = "El Rol fue Creado Con Exito", result = rolRes });
                }

                RolesRespuesta rolExiste = _mapper.Map<RolesRespuesta>(roles);
                return Ok(new { success = false, message = " El Rol Ya Existe ", result = rolExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CargarRoles");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditRoles(int id, RolesRequerido rolEdit)
        {
            try
            {
                var rol = _uow.RepositorioJuncalRole.GetById(id);

                if (rol != null)
                {
                    _mapper.Map(rolEdit, rol);
                    _uow.RepositorioJuncalRole.Update(rol);
                    RolesRespuesta rolRes = _mapper.Map<RolesRespuesta>(rol);
                    return Ok(new { success = true, message = "El Rol fue actualizado", result = rolRes });
                }

                return Ok(new { success = false, message = "El Rol no fue encontrado ", result = new RolesRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en EditRoles");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
