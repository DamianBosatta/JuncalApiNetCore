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

        public CamionController(IUnidadDeTrabajo uow,IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CamionRespuesta>>> GetCamiones()
        {

            List<ItemCamion> ListaCamiones = _uow.RepositorioJuncalCamion.GetAllCamiones().ToList(); 

            if (ListaCamiones.Count() > 0)
            {
                List<CamionRespuesta> listaCamionesRespuesta = _mapper.Map<List<CamionRespuesta>>(ListaCamiones);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaCamionesRespuesta.ToList() });

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
                CamionRespuesta camionRes = new();
                _mapper.Map(camionNuevo, camionRes);
                return Ok(new { success = true, message = "El transportista fue Creado Con Exito", result = camionRes });
            }

            CamionRespuesta camionExiste = new();
            _mapper.Map(camion, camionExiste);
            return Ok(new { success = false, message = " El Camion Ya Esta Registrado ", result = camionExiste });

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
                CamionRespuesta camionRes = new();
                _mapper.Map(camion, camionRes);

                return Ok(new { success = true, message = "El Camion fue Eliminado", result = camionRes });


            }
       

            return Ok(new { success = false, message = "El Camion no fue encontrado", result = new CamionRespuesta() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCamion(int id, CamionRequerido camionEdit)
        { 
            var camion = _uow.RepositorioJuncalCamion.GetById(id);

            if (camion != null && camion.Isdeleted == false)
            {
                camion = _mapper.Map(camionEdit,camion);
                _uow.RepositorioJuncalCamion.Update(camion);
                CamionRespuesta camionRes = new();
                _mapper.Map(camion, camionRes);
                return Ok(new { success = true, message = "El Camion fue actualizado", result = camionRes});
            }

            return Ok(new { success = false, message = "El Camion no fue encontrado", result = new CamionRespuesta()==null }) ;


        }
    }
}
