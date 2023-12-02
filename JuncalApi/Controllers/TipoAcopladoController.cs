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
    public class TipoAcopladoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<TipoAcopladoController> _logger;

        public TipoAcopladoController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<TipoAcopladoController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAcopladoRespuesta>>> GetTipoAcoplados()
        {
            try
            {
                var ListaTipoAcoplados = _uow.RepositorioJuncalTipoAcoplado.GetAll().ToList();

                if (ListaTipoAcoplados.Count() > 0)
                {
                    List<TipoAcopladoRespuesta> listaTipoAcopladoRespuesta = _mapper.Map<List<TipoAcopladoRespuesta>>(ListaTipoAcoplados);
                    return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaTipoAcopladoRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<TipoAcopladoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los Tipo Acoplados.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdTipoAcoplado(int id)
        {
            try
            {
                var tipoAcoplado = _uow.RepositorioJuncalTipoAcoplado.GetById(id);

                if (tipoAcoplado is null)
                {
                    return Ok(new { success = false, message = "No Se Encontro El Tipo Acoplado", result = new TipoAcopladoRespuesta() });
                }

                TipoAcopladoRespuesta TipoAcopladoRes = new();
                _mapper.Map(tipoAcoplado, TipoAcopladoRes);

                return Ok(new { success = true, message = "Tipo Acoplado Encontrado", result = TipoAcopladoRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Tipo Acoplado por ID.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPost]
        public ActionResult CargarTipoAcoplado([FromBody] TipoAcopladoRequerido tipoAcopladoReq)
        {
            try
            {
                var tipoAcoplado = _uow.RepositorioJuncalTipoAcoplado.GetByCondition(c => c.Nombre.Equals(tipoAcopladoReq.Nombre));

                if (tipoAcoplado is null)
                {
                    JuncalTipoAcoplado tipoAcopladoNuevo = _mapper.Map<JuncalTipoAcoplado>(tipoAcopladoReq);
                    _uow.RepositorioJuncalTipoAcoplado.Insert(tipoAcopladoNuevo);
                    TipoAcopladoRespuesta tipoAcopladoRes = new();
                    _mapper.Map(tipoAcopladoNuevo, tipoAcopladoRes);
                    return Ok(new { success = true, message = "El Tipo De Acoplado Fue Creado Con Exito", result = tipoAcopladoRes });
                }

                TipoAcopladoRespuesta tipoAcopladoExiste = new();
                _mapper.Map(tipoAcoplado, tipoAcopladoExiste);
                return Ok(new { success = false, message = " El Tipo Acoplado Ya Esta Cargado ", result = tipoAcopladoExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar el Tipo Acoplado.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTipoAcoplado(int id, TipoAcopladoRequerido tipoAcopladoEditado)
        {
            try
            {
                var tipoAcoplado = _uow.RepositorioJuncalTipoAcoplado.GetById(id);

                if (tipoAcoplado != null)
                {
                    _mapper.Map(tipoAcopladoEditado, tipoAcoplado);
                    _uow.RepositorioJuncalTipoAcoplado.Update(tipoAcoplado);
                    TipoAcopladoRespuesta tipoAcopladoRes = new();
                    _mapper.Map(tipoAcoplado, tipoAcopladoRes);
                    return Ok(new { success = true, message = "El Tipo De Acoplado fue Actualizado", result = tipoAcopladoRes });
                }

                return Ok(new { success = false, message = "El Tipo De Acoplado No Fue Encontrado ", result = new TipoAcopladoRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el Tipo Acoplado por ID.");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
