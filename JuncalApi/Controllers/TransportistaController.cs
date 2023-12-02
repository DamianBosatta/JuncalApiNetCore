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
    public class TransportistaController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<TransportistaController> _logger;

        public TransportistaController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<TransportistaController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportistaRespuesta>>> GetTransportistas()
        {
            try
            {
                var ListaTransportistas = _uow.RepositorioJuncalTransportistum.GetAllByCondition(t => t.Isdeleted == false).ToList();

                if (ListaTransportistas.Count() > 0)
                {
                    List<TransportistaRespuesta> listaTransportistasRespuesta = _mapper.Map<List<TransportistaRespuesta>>(ListaTransportistas);
                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaTransportistasRespuesta });
                }

                return Ok(new { success = false, message = "La Lista Esta Vacia", result = new List<TransportistaRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de Transportistas");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public ActionResult CargarTransportista([FromBody] TransportistaRequerido transportistaReq)
        {
            try
            {
                var transportista = _uow.RepositorioJuncalTransportistum.GetByCondition(t => t.Cuit.Equals(transportistaReq.Cuit) && t.Isdeleted == false);

                if (transportista is null)
                {
                    JuncalTransportistum TransportistaNuevo = _mapper.Map<JuncalTransportistum>(transportistaReq);
                    _uow.RepositorioJuncalTransportistum.Insert(TransportistaNuevo);
                    TransportistaRespuesta transportistaRes = _mapper.Map<TransportistaRespuesta>(TransportistaNuevo);
                    return Ok(new { success = true, message = "El Transportista Fue Creado Con Exito", result = transportistaRes });
                }

                TransportistaRespuesta transportistaExiste = _mapper.Map<TransportistaRespuesta>(transportista);
                return Ok(new { success = false, message = "El Transportista Ya Existe", result = transportistaExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar el Transportista");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedTransportista(int id)
        {
            try
            {
                var transportista = _uow.RepositorioJuncalTransportistum.GetById(id);
                if (transportista != null && transportista.Isdeleted == false)
                {
                    transportista.Isdeleted = true;
                    _uow.RepositorioJuncalTransportistum.Update(transportista);
                    TransportistaRespuesta transportistaRes = _mapper.Map<TransportistaRespuesta>(transportista);
                    return Ok(new { success = true, message = "El Transportista Fue Eliminado Con Exito", result = transportistaRes });
                }

                return Ok(new { success = false, message = "El Transportista No Se Encontro", result = new TransportistaRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el Transportista con ID: {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransportista(int id, TransportistaRequerido transportistaEdit)
        {
            try
            {
                var transportista = _uow.RepositorioJuncalTransportistum.GetById(id);

                if (transportista != null && transportista.Isdeleted == false)
                {
                    transportista = _mapper.Map(transportistaEdit, transportista);
                    _uow.RepositorioJuncalTransportistum.Update(transportista);
                    TransportistaRespuesta transportistaRes = _mapper.Map<TransportistaRespuesta>(transportista);
                    return Ok(new { success = true, message = "El Transportista Fue Actualizado Con Exito", result = transportistaRes });
                }

                return Ok(new { success = false, message = "El Transportista No Se Encontro", result = new TransportistaRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el Transportista con ID: {Id}", id);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
