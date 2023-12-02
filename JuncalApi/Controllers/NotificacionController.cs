using AutoMapper;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificacionController> _logger;

        public NotificacionController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<NotificacionController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<int>> GetNotificaciones()
        {
            try
            {
                DateOnly Fecha = DateOnly.FromDateTime(DateTime.Now);
                var cantidadSinFacturar = _uow.RepositorioJuncalPreFactura.GetAll(a => a.Facturado == false).Count();
                var cantidadReclamos = _uow.RepositorioJuncalRemitosReclamado.GetAllByCondition(x => x.IdEstadoReclamo == 1).Count();
                var notificacion = _uow.RepositorioJuncalNotificacion.GetAll().FirstOrDefault();
                int contratos = notificacion?.CantidadContratos ?? 0;
                bool contratosComprobados = false;

                if (notificacion == null)
                {
                    notificacion = new JuncalNotificacione();
                    notificacion.Fecha = Fecha;
                    contratos = _uow.RepositorioJuncalContrato.cambiarEstado(Fecha.ToDateTime(new TimeOnly(0, 0, 0)));
                    notificacion.CantidadContratos = contratos;
                    _uow.RepositorioJuncalNotificacion.Insert(notificacion);
                    contratosComprobados = true;
                }
                else if (notificacion.Fecha < Fecha)
                {
                    notificacion.Fecha = Fecha;
                    contratos = _uow.RepositorioJuncalContrato.cambiarEstado(Fecha.ToDateTime(new TimeOnly(0, 0, 0)));
                    notificacion.CantidadContratos = contratos;
                    _uow.RepositorioJuncalNotificacion.Update(notificacion);
                    contratosComprobados = true;
                }

                var response = new
                {
                    success = true,
                    message = "Notificaciones",
                    sinFacturar = cantidadSinFacturar,
                    reclamos = cantidadReclamos,
                    total = cantidadSinFacturar + cantidadReclamos,
                    fechaComprobacionContratos = notificacion.Fecha,
                    contratosCambiados = contratos,
                    contratoscomprobados = contratosComprobados
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las notificaciones");
                return StatusCode(500, "Ocurrió un error al obtener las notificaciones");
            }
        }
    }
}
