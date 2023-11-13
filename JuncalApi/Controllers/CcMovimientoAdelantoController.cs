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
    public class CcMovimientoAdelantoController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public CcMovimientoAdelantoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<CcMovimientoAdelantoRespuesta>>> GetCcMovimientoAdelanto()
        {

            var ListaCcMovimientoAdelanto = _uow.RepositorioJuncalCcMovimientoAdelanto.GetAll();

            if (ListaCcMovimientoAdelanto.Any())
            {
                List<CcMovimientoAdelantoRespuesta> ListaCcMovimientoAdelantoRespuesta = _mapper.Map<List<CcMovimientoAdelantoRespuesta>> (ListaCcMovimientoAdelanto);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaCcMovimientoAdelantoRespuesta });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<CcMovimientoAdelantoRespuesta>() == null });


        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdCcMovimientoAdelanto(int id)
        {
            var CcMovimientoAdelanto = _uow.RepositorioJuncalCcMovimientoAdelanto.GetById(id);

            if (CcMovimientoAdelanto is null)
            {
                return Ok(new { success = false, message = "No Se Encontro El Adelanto", result = new CcMovimientoAdelantoRespuesta() == null });
            }
        
           var CcMovimientoAdelantoRespuesta = _mapper.Map<CcMovimientoAdelantoRespuesta>(CcMovimientoAdelanto);

            return Ok(new { success = true, message = "Adelanto Encontrado", result = CcMovimientoAdelantoRespuesta });

        }

        [HttpPost]
        public ActionResult CargarCcMovimientoAdelanto([FromBody] CcMovimientoAdelantoRequerido CcMovimientoAdelantoReq)
        {

            var MovimeintoAdelantoNuevo = _mapper.Map<JuncalCcMovimeintoAdelanto>(CcMovimientoAdelantoReq);

                _uow.RepositorioJuncalCcMovimientoAdelanto.Insert(MovimeintoAdelantoNuevo);

            var CcMovimientoAdelantoRes = _mapper.Map<CcMovimientoAdelantoRespuesta>(MovimeintoAdelantoNuevo);
                
            return Ok(new { success = true, message = " El Adelanto Fue Creado Con Exito ", result = CcMovimientoAdelantoRes });
                  

        }
     

        [HttpPut("{id}")]  
        public async Task<IActionResult> EditCcMovimientoAdelanto(int id, CcMovimientoAdelantoRequerido CcMovimientoAdelantoReq)
        {
            var CcMovimientoAdelanto = _uow.RepositorioJuncalCcMovimientoAdelanto.GetById(id);

            if (CcMovimientoAdelanto != null)
            {
                CcMovimientoAdelanto = _mapper.Map<JuncalCcMovimeintoAdelanto>(CcMovimientoAdelantoReq);
                _uow.RepositorioJuncalCcMovimientoAdelanto.Update(CcMovimientoAdelanto);
                
            var CcMovimientoAdelantoRes = _mapper.Map<CcMovimientoAdelantoRespuesta>(CcMovimientoAdelanto);
                return Ok(new { success = true, message = " El Adelanto Ha Sido Actualizado ", result = CcMovimientoAdelantoRes });
            }

            return Ok(new { success = false, message = "No Se Encontro El Adelanto ", result = new CcMovimientoAdelantoRespuesta() == null });


        }

    }
}
