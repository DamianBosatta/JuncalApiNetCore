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
    public class ContratoController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ContratoController> _logger;

        public ContratoController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ContratoController> logger)
        {

            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoRespuesta>>> GetAContratos()
        {
            try
            {
                var ListaContratos = _uow.RepositorioJuncalContrato.GetContratos();

                if (ListaContratos.Count() > 0)
                {
                    List<ContratoRespuesta> listaContratoRespuesta = _mapper.Map<List<ContratoRespuesta>>(ListaContratos);
                    return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaContratoRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ContratoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de contratos");
                return StatusCode(500, "Ocurrió un error al obtener la lista de contratos");
            }
        }


        [HttpPost]
        public ActionResult CargarContrato(ContratoRequerido contratoRequerido)
        {
            try
            {
                var contrato = _uow.RepositorioJuncalContrato.GetByCondition(c =>
                    c.IdAceria == contratoRequerido.IdAceria
                    && c.Numero == contratoRequerido.Numero
                    && c.Isdeleted == false
                );

                if (contrato != null)
                {
                    if (contratoRequerido.FechaVigencia <= contrato.FechaVigencia)
                    {
                        return Ok(new
                        {
                            success = false,
                            message = "El contrato debe tener una fecha DESDE (" + contratoRequerido.FechaVigencia + "), posterior al del contrato existente con el numero: " + contrato.Numero + " y Fecha: " + contrato.FechaVigencia
                        });
                    }
                    else
                    {
                        JuncalContrato contratoNuevo = _mapper.Map<JuncalContrato>(contratoRequerido);
                        _uow.RepositorioJuncalContrato.Insert(contratoNuevo);
                        ContratoRespuesta contratoRes = new();
                        _mapper.Map(contratoNuevo, contratoRes);
                        return Ok(new { success = true, message = "El Contrato Fue Creado Con Exito", result = contratoRes });
                    }
                }
                else
                {
                    JuncalContrato contratoNuevo = _mapper.Map<JuncalContrato>(contratoRequerido);
                    _uow.RepositorioJuncalContrato.Insert(contratoNuevo);
                    ContratoRespuesta contratoRes = new();
                    _mapper.Map(contratoNuevo, contratoRes);
                    return Ok(new { success = true, message = "El Contrato Fue Creado Con Exito", result = contratoRes });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar un contrato");
                return StatusCode(500, "Ocurrió un error al cargar el contrato");
            }
        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedContrato(int id)
        {
            try
            {
                var contrato = _uow.RepositorioJuncalContrato.GetById(id);
                if (contrato != null && contrato.Isdeleted == false)
                {
                    contrato.Isdeleted = true;
                    _uow.RepositorioJuncalContrato.Update(contrato);
                    ContratoRespuesta contratoRes = new();
                    _mapper.Map(contrato, contratoRes);

                    return Ok(new { success = true, message = "El Contrato Fue Eliminado", result = contratoRes });
                }

                return Ok(new { success = false, message = "El Contrato No Se Encontró", result = new ContratoRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar un contrato");
                return StatusCode(500, "Ocurrió un error al eliminar el contrato");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditContrato(int id, ContratoRequerido contratoEdit)
        {
            try
            {
                var contrato = _uow.RepositorioJuncalContrato.GetById(id);

                if (contrato != null && contrato.Isdeleted == false)
                {
                    _mapper.Map(contratoEdit, contrato);
                    _uow.RepositorioJuncalContrato.Update(contrato);
                    ContratoRespuesta contratoRes = new();
                    _mapper.Map(contrato, contratoRes);

                    return Ok(new { success = true, message = "El Contrato fue Actualizado", result = contratoRes });
                }

                return Ok(new { success = false, message = "El Contrato No Fue Encontrado", result = new ContratoRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el contrato con ID: {ContractId}", id);
                return StatusCode(500, "Ocurrió un error al editar el contrato");
            }
        }

        /// enPoint Felix
        [HttpGet("Comprobar/{idAceria}/{numero}")]
        public IActionResult CheckContrato(string numero, int idAceria)
        {
            try
            {
                var ListaContratos = _uow.RepositorioJuncalContrato.GetAllByCondition(contrato => contrato.Numero == numero && contrato.IdAceria == idAceria && contrato.Isdeleted == false);

                if (ListaContratos.Count() > 0)
                {
                    return Ok(new { success = true, message = "Ya existe un contrato con el número: " + numero });
                }

                return Ok(new { success = false, message = "No existe contrato con el número: " + numero });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar el contrato con número: {ContractNumber} para la acería con ID: {AceriaId}", numero, idAceria);
                return StatusCode(500, "Ocurrió un error al verificar el contrato");
            }
        }



    }
}
