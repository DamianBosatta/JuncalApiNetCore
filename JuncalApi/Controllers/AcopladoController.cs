﻿using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcopladoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public AcopladoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AcopladoRespuesta>>> GetAcoplados()
        {

            var ListaAcoplados = _uow.RepositorioJuncalAcoplado.GetAllByCondition(c => c.Isdeleted == false);

            if (ListaAcoplados.Count() > 0)
            {
                List<AcopladoRespuesta> listaAcopladosRespuesta = _mapper.Map<List<AcopladoRespuesta>>(ListaAcoplados);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaAcopladosRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<AcopladoRespuesta>() == null });


        }



        [HttpPost]
        public ActionResult CargarAcoplado([FromBody] AcopladoRequerido acopladoRequerido)
        {
            var acoplado = _uow.RepositorioJuncalAcoplado.GetByCondition(c => c.Patente == acopladoRequerido.Patente);
            
            if(acoplado is null)
            {
                var acopladoNuevo = _mapper.Map<JuncalAcoplado>(acopladoRequerido);
                _uow.RepositorioJuncalAcoplado.Insert(acopladoNuevo);
                return Ok(new { success = true, message = "El Acoplado Fue Creado Con Exito ", result = acopladoNuevo });

            }
               
            return Ok(new { success = false, message = " Ya Tenemos Un Acoplado Con Esa Patente ", result = acoplado });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedAcoplado(int id)
        {

            var acoplado = _uow.RepositorioJuncalAcoplado.GetById(id);
            if (acoplado != null)
            {
                acoplado.Isdeleted = true;
                _uow.RepositorioJuncalAcoplado.Update(acoplado);

                return Ok(new { success = true, message = "El Acoplado Fue Eliminado ", result = acoplado.Isdeleted });

            }

            return Ok(new { success = false, message = " No Se Encontro El Acoplado ", result = new JuncalAcoplado() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAcoplado(int id, AcopladoRequerido acopladoEdit)
        {
            var acoplado = _uow.RepositorioJuncalAcoplado.GetById(id);

            if (acoplado != null)
            {
                _mapper.Map(acopladoEdit, acoplado);
                _uow.RepositorioJuncalAcoplado.Update(acoplado);
                return Ok(new { success = true, message = "El Acoplado  fue Actualizado ", result = acoplado });
            }

            return Ok(new { success = false, message = "El Acoplado No Fue Encontrado ", result = new JuncalAcoplado() == null });


        }







    }
}
