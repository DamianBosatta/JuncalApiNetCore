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
    public class RemitoHistorialController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public RemitoHistorialController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [HttpGet("{idOrden}")]
        public async Task<ActionResult<IEnumerable<RemitoHistorialRespuesta>>> GetRemitoHistoriaById(int idOrden)
        {

            var remitos = _uow.RepositorioJuncalRemitoHistorial.GetAllByCondition(c => c.IdOrden==idOrden).OrderByDescending(o=>o.FechaGenerado).ToList();

            if (remitos.Count()>0)
            {
           
                    List<RemitoHistorialRespuesta> remitosRespuesta = _mapper.Map<List<RemitoHistorialRespuesta>>(remitos);
                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = remitosRespuesta });

                }
                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoHistorialRespuesta>() == null });


            }

        [HttpPost]
        public ActionResult CargarRemitoHistorial([FromBody] RemitoHistorialRequerido remitoRequerido)
        {
          

            if (remitoRequerido != null)
            {
                JuncalRemitoHistorial remitoNuevo = _mapper.Map<JuncalRemitoHistorial>(remitoRequerido);

                _uow.RepositorioJuncalRemitoHistorial.Insert(remitoNuevo);
                return Ok(new { success = true, message = "El Remito Historial fue Creado Con Exito", result = remitoNuevo });
            }

            return Ok(new { success = false, message = " El Remito A Insertar Llego Vacio", result = remitoRequerido });

        }







    }
}
