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
    public class CcMovimientoConciliacionController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public CcMovimientoConciliacionController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CcMovimientoConciliacionRespuesta>>> GetCcMovimientoConciliacion()
        {

            var ListaCcMovimientoConciliacion = _uow.RepositorioJuncalCcMovimientoConciliacion.GetAll();

            if (ListaCcMovimientoConciliacion.Any())
            {
                List<CcMovimientoConciliacionRespuesta> ListaCcMovimientoAdelantoRespuesta = _mapper.Map<List<CcMovimientoConciliacionRespuesta>>(ListaCcMovimientoConciliacion);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaCcMovimientoAdelantoRespuesta });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<CcMovimientoConciliacionRespuesta>() == null });


        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdCcMovimientoConciliacion(int id)
        {
            var CcMovimientoConciliacion = _uow.RepositorioJuncalCcMovimientoConciliacion.GetById(id);

            if (CcMovimientoConciliacion is null)
            {
                return Ok(new { success = false, message = "No Se Encontro El Adelanto", result = new CcMovimientoConciliacionRespuesta() == null });
            }


            var CcMovimientoConciliacionResp = _mapper.Map<CcMovimientoConciliacionRespuesta>(CcMovimientoConciliacion);

            return Ok(new { success = true, message = "Conciliacion Encontrada", result = CcMovimientoConciliacionResp });

        }

        [HttpPost]
        public ActionResult CargarCcMovimientoConciliacion([FromBody] CcMovimientoConciliacionRequerido CcMovimientoConciliacionReq)
        {

            var CcMovimientoConciliacion = _mapper.Map<JuncalCcMovimientoConciliacion>(CcMovimientoConciliacionReq);

            _uow.RepositorioJuncalCcMovimientoConciliacion.Insert(CcMovimientoConciliacion);

            var CcMovimientoAdelantoRes = _mapper.Map<CcMovimientoConciliacionRespuesta>(CcMovimientoConciliacion);

            return Ok(new { success = true, message = " La Conciliacion Fue Creada Con Exito ", result = CcMovimientoAdelantoRes });


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditCcMovimientoConciliacion(int id, CcMovimientoConciliacionRequerido CcMovimientoConciliacionReq)
        {
            var CcMovimientoConciliacion = _uow.RepositorioJuncalCcMovimientoConciliacion.GetById(id);

            if (CcMovimientoConciliacion != null)
            {
                CcMovimientoConciliacion = _mapper.Map<JuncalCcMovimientoConciliacion>(CcMovimientoConciliacionReq);
                _uow.RepositorioJuncalCcMovimientoConciliacion.Update(CcMovimientoConciliacion);

                var CcMovimientoConciliacionRes = _mapper.Map<CcMovimientoConciliacionRespuesta>(CcMovimientoConciliacion);
                return Ok(new { success = true, message = " La Conciliacion Ha Sido Actualizado ", result = CcMovimientoConciliacionRes });
            }

            return Ok(new { success = false, message = "No Se Encontro La Conciliacion ", result = new CcMovimientoConciliacionRespuesta() == null });


        }

    }
}
