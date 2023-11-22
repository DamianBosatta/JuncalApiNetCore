using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CcTiposMovimientoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public CcTiposMovimientoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CcTiposMovimientoRespuesta>>> GetCcTiposMovimiento()
        {

            var ListaCcTiposMovimiento = _uow.RepositorioJuncalCcTipoMovimiento.GetAll();

            if (ListaCcTiposMovimiento.Any())
            {
                List<CcTiposMovimientoRespuesta> ListaCcTiposMovimientoRespuesta = _mapper.Map<List<CcTiposMovimientoRespuesta>>(ListaCcTiposMovimiento);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaCcTiposMovimientoRespuesta });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<CcTiposMovimientoRespuesta>() == null });


        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdCcTiposMovimiento(int id)
        {
            var CcTiposMovimiento = _uow.RepositorioJuncalCcTipoMovimiento.GetById(id);

            if (CcTiposMovimiento is null)
            {
                return Ok(new { success = false, message = "No Se Encontro El Tipo De Movimiento", result = new CcTiposMovimientoRespuesta() == null });
            }


            var CcMovimientoRemitoResp = _mapper.Map<CcMovimientoRemitoRespuesta>(CcTiposMovimiento);

            return Ok(new { success = true, message = "Remito Encontrado", result = CcMovimientoRemitoResp });

        }

        [HttpPost]
        public ActionResult CargarCcTiposMovimiento([FromBody] CcTiposMovimientoRequerido CcTiposMovimientoReq)
        {

            var CcTiposMovimiento = _mapper.Map<JuncalCcTiposMovimiento>(CcTiposMovimientoReq);

            _uow.RepositorioJuncalCcTipoMovimiento.Insert(CcTiposMovimiento);

            var CcTiposMovimientoRes = _mapper.Map<CcTiposMovimientoRespuesta>(CcTiposMovimiento);

            return Ok(new { success = true, message = " El Tipo de Movimiento Fue Creado Con Exito ", result = CcTiposMovimientoRes });


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditCcTiposMovimiento(int id,CcTiposMovimientoRequerido CcTiposMovimientoReq)
        {
            var CcTiposMovimiento = _uow.RepositorioJuncalCcTipoMovimiento.GetById(id);

            if (CcTiposMovimiento != null)
            {
                CcTiposMovimiento = _mapper.Map<JuncalCcTiposMovimiento>(CcTiposMovimientoReq);
                _uow.RepositorioJuncalCcTipoMovimiento.Update(CcTiposMovimiento);

                var CcTiposMovimientoRes = _mapper.Map<CcTiposMovimientoRespuesta>(CcTiposMovimiento);
                return Ok(new { success = true, message = " El Tipo De Movimiento Sido Actualizado ", result = CcTiposMovimientoRes });
            }

            return Ok(new { success = false, message = "No Se Encontro El Tipo De Movimiento", result = new CcTiposMovimientoRespuesta() == null });

        }



    }
}
