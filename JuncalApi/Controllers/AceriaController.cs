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
    public class AceriaController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AceriaController> _logger;

        public AceriaController(IUnidadDeTrabajo uow, IMapper mapper, ILogger <AceriaController> logger)
        {

            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AceriaRespuesta>>> GetAcerias()
        {
            try
            {
                var ListaAcerias = _uow.RepositorioJuncalAcerium.GetAllByCondition(c => c.Isdeleted == false).ToList();
           
                if (ListaAcerias.Count > 0)
                {
                    List<AceriaRespuesta> listaAceriasRespuesta = _mapper.Map<List<AceriaRespuesta>>(ListaAcerias);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = listaAceriasRespuesta });
                }
                else
                {
                    return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<AceriaRespuesta>() });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener la lista de acerias");
                return StatusCode(500, new { success = false, message = "Error al procesar la solicitud" });
            }
        }
        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdAceria(int id)
        {
            try
            {
                var aceria = _uow.RepositorioJuncalAcerium.GetById(id);

                if (aceria is null || aceria.Isdeleted==true)
                {
                   
                    return Ok(new { success = false, message = "No se encontró la Aceria", result = aceria });
                }

                AceriaRespuesta aceriaRes = _mapper.Map<AceriaRespuesta>(aceria);

                return Ok(new { success = true, message = "Aceria encontrada", result = aceriaRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar obtener la Aceria con ID: {AceriaId}", id);
                return StatusCode(500, new { success = false, message = "Error interno al procesar la solicitud" });
            }
        }


        [HttpPost]
        public ActionResult CargarAceria([FromBody] AceriaRequerido aceriaReq)
        {
            try
            {
                var aceria = _uow.RepositorioJuncalAcerium.GetByCondition(c => c.Cuit.Equals(aceriaReq.Cuit) && c.Isdeleted == false);

                if (aceria is null)
                {
                    JuncalAcerium aceriaNuevo = _mapper.Map<JuncalAcerium>(aceriaReq);

                    _uow.RepositorioJuncalAcerium.Insert(aceriaNuevo);
                    return Ok(new { success = true, message = "La Aceria fue Creada Con Éxito", result = aceriaNuevo });
                }

                AceriaRespuesta aceriaRes = _mapper.Map<AceriaRespuesta>(aceria);
                return Ok(new { success = false, message = "La Aceria Ya Existe", result = aceriaRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar cargar la Aceria");
                return StatusCode(500, new { success = false, message = "Error interno al procesar la solicitud" });
            }
        }



        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedAceria(int id)
        {
            try
            {
                var aceria = _uow.RepositorioJuncalAcerium.GetById(id);

                if (aceria != null && aceria.Isdeleted == false)
                {
                    aceria.Isdeleted = true;
                    _uow.RepositorioJuncalAcerium.Update(aceria);

                    AceriaRespuesta aceriaRes = _mapper.Map<AceriaRespuesta>(aceria);

                    return Ok(new { success = true, message = "La Aceria Fue Eliminada", result = aceriaRes });
                }

                return Ok(new { success = false, message = "La Aceria no fue encontrada", result = aceria });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar marcar como eliminada la Aceria con ID: {AceriaId}", id);
                return StatusCode(500, new { success = false, message = "Error interno al procesar la solicitud" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAceria(int id, AceriaRequerido aceriaEdit)
        {
            try
            {
                var aceria = _uow.RepositorioJuncalAcerium.GetById(id);

                if (aceria != null && aceria.Isdeleted == false)
                {
                    _mapper.Map(aceriaEdit, aceria);
                    _uow.RepositorioJuncalAcerium.Update(aceria);

                    AceriaRespuesta aceriaRes = _mapper.Map<AceriaRespuesta>(aceria);

                    return Ok(new { success = true, message = "La Aceria fue actualizada", result = aceriaRes });
                }

                return Ok(new { success = false, message = "La Aceria no fue encontrada", result = aceria});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar editar la Aceria con ID: {AceriaId}", id);
                return StatusCode(500, new { success = false, message = "Error interno al procesar la solicitud" });
            }
        }

    }
}
