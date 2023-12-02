using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChoferController : Controller
    {


        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ChoferController> _logger;

        public ChoferController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ChoferController> logger)
        {

            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChoferRespuesta>>> GetChoferes()
        {
            try
            {
                var ListaChoferes = _uow.RepositorioJuncalChofer.GetAllByCondition(c => c.Isdeleted == false).ToList();

                if (ListaChoferes.Count() > 0)
                {
                    List<ChoferRespuesta> listaChoferesRespuesta = _mapper.Map<List<ChoferRespuesta>>(ListaChoferes);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = listaChoferesRespuesta });
                }

                return Ok(new { success = false, message = "La Lista Está Vacía", result = new List<ChoferRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de choferes");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [HttpPost]
        public ActionResult CargarChofer([FromBody] ChoferRequerido choferReq)
        {
            try
            {
                var chofer = _uow.RepositorioJuncalChofer.GetByCondition(c => c.Dni.Equals(choferReq.Dni) && c.Isdeleted == false);

                if (chofer is null)
                {
                    JuncalChofer choferNuevo = _mapper.Map<JuncalChofer>(choferReq);
                    _uow.RepositorioJuncalChofer.Insert(choferNuevo);
                    ChoferRespuesta choferRes = _mapper.Map<ChoferRespuesta>(choferNuevo);
                    return Ok(new { success = true, message = "El Chofer Fue Creado Con Éxito", result = choferRes });
                }

                ChoferRespuesta choferExiste = _mapper.Map<ChoferRespuesta>(chofer);
                return Ok(new { success = false, message = "El Chofer Ya Está Registrado", result = choferExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar cargar el Chofer");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedChofer(int id)
        {
            try
            {
                var chofer = _uow.RepositorioJuncalChofer.GetById(id);
                if (chofer != null && chofer.Isdeleted == false)
                {
                    chofer.Isdeleted = true;
                    _uow.RepositorioJuncalChofer.Update(chofer);
                    ChoferRespuesta choferRes = _mapper.Map<ChoferRespuesta>(chofer);
                    return Ok(new { success = true, message = "Chofer Eliminado", result = choferRes });
                }

                return Ok(new { success = false, message = "No Se Encontró El Chofer", result = new ChoferRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar eliminar el Chofer");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditChofer(int id, ChoferRequerido choferEdit)
        {
            try
            {
                var chofer = _uow.RepositorioJuncalChofer.GetById(id);

                if (chofer != null && chofer.Isdeleted == false)
                {
                    _mapper.Map(choferEdit, chofer);
                    _uow.RepositorioJuncalChofer.Update(chofer);
                    ChoferRespuesta choferRes = _mapper.Map<ChoferRespuesta>(chofer);
                    return Ok(new { success = true, message = "El Chofer Ha Sido Actualizado", result = choferRes });
                }

                return Ok(new { success = false, message = "No Se Encontró El Chofer", result = new ChoferRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar editar el Chofer");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

    }
}
