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
    public class AcopladoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AcopladoController> _logger;

        public AcopladoController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<AcopladoController> logger)
        {

            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcopladoRespuesta>>> GetAllAcoplados()
        {
            try
            {
                var listaAcoplados = _uow.RepositorioJuncalAcoplado.GetAcoplados();

                if (listaAcoplados.Count() > 0)
                {
                    List<AcopladoRespuesta> listaAcopladosRespuesta = _mapper.Map<List<AcopladoRespuesta>>(listaAcoplados);
                    return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaAcopladosRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<AcopladoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de Acoplados");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }



        [HttpPost]
        public ActionResult CargarAcoplado([FromBody] AcopladoRequerido acopladoRequerido)
        {
            try
            {
                var acoplado = _uow.RepositorioJuncalAcoplado.GetByCondition(c => c.Patente == acopladoRequerido.Patente);

                if (acoplado is null)
                {
                    var acopladoNuevo = _mapper.Map<JuncalAcoplado>(acopladoRequerido);
                    _uow.RepositorioJuncalAcoplado.Insert(acopladoNuevo);
                    AcopladoRespuesta acopladoRes = _mapper.Map<AcopladoRespuesta>(acopladoNuevo);
                    return Ok(new { success = true, message = "El Acoplado Fue Creado Con Éxito", result = acopladoRes });
                }

                AcopladoRespuesta acopladoExiste = _mapper.Map<AcopladoRespuesta>(acoplado);
                return Ok(new { success = false, message = "Ya Tenemos Un Acoplado Con Esa Patente", result = acopladoExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar cargar el Acoplado");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }



        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedAcoplado(int id)
        {
            try
            {
                var acoplado = _uow.RepositorioJuncalAcoplado.GetById(id);

                if (acoplado != null)
                {
                    acoplado.Isdeleted = true;
                    _uow.RepositorioJuncalAcoplado.Update(acoplado);

                    AcopladoRespuesta acopladoRes = _mapper.Map<AcopladoRespuesta>(acoplado);

                    return Ok(new { success = true, message = "El Acoplado Fue Eliminado", result = acopladoRes });
                }

                return Ok(new { success = false, message = "No Se Encontró El Acoplado", result = new AcopladoRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar eliminar el Acoplado");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAcoplado(int id, AcopladoRequerido acopladoEdit)
        {
            try
            {
                var acoplado = _uow.RepositorioJuncalAcoplado.GetById(id);

                if (acoplado != null)
                {
                    _mapper.Map(acopladoEdit, acoplado);
                    _uow.RepositorioJuncalAcoplado.Update(acoplado);

                    AcopladoRespuesta acopladoRes = _mapper.Map<AcopladoRespuesta>(acoplado);

                    return Ok(new { success = true, message = "El Acoplado fue Actualizado", result = acopladoRes });
                }

                return Ok(new { success = false, message = "El Acoplado No Fue Encontrado", result = new AcopladoRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar editar el Acoplado");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

    }
}
