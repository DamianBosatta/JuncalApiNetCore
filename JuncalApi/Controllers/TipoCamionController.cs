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
    public class TipoCamionController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<TipoCamionController> _logger;

        public TipoCamionController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<TipoCamionController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCamionRespuesta>>> GetTipoCamiones()
        {
            try
            {
                var listaTipoCamiones = _uow.RepositorioJuncalTipoCamion.GetAll().ToList();

                if (listaTipoCamiones.Count > 0)
                {
                    List<TipoCamionRespuesta> listaTipoCamionesRespuesta = _mapper.Map<List<TipoCamionRespuesta>>(listaTipoCamiones);
                    return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaTipoCamionesRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<TipoCamionRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de Tipo de Camiones");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdTipoCamion(int id)
        {
            try
            {
                var tipoCamion = _uow.RepositorioJuncalTipoCamion.GetById(id);

                if (tipoCamion is null)
                {
                    return Ok(new { success = false, message = "No Se Encontro El Tipo Camion", result = new TipoCamionRespuesta() });
                }

                TipoCamionRespuesta TipoCamionRes = new TipoCamionRespuesta();
                _mapper.Map(tipoCamion, TipoCamionRes);

                return Ok(new { success = true, message = "Tipo Camion Encontrado", result = TipoCamionRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el Tipo de Camion con ID: {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public ActionResult CargarTipoCamion([FromBody] TipoCamionRequerido tipoCamionReq)
        {
            try
            {
                var tipoCamion = _uow.RepositorioJuncalTipoCamion.GetByCondition(c => c.Nombre.Equals(tipoCamionReq.Nombre));

                if (tipoCamion is null)
                {
                    JuncalTipoCamion tipoCamionNuevo = _mapper.Map<JuncalTipoCamion>(tipoCamionReq);
                    _uow.RepositorioJuncalTipoCamion.Insert(tipoCamionNuevo);
                    TipoCamionRespuesta TipoCamionRes = new TipoCamionRespuesta();

                    _mapper.Map(tipoCamionNuevo, TipoCamionRes);
                    return Ok(new { success = true, message = "El Tipo De Camion Fue Creado Con Exito", result = TipoCamionRes });
                }

                TipoCamionRespuesta TipoCamionExiste = new TipoCamionRespuesta();
                _mapper.Map(tipoCamion, TipoCamionExiste);

                return Ok(new { success = false, message = " El Tipo Camion Ya Esta Cargado ", result = TipoCamionExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar el Tipo de Camion");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTipoCamion(int id, TipoCamionRequerido tipoCamionEdit)
        {
            try
            {
                var tipoCamion = _uow.RepositorioJuncalTipoCamion.GetById(id);

                if (tipoCamion != null)
                {
                    tipoCamion = _mapper.Map(tipoCamionEdit, tipoCamion);
                    _uow.RepositorioJuncalTipoCamion.Update(tipoCamion);
                    TipoCamionRespuesta TipoCamionRes = new TipoCamionRespuesta();

                    _mapper.Map(tipoCamion, TipoCamionRes);
                    return Ok(new { success = true, message = "El Tipo De Camion fue Actualizado", result = TipoCamionRes });
                }

                return Ok(new { success = false, message = "El Tipo De Camion No Fue Encontrado ", result = new TipoCamionRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el Tipo de Camion con ID: {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
