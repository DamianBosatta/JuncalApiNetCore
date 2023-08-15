using AutoMapper;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public NotificacionController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [Route("notificacion")]
        [HttpGet]
        public async Task<ActionResult<int>>GetSinFacturar()
        {

            var cantidadSinFacturar = _uow.RepositorioJuncalPreFactura.GetAll(a => a.Facturado == false).Count();

            if (cantidadSinFacturar > 0)
            {

                return Ok(new { success = true, message = "La Cantidad Sin Facturar Es De : " + cantidadSinFacturar, result = cantidadSinFacturar });

            }
            return Ok(new { success = false, message = "No Hay Pre Facturar", result = 0 });


        }
    }
}
