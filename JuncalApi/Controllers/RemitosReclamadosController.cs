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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RemitosReclamadosController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServicioUsuario _serviceUsuario;

        public RemitosReclamadosController(IUnidadDeTrabajo uow, IMapper mapper,IServicioUsuario serviceUsuario)
        {
            _mapper = mapper;
            _uow = uow;
            _serviceUsuario = serviceUsuario;
        }

        #region METODOS GET

        /// <summary>
        /// Obtiene todas los reclamos.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoReclamadoRespuesta>>> GetRemitosReclamo()
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


        /// <summary>
        /// Obtiene todos los reclamos asociados a un remito y una acería específicos.
        /// </summary>
        /// <param name="idRemito">El ID del remito.</param>
        /// <param name="idAceria">El ID de la acería.</param>
        /// <returns>Una acción de resultado que contiene una lista de reclamos o un mensaje de lista vacía.</returns>
        [Route("api/reclamos")]
        [HttpGet]
        public ActionResult GetAllReclamos()
        {
            var reclamos = _uow.RepositorioJuncalRemitosReclamado.GetReclamos();

            List<RemitoReclamadoRespuesta> reclamosNew = new List<RemitoReclamadoRespuesta>();

            if (reclamos.Count()>0)
            {
           
                _mapper.Map(reclamos, reclamosNew);

                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = reclamosNew });

            }

            return Ok(new { success = false, message = "Lista Vacia", result = reclamosNew });

           
        }

        #endregion

        #region METODOS POST

        /// <summary>
        /// Crea una nuevo reclamo.
        /// </summary>
        /// <param name="aceriaReq">Datos requeridos para crear el reclamo</param>
        [HttpPost]
        public ActionResult CargarReclamo([FromBody] RemitosReclamadoRequerido reclamoReq)
        {
            var reclamo = _uow.RepositorioJuncalRemitosReclamado.GetByCondition(c => c.IdRemito==reclamoReq.IdRemito && c.IsDeleted == 0);

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

        #endregion

        #region METODOS PUT

        /// <summary>
        /// Marca un reclamo eliminado
        /// </summary>
        /// <param name="id">ID del reclamo</param>
       
        [Route("Borrar/{idReclamo?}")]
        [HttpPut]
        public IActionResult IsDeletedReclamo(int idReclamo)
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

        /// <summary>
        /// Actualiza un reclamo.
        /// </summary>
        /// <param name="id">ID de la reclamo</param>
        /// <param name="aceriaEdit">Datos actualizados del reclamo</param>
        [HttpPut("{idReclamo}")]
        public async Task<IActionResult> EditReclamo(int idReclamo, RemitosReclamadoRequerido reclamoEdit)
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

        #endregion
    }
}
