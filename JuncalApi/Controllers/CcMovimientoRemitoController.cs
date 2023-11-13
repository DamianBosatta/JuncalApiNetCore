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
    public class CcMovimientoRemitoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public CcMovimientoRemitoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CcMovimientoRemitoRespuesta>>> GetCcMovimientoRemito()
        {

            var ListaCcMovimientoRemito = _uow.RepositorioJuncalCcMovimientoRemito.GetAll();

            if (ListaCcMovimientoRemito.Any())
            {
                List<CcMovimientoRemitoRespuesta> ListaCcMovimientoRemitoRespuesta = _mapper.Map<List<CcMovimientoRemitoRespuesta>>(ListaCcMovimientoRemito);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaCcMovimientoRemitoRespuesta });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<CcMovimientoRemitoRespuesta>() == null });


        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdCcMovimientoRemito(int id)
        {
            var CcMovimientoRemito = _uow.RepositorioJuncalCcMovimientoRemito.GetById(id);

            if (CcMovimientoRemito is null)
            {
                return Ok(new { success = false, message = "No Se Encontro El Movimiento de Remito", result = new CcMovimientoRemitoRespuesta() == null });
            }


            var CcMovimientoRemitoResp = _mapper.Map<CcMovimientoRemitoRespuesta>(CcMovimientoRemito);

            return Ok(new { success = true, message = "Remito Encontrado", result = CcMovimientoRemitoResp });

        }

        [HttpPost]
        public ActionResult CargarCcMovimientoRemito([FromBody] CcMovimientoRemitoRequerido CcMovimientoRemitoReq)
        {

            var CcMovimientoRemito = _mapper.Map<JuncalCcMovimientoRemito>(CcMovimientoRemitoReq);

            _uow.RepositorioJuncalCcMovimientoRemito.Insert(CcMovimientoRemito);

            var CcMovimientoRemitoRes = _mapper.Map<CcMovimientoRemitoRespuesta>(CcMovimientoRemito);

            return Ok(new { success = true, message = " El Movimiento De Remito Fue Creado Con Exito ", result = CcMovimientoRemitoRes });


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditCcMovimientoRemito(int id, CcMovimientoRemitoRequerido CcMovimientoRemitoReq)
        {
            var CcMovimientoRemito = _uow.RepositorioJuncalCcMovimientoRemito.GetById(id);

            if (CcMovimientoRemito != null)
            {
                CcMovimientoRemito = _mapper.Map<JuncalCcMovimientoRemito>(CcMovimientoRemitoReq);
                _uow.RepositorioJuncalCcMovimientoRemito.Update(CcMovimientoRemito);

                var CcMovimientoRemitoRes = _mapper.Map<CcMovimientoRemitoRespuesta>(CcMovimientoRemito);
                return Ok(new { success = true, message = " El Movimiento De Remito Ha Sido Actualizado ", result = CcMovimientoRemitoRes });
            }

            return Ok(new { success = false, message = "No Se Encontro El Movimiento De Remito ", result = new CcMovimientoRemitoRespuesta() == null });


        }


    }
}
