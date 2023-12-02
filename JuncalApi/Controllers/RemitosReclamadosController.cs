using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Codigos_Utiles;
using JuncalApi.Servicios;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RemitosReclamadosController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServicioUsuario _serviceUsuario;
        private readonly ILogger<RemitosReclamadosController> _logger;

        public RemitosReclamadosController(IUnidadDeTrabajo uow, IMapper mapper, IServicioUsuario serviceUsuario, ILogger<RemitosReclamadosController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _serviceUsuario = serviceUsuario;
            _logger = logger;
        }

        #region METODOS GET

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoReclamadoRespuesta>>> GetRemitosReclamo()
        {
            try
            {
                var remitosReclamo = _uow.RepositorioJuncalRemitosReclamado.GetReclamos();

                List<RemitoReclamadoRespuesta> listaAceriasRespuesta = new List<RemitoReclamadoRespuesta>();

                if (remitosReclamo.Count() > 0)
                {
                    listaAceriasRespuesta = _mapper.Map<List<RemitoReclamadoRespuesta>>(remitosReclamo);
                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaAceriasRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = listaAceriasRespuesta });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetRemitosReclamo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("api/reclamos")]
        [HttpGet]
        public ActionResult GetAllReclamos()
        {
            try
            {
                var reclamos = _uow.RepositorioJuncalRemitosReclamado.GetReclamos();

                List<RemitoReclamadoRespuesta> reclamosNew = new List<RemitoReclamadoRespuesta>();

                if (reclamos.Count() > 0)
                {
                    _mapper.Map(reclamos, reclamosNew);

                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = reclamosNew });
                }

                return Ok(new { success = false, message = "Lista Vacia", result = reclamosNew });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en GetAllReclamos");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Agregar aquí los otros métodos GET con manejo de excepciones similar

        #endregion

        #region METODOS POST

        [HttpPost]
        public ActionResult CargarReclamo([FromBody] RemitosReclamadoRequerido reclamoReq)
        {
            try
            {
                var reclamo = _uow.RepositorioJuncalRemitosReclamado.GetByCondition(c => c.IdRemito == reclamoReq.IdRemito && c.IsDeleted == 0);

                JuncalRemitosReclamado reclamoNuevo = new JuncalRemitosReclamado();

                if (reclamo is null)
                {
                    reclamoNuevo = _mapper.Map<JuncalRemitosReclamado>(reclamoReq);
                    _uow.RepositorioJuncalRemitosReclamado.Insert(reclamoNuevo);
                    RemitoReclamadoRespuesta reclamoRes = new RemitoReclamadoRespuesta();
                    _mapper.Map(reclamoNuevo, reclamoRes);

                    return Ok(new { success = true, message = "El Reclamo fue Creado Con Exito", result = reclamoRes });
                }

                return Ok(new { success = false, message = "La Aceria Ya Existe", result = reclamoNuevo });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en CargarReclamo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Agregar aquí los otros métodos POST con manejo de excepciones similar

        #endregion

        #region METODOS PUT

        [Route("Borrar/{idReclamo?}")]
        [HttpPut]
        public IActionResult IsDeletedReclamo(int idReclamo)
        {
            try
            {
                var reclamo = _uow.RepositorioJuncalRemitosReclamado.GetById(idReclamo);
                RemitoReclamadoRespuesta reclamoRes = new RemitoReclamadoRespuesta();

                if (reclamo != null && reclamo.IsDeleted == 0)
                {
                    reclamo.IsDeleted = 1;
                    _uow.RepositorioJuncalRemitosReclamado.Update(reclamo);
                    _mapper.Map(reclamo, reclamoRes);

                    return Ok(new { success = true, message = "El Reclamo Fue Eliminado", result = reclamoRes });
                }

                return Ok(new { success = false, message = "El Reclamo no fue encontrado", result = reclamoRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en IsDeletedReclamo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{idReclamo}")]
        public async Task<IActionResult> EditReclamo(int idReclamo, RemitosReclamadoRequerido reclamoEdit)
        {
            try
            {
                var reclamo = _uow.RepositorioJuncalRemitosReclamado.GetById(idReclamo);
                RemitoReclamadoRespuesta reclamoRes = new RemitoReclamadoRespuesta();

                if (reclamo != null && reclamo.IsDeleted == 0)
                {
                    _mapper.Map(reclamoEdit, reclamo);
                    _uow.RepositorioJuncalRemitosReclamado.Update(reclamo);
                    _mapper.Map(reclamo, reclamoRes);

                    return Ok(new { success = true, message = "El Reclamo fue actualizado", result = reclamoRes });
                }

                return Ok(new { success = false, message = "El Reclamo no fue encontrado", result = reclamoRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en EditReclamo");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        // Agregar aquí los otros métodos PUT con manejo de excepciones similar

        #endregion
    }
}
