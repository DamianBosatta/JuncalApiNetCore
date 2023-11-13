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
    public class ProveedorCcMovimientoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ProveedorCcMovimientoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorCcMovimientoRespuesta>>> GetProveedorCcMovimiento()
        {

            var ListaProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCcMovimiento.GetAll();

            if (ListaProveedorCcMovimiento.Any())
            {
                List<ProveedorCcMovimientoRespuesta> ListaProveedorCcMovimientoRespuesta = _mapper.Map<List<ProveedorCcMovimientoRespuesta>>(ListaProveedorCcMovimiento);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaProveedorCcMovimientoRespuesta });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<ProveedorCcMovimientoRespuesta>() == null });


        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedorCcMovimiento(int id)
        {
            var ProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCcMovimiento.GetById(id);

            if (ProveedorCcMovimiento is null)
            {
                return Ok(new { success = false, message = "No Se EncontroLa C.c Del Proveedor", result = new ProveedorCcMovimientoRespuesta() == null });
            }

            var ProveedorCcMovimientoRespuesta = _mapper.Map<ProveedorCcMovimientoRespuesta>(ProveedorCcMovimiento);

            return Ok(new { success = true, message = "C.C Proveedor Encontrada", result = ProveedorCcMovimientoRespuesta });

        }

        [HttpPost]
        public ActionResult CargarProveedorCcMovimiento([FromBody] ProveedorCcMovimientoRequerido ProveedorCcMovimientoReq)
        {

            var ProveedorCcMovimiento = _mapper.Map<JuncalProveedorCcMovimiento>(ProveedorCcMovimientoReq);

            _uow.RepositorioJuncalProveedorCcMovimiento.Insert(ProveedorCcMovimiento);

            var ProveedorCcMovimientoRes = _mapper.Map<ProveedorCcMovimientoRespuesta>(ProveedorCcMovimiento);

            return Ok(new { success = true, message = " La CC Del Proveedor Fue Creada Con Exito ", result = ProveedorCcMovimientoRes });


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedorCcMovimiento(int id, ProveedorCcMovimientoRequerido ProveedorCcMovimientoReq)
        {
            var ProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCcMovimiento.GetById(id);

            if (ProveedorCcMovimiento != null)
            {
                ProveedorCcMovimiento = _mapper.Map<JuncalProveedorCcMovimiento>(ProveedorCcMovimientoReq);
                _uow.RepositorioJuncalProveedorCcMovimiento.Update(ProveedorCcMovimiento);

                var ProveedorCcMovimientoRes = _mapper.Map<ProveedorCcMovimientoRespuesta>(ProveedorCcMovimiento);
                
                return Ok(new { success = true, message = " La CC Ha Sido Del Proveedor Ha Sido Actualizado ", result = ProveedorCcMovimientoRes });
            }

            return Ok(new { success = false, message = "No Se Encontro El CC Del Proveedor  ", result = new CcMovimientoAdelantoRespuesta() == null });


        }


    }
}
