﻿using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Intrinsics.X86;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamionController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public CamionController(IUnidadDeTrabajo uow,IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CamionRespuesta>>> GetCamiones()
        {
           
           var ListaCamiones = _uow.RepositorioJuncalCamion.GetAllByCondition(c=>c.Isdeleted==false).ToList(); 

            if (ListaCamiones.Count() > 0)
            {
                List<CamionRespuesta> listaCamionesRespuesta = _mapper.Map<List<CamionRespuesta>>(ListaCamiones);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaCamionesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<CamionRespuesta>()==null });
          

        }

        [HttpPost]
        public ActionResult CargarCamion([FromBody] CamionRequerido camionReq) 
        {
            var camion = _uow.RepositorioJuncalCamion.GetByCondition(c => c.Patente.Equals(camionReq.Patente) && c.Isdeleted == false);       

            if (camion is null)
            {
                JuncalCamion camionNuevo = _mapper.Map<JuncalCamion>(camionReq);
                _uow.RepositorioJuncalCamion.Insert(camionNuevo);
                return Ok(new { success = true, message = "El transportista fue Creado Con Exito", result = camionNuevo });
            }
        
            else return Ok(new { success = false, message = " El Camion Ya Esta Registrado ", result = camion });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedCamion(int id)
        {

            var camion = _uow.RepositorioJuncalCamion.GetById(id);
            if (camion != null && camion.Isdeleted == false)
            {
                camion.Isdeleted = true;
                _uow.RepositorioJuncalCamion.Update(camion);

                return Ok(new { success = true, message = "El transportista fue Eliminado", result = camion.Isdeleted });


            }


            return Ok(new { success = false, message = "El transportista no fue encontrado", result = new JuncalCamion() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCamion(int id, CamionRequerido camionEdit)
        { 
            var camion = _uow.RepositorioJuncalCamion.GetById(id);

            if (camion != null && camion.Isdeleted == false)
            {
                camion = _mapper.Map(camionEdit,camion);
                _uow.RepositorioJuncalCamion.Update(camion);
                return Ok(new { success = true, message = "El transportista fue actualizado", result = camion});
            }

            return Ok(new { success = false, message = "El transportista no fue encontrado", result = new JuncalCamion()==null }) ;


        }
    }
}
