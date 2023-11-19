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
    public class ProveedorCuentaCorrienteController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ProveedorCuentaCorrienteController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorCuentaCorrienteRespuesta>>> GetProveedorCcMovimiento()
        {

            var ListaProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetProveedorCcMovimientos(0);

            if (ListaProveedorCcMovimiento.Any())
            {
               
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaProveedorCcMovimiento });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<ProveedorCuentaCorrienteRespuesta>() == null });


        }
        [Route("Proveedor")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorCuentaCorrienteRespuesta>>> GetProveedorCcMovimientoForIdProveedor(int idProveedor)
        {

            var ListaProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetProveedorCcMovimientos(idProveedor);

            if (ListaProveedorCcMovimiento.Any())
            {
               
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaProveedorCcMovimiento });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<ProveedorCuentaCorrienteRespuesta>() == null });


        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedorCcMovimiento(int id)
        {
            var ProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetById(id);

            if (ProveedorCcMovimiento is null)
            {
                return Ok(new { success = false, message = "No Se EncontroLa C.c Del Proveedor", result = new ProveedorCuentaCorrienteRespuesta() == null });
            }

            var ProveedorCcMovimientoRespuesta = _mapper.Map<ProveedorCuentaCorrienteRespuesta>(ProveedorCcMovimiento);

            return Ok(new { success = true, message = "C.C Proveedor Encontrada", result = ProveedorCcMovimientoRespuesta });

        }

        [HttpPost]
        public ActionResult CargarProveedorCcMovimiento([FromBody] ProveedorCuentaCorrienteRequerido ProveedorCcMovimientoReq)
        {

            var ProveedorCcMovimiento = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCcMovimientoReq);

            _uow.RepositorioJuncalProveedorCuentaCorriente.Insert(ProveedorCcMovimiento);

            var ProveedorCcMovimientoRes = _mapper.Map<ProveedorCuentaCorrienteRespuesta>(ProveedorCcMovimiento);

            return Ok(new { success = true, message = " La CC Del Proveedor Fue Creada Con Exito ", result = ProveedorCcMovimientoRes });


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedorCcMovimiento(int id, ProveedorCuentaCorrienteRequerido ProveedorCcMovimientoReq)
        {
            var ProveedorCcMovimiento = _uow.RepositorioJuncalProveedorCuentaCorriente.GetById(id);

            if (ProveedorCcMovimiento != null)
            {
                ProveedorCcMovimiento = _mapper.Map<JuncalProveedorCuentaCorriente>(ProveedorCcMovimientoReq);
                _uow.RepositorioJuncalProveedorCuentaCorriente.Update(ProveedorCcMovimiento);

                var ProveedorCcMovimientoRes = _mapper.Map<ProveedorCuentaCorrienteRespuesta>(ProveedorCcMovimiento);
                
                return Ok(new { success = true, message = " La CC Ha Sido Del Proveedor Ha Sido Actualizado ", result = ProveedorCcMovimientoRes });
            }

            return Ok(new { success = false, message = "No Se Encontro El CC Del Proveedor  ", result = new CcMovimientoAdelantoRespuesta() == null });


        }


    }
}
