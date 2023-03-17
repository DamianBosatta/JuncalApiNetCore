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

        public ContratoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoRespuesta>>> GetAContratos()
        {

            var ListaContratos = _uow.RepositorioJuncalContrato.GetAllByCondition(c => c.Isdeleted == false);

            if (ListaContratos.Count() > 0)
            {
                List<ContratoRespuesta> listaContratoRespuesta = _mapper.Map<List<ContratoRespuesta>>(ListaContratos);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaContratoRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ContratoRespuesta>() == null });


        }

        [HttpPost]
        public ActionResult CargarContrato (ContratoRequerido contratoRequerido)
        {
            var contrato = _uow.RepositorioJuncalContrato.GetByCondition(c => c.IdAceria==contratoRequerido.IdAceria && c.Isdeleted == false
            && c.FechaVigencia>= DateTime.Now && c.FechaVencimiento <= DateTime.Now);

            if (contrato is null)
            {
                JuncalContrato contratoNuevo = _mapper.Map<JuncalContrato>(contratoRequerido);                              
                _uow.RepositorioJuncalContrato.Insert(contratoNuevo);
                return Ok(new { success = true, message = "El Contrato Fue Creado Con Exito ", result = contratoNuevo });
            }

            return Ok(new { success = false, message = " La Aceria Ya Tiene Un Contrato Vigente ", result = contrato });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedContrato(int id)
        {

            var contrato = _uow.RepositorioJuncalContrato.GetById(id);
            if (contrato != null && contrato.Isdeleted == false)
            {
                contrato.Isdeleted = true;
                _uow.RepositorioJuncalContrato.Update(contrato);

                return Ok(new { success = true, message = "El Contrato Fue Eliminado ", result = contrato.Isdeleted });

            }

            return Ok(new { success = false, message = "La Contrato No Se Encontro ", result = new JuncalContrato() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditContrato(int id, ContratoRequerido contratoEdit)
        {
            var contrato = _uow.RepositorioJuncalContrato.GetById(id);

            if (contrato != null && contrato.Isdeleted == false)
            {
                  _mapper.Map(contratoEdit, contrato);
                _uow.RepositorioJuncalContrato.Update(contrato);
                return Ok(new { success = true, message = "La Contrato fue Actualizado ", result = contrato });
            }

            return Ok(new { success = false, message = "El Contrato No Fue Encontrado ", result = new JuncalContrato() == null });


        }





    }
}
