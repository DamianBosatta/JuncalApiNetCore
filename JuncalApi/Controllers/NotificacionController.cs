using AutoMapper;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{

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

        [HttpGet]
        public async Task<ActionResult<int>> GetNotificaciones()
        {
            var cantidadSinFacturar = _uow.RepositorioJuncalPreFactura.GetAll(a => a.Facturado == false).Count();
            var cantidadReclamos = _uow.RepositorioJuncalRemitosReclamado.GetAllByCondition(x => x.IdEstadoReclamo == 1).Count();

            return Ok(new
            {
                success = true,
                message = "Notificaciones",
                sinFacturar= cantidadSinFacturar,
                reclamos = cantidadReclamos,
                total = cantidadSinFacturar+ cantidadReclamos
            });
        }


    }
}
