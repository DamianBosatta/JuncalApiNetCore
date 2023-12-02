using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoItemController : Controller
    {


        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ContratoItemController> _logger;

        public ContratoItemController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ContratoItemController> logger)
        {

            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }




        [HttpGet("{idContrato}")]
        public async Task<ActionResult<IEnumerable<ContratoItemRespuesta>>> GetAContratosItemForContrato(int idContrato)
        {
            try
            {
                var ListaContratosItem = _uow.RepositorioJuncalContratoItem.GetContratoItemForIdContrato(idContrato);

                if (ListaContratosItem.Count() > 0)
                {
                    List<ContratoItemRespuesta> listaContratoItemRespuesta = _mapper.Map<List<ContratoItemRespuesta>>(ListaContratosItem);
                    return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaContratoItemRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ContratoItemRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los elementos del contrato con ID: {ContractId}", idContrato);
                return StatusCode(500, "Ocurrió un error al obtener los elementos del contrato");
            }
        }

        [HttpGet("precio/{idContrato}/{idMaterial}")]
        public async Task<ActionResult<decimal>> GetPrecioMaterial(int idContrato, int idMaterial)
        {
            try
            {
                var precioMaterial = _uow.RepositorioJuncalContratoItem.GetPrecioMaterial(idContrato, idMaterial);

                if (precioMaterial > 0)
                {
                    return Ok(new { success = true, message = "El Precio Ha Sido Enviado", result = precioMaterial });
                }

                return Ok(new { success = false, message = "El Precio Dio 0 o No tiene precio", result = precioMaterial });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el precio del material para el contrato ID: {ContractId} y Material ID: {MaterialId}", idContrato, idMaterial);
                return StatusCode(500, "Ocurrió un error al obtener el precio del material");
            }
        }


        [HttpPost]
        public ActionResult CargarContratoItem([FromBody] List<ContratoItemRequerido> listaContratoItemRequerido)
        {
            List<ContratoItemRespuesta> contratoItemRespuesta = new List<ContratoItemRespuesta>();

            try
            {
                if (listaContratoItemRequerido.Count() > 0)
                {
                    foreach (var item in listaContratoItemRequerido)
                    {
                        JuncalContratoItem contratoItemNuevo = new JuncalContratoItem();
                        contratoItemNuevo = _mapper.Map<JuncalContratoItem>(item);
                        _uow.RepositorioJuncalContratoItem.Insert(contratoItemNuevo);
                        ContratoItemRespuesta contratoItemRes = new();
                        _mapper.Map(contratoItemNuevo, contratoItemRes);
                        contratoItemRespuesta.Add(contratoItemRes);
                    }

                    return Ok(new { success = true, message = "La Lista Contrato Item Fue Cargada Con Exito ", result = contratoItemRespuesta });
                }

                return Ok(new { success = false, message = " Carga De Data Invalida", result = new ContratoItemRespuesta() == null });
            }
            catch (Exception ex)
            {
               _logger.LogError(ex, "Error al cargar elementos del contrato"); // Registrando el error con Serilog
                return StatusCode(500, "Ocurrió un error al cargar los elementos del contrato");
            }
        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedContratoItem(int id)
        {

            try
            {
                var contratoItem = _uow.RepositorioJuncalContratoItem.GetById(id);
                if (contratoItem != null && contratoItem.Isdeleted == false)
                {
                    contratoItem.Isdeleted = true;
                    _uow.RepositorioJuncalContratoItem.Update(contratoItem);
                    ContratoItemRespuesta contratoItemRes = new();
                    _mapper.Map(contratoItem, contratoItemRes);

                    return Ok(new { success = true, message = "El Contrato Item Fue Eliminado ", result = contratoItemRes });
                }

                return Ok(new { success = false, message = "La Contrato Item  No Se Encontro ", result = new ContratoItemRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar eliminar el Contrato Item");
                return StatusCode(500, "Ocurrió un error interno en el servidor al intentar eliminar el Contrato Item");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditContratoItem(int id, ContratoItemRequerido contratoItemEdit)
        {
           

            try
            {
                var contratoItem = _uow.RepositorioJuncalContratoItem.GetById(id);

                if (contratoItem != null && contratoItem.Isdeleted == false)
                {
                    _mapper.Map(contratoItemEdit, contratoItem);
                    _uow.RepositorioJuncalContratoItem.Update(contratoItem);
                    ContratoItemRespuesta contratoItemRes = new();
                    _mapper.Map(contratoItem, contratoItemRes);
                    return Ok(new { success = true, message = "La Contrato Item fue Actualizado ", result = contratoItemRes });
                }

                return Ok(new { success = false, message = "El Contrato Item No Fue Encontrado ", result = new ContratoItemRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar editar el Contrato Item con ID: {Id}", id);
                return StatusCode(500, "Ocurrió un error interno en el servidor al intentar editar el Contrato Item");
            }
        }

    }
}
