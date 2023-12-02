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
    public class AceriaMaterialController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AceriaMaterialController> _logger;

        public AceriaMaterialController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<AceriaMaterialController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AceriaMaterialRespuesta>>> GetAceriasMaterial()
        {
            try
            {
                var ListaAceriasMaterial = _uow.RepositorioJuncalAceriaMaterial.GetAllByCondition(c => c.Isdeleted == false).ToList();

                if (ListaAceriasMaterial.Count() > 0)
                {
                    List<AceriaMaterialRespuesta> listaAceriasMatRespuesta = _mapper.Map<List<AceriaMaterialRespuesta>>(ListaAceriasMaterial);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = listaAceriasMatRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<AceriaMaterialRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de Acerias Material");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }



        [HttpGet("{idAceria}")]
        public async Task<ActionResult<IEnumerable<AceriaMaterialRespuesta>>> GetAceriaMaterialById(int idAceria)
        {
            try
            {
                var listaAceriaMaterial = _uow.RepositorioJuncalAceriaMaterial.GetAceriaMaterialesForIdAceria(idAceria);

                if (listaAceriaMaterial.Count() > 0)
                {
                    var listaAceriaMatRespuesta = _mapper.Map<List<AceriaMaterialRespuesta>>(listaAceriaMaterial);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = listaAceriaMatRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<AceriaMaterialRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de AceriaMaterial por ID");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }


        [HttpPost]
        public ActionResult CargarAceriaMaterial([FromBody] AceriaMaterialRequerido aceriaMatRequerido)
        {
            try
            {
                var aceriaMat = _uow.RepositorioJuncalAceriaMaterial.GetByCondition(m => m.Cod.Equals(aceriaMatRequerido.Cod) && m.Isdeleted == false);

                if (aceriaMat is null)
                {
                    JuncalAceriaMaterial aceriaMatNuevo = _mapper.Map<JuncalAceriaMaterial>(aceriaMatRequerido);

                    _uow.RepositorioJuncalAceriaMaterial.Insert(aceriaMatNuevo);

                    AceriaMaterialRespuesta aceriaMatRes = _mapper.Map<AceriaMaterialRespuesta>(aceriaMatNuevo);
                    return Ok(new { success = true, message = "La Aceria Material fue Creada Con Éxito", result = aceriaMatRes });
                }

                AceriaMaterialRespuesta respuestaExiste = _mapper.Map<AceriaMaterialRespuesta>(aceriaMat);
                return Ok(new { success = false, message = "La Aceria Ya Está Cargada", result = respuestaExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar cargar la Aceria Material");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedAceriaMaterial(int id)
        {
            try
            {
                var aceriaMat = _uow.RepositorioJuncalAceriaMaterial.GetById(id);

                if (aceriaMat != null && aceriaMat.Isdeleted == false)
                {
                    aceriaMat.Isdeleted = true;
                    _uow.RepositorioJuncalAceriaMaterial.Update(aceriaMat);

                    AceriaMaterialRespuesta aceriaMatRes = _mapper.Map<AceriaMaterialRespuesta>(aceriaMat);

                    return Ok(new { success = true, message = "La Aceria Material Fue Eliminada", result = aceriaMatRes });
                }

                return Ok(new { success = false, message = "La Aceria Material no fue encontrada", result = new AceriaMaterialRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar eliminar la Aceria Material");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAceriaMaterial(int id, AceriaMaterialRequerido aceriaMatEdit)
        {
            try
            {
                var aceriaMat = _uow.RepositorioJuncalAceriaMaterial.GetById(id);

                if (aceriaMat != null && aceriaMat.Isdeleted == false)
                {
                    aceriaMat = _mapper.Map(aceriaMatEdit, aceriaMat);
                    _uow.RepositorioJuncalAceriaMaterial.Update(aceriaMat);

                    AceriaMaterialRespuesta aceriaMatRes = _mapper.Map<AceriaMaterialRespuesta>(aceriaMat);

                    return Ok(new { success = true, message = "La Aceria Material fue actualizada", result = aceriaMatRes });
                }

                return Ok(new { success = false, message = "La Aceria Material no fue encontrada", result = new AceriaMaterialRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar editar la Aceria Material");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }
    }
}