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
    public class ProveedorListaPrecioController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ProveedorListaPrecioController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorListaPrecioRespuesta>>> GetProveedorListaPrecio()
        {

            var ListaProveedorListaPrecio = _uow.RepositorioJuncalProveedorListaPrecio.GetAll();

            if (ListaProveedorListaPrecio.Any())
            {
                List<ProveedorListaPrecioRespuesta> ListaProveedorListaPrecioRespuesta = _mapper.Map<List<ProveedorListaPrecioRespuesta>>(ListaProveedorListaPrecio);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaProveedorListaPrecioRespuesta });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<ProveedorListaPrecioRespuesta>() == null });


        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedorListaPrecio(int id)
        {
            var ProveedorListaPrecio = _uow.RepositorioJuncalProveedorListaPrecio.GetById(id);

            if (ProveedorListaPrecio is null)
            {
                return Ok(new { success = false, message = "No Se Encontro La lista de precio del Proveedor", result = new ProveedorListaPrecioRespuesta() == null });
            }

            var ProveedorListaPrecioRespuesta = _mapper.Map<ProveedorListaPrecioRespuesta>(ProveedorListaPrecio);

            return Ok(new { success = true, message = "Lista de precio del Proveedor Encontrada", result = ProveedorListaPrecioRespuesta });

        }

        [HttpPost]
        public ActionResult CargarProveedorListaPrecio([FromBody] ProveedorListaPrecioRequerido ProveedorListaPrecioReq)
        {

            var ProveedorListaPrecio = _mapper.Map<JuncalProveedorListaprecio>(ProveedorListaPrecioReq);

            _uow.RepositorioJuncalProveedorListaPrecio.Insert(ProveedorListaPrecio);

            var ProveedorListaPrecioRes = _mapper.Map<ProveedorListaPrecioRespuesta>(ProveedorListaPrecio);

            return Ok(new { success = true, message = " La lista de precio del Proveedor Fue Creada Con Exito ", result = ProveedorListaPrecioRes });


        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedorListaPrecio(int id, ProveedorListaPrecioRequerido ProveedorListaPrecioReq)
        {
            var ProveedorListaPrecio = _uow.RepositorioJuncalProveedorListaPrecio.GetById(id);

            if (ProveedorListaPrecio != null)
            {
                ProveedorListaPrecio = _mapper.Map<JuncalProveedorListaprecio>(ProveedorListaPrecioReq);
                _uow.RepositorioJuncalProveedorListaPrecio.Update(ProveedorListaPrecio);

                var ProveedorListaPrecioRes = _mapper.Map<ProveedorListaPrecioRespuesta>(ProveedorListaPrecio);

                return Ok(new { success = true, message = " La Lista de precio Del Proveedor Ha Sido Actualizada ", result = ProveedorListaPrecioRes });
            }

            return Ok(new { success = false, message = "No Se Encontro la lista de precio Del Proveedor  ", result = new ProveedorListaPrecioRespuesta() == null });


        }
    }
}
