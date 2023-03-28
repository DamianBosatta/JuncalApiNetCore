using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public ContratoItemController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


 

        [HttpGet("{idContrato}")]
        public async Task<ActionResult<IEnumerable<ContratoItemRespuesta>>> GetAContratosItemForContrato(int idContrato)
        {

            var ListaContratosItem = _uow.RepositorioJuncalContratoItem.GetAllByCondition(c => c.IdContrato==idContrato && c.Isdeleted == false);

            if (ListaContratosItem.Count() > 0)
            {
                List<ContratoItemRespuesta> listaContratoItemRespuesta = _mapper.Map<List<ContratoItemRespuesta>>(ListaContratosItem);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaContratoItemRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ContratoItemRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarContratoItem([FromBody] List<ContratoItemRequerido> listaContratoItemRequerido)
        {
            
            List<ContratoItemRespuesta> contratoItemRespuesta = new List<ContratoItemRespuesta>(); 


            if (listaContratoItemRequerido.Count() > 0)
            {


                foreach(var item in listaContratoItemRequerido)
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


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedContratoItem(int id)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditContratoItem(int id, ContratoItemRequerido contratoItemEdit)
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






    }
}
