using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CamionController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<CamionController> _logger;

        public CamionController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<CamionController> logger)
        {

            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }


   [HttpGet]
        public async Task<ActionResult<IEnumerable<CamionRespuesta>>> GetCamiones()
        {
            try
            {
                var listaCamiones = _uow.RepositorioJuncalCamion.GetCamiones().ToList();

                if (listaCamiones.Count() > 0)
                {
                    List<CamionRespuesta> listaCamionesRespuesta = _mapper.Map<List<CamionRespuesta>>(listaCamiones);
                  
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = listaCamionesRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<CamionRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de Camiones");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [HttpPost]
        public ActionResult CargarCamion([FromBody] CamionRequerido camionReq)
        {
            try
            {
                var camion = _uow.RepositorioJuncalCamion.GetByCondition(c => c.Patente.Equals(camionReq.Patente) && c.Isdeleted == false);

                if (camion is null)
                {
                    JuncalCamion camionNuevo = _mapper.Map<JuncalCamion>(camionReq);
                    _uow.RepositorioJuncalCamion.Insert(camionNuevo);

                    CamionRespuesta camionRes = _mapper.Map<CamionRespuesta>(camionNuevo);

                    return Ok(new { success = true, message = "El transportista fue Creado Con Éxito", result = camionRes });
                }

                CamionRespuesta camionExiste = _mapper.Map<CamionRespuesta>(camion);
                return Ok(new { success = false, message = "El Camion Ya Está Registrado", result = camionExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar cargar el Camion");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedCamion(int id)
        {
            try
            {
                var camion = _uow.RepositorioJuncalCamion.GetById(id);
                if (camion != null && camion.Isdeleted == false)
                {
                    camion.Isdeleted = true;
                    _uow.RepositorioJuncalCamion.Update(camion);
                    CamionRespuesta camionRes = _mapper.Map<CamionRespuesta>(camion);
                    return Ok(new { success = true, message = "El Camion fue Eliminado", result = camionRes });
                }

                return Ok(new { success = false, message = "El Camion no fue encontrado", result = new CamionRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar eliminar el Camion");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCamion(int id, CamionRequerido camionEdit)
        {
            try
            {
                var camion = _uow.RepositorioJuncalCamion.GetById(id);

                if (camion != null && camion.Isdeleted == false)
                {
                    _mapper.Map(camionEdit, camion);
                    _uow.RepositorioJuncalCamion.Update(camion);

                    CamionRespuesta camionRes = _mapper.Map<CamionRespuesta>(camion);

                    return Ok(new { success = true, message = "El Camion fue actualizado", result = camionRes });
                }

                return Ok(new { success = false, message = "El Camion no fue encontrado", result = new CamionRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar editar el Camion");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }
    }
}
