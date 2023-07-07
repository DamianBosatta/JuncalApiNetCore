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
    public class EstadosReclamoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public EstadosReclamoController(IUnidadDeTrabajo uow, IMapper mapper)
        {
            _mapper = mapper;
            _uow = uow;
        }

        #region METODOS GET

        /// <summary>
        /// Obtiene la lista de estados.
        /// </summary>
        /// <returns>Lista de estados disponibles.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadosReclamoResponse>>> GetEstados()
        {
            var ListaEstados = _uow.RepositorioJuncalEstadosReclamo.GetAllByCondition(c => c.Isdelete==false);

            List<EstadosReclamoResponse> listaEstadoRespuesta = new List<EstadosReclamoResponse>();

            if (ListaEstados.Count() > 0)
            {
               listaEstadoRespuesta = _mapper.Map<List<EstadosReclamoResponse>>(ListaEstados);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaEstadoRespuesta });
            }

            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = listaEstadoRespuesta });
        }

        #endregion

        #region METODOS POST

        /// <summary>
        /// Carga un nuevo estado.
        /// </summary>
        /// <param name="estadoRequerido">Datos del estado a cargar.</param>
        /// <returns>Estado creado o mensaje de estado ya existente.</returns>
        [HttpPost]
        public ActionResult CargarEstado([FromBody] EstadosReclamoRequerido estadoRequerido)
        {
            var estado = _uow.RepositorioJuncalEstadosReclamo.GetByCondition(c => c.Nombre == estadoRequerido.Nombre &&
            c.Isdelete==false);

            EstadosReclamoResponse estadoRes = new();

            if (estado is null)
            {
                JuncalEstadosReclamo estadoNuevo = _mapper.Map<JuncalEstadosReclamo>(estadoRequerido);

                _uow.RepositorioJuncalEstadosReclamo.Insert(estadoNuevo);
               
                _mapper.Map(estadoNuevo, estadoRes);
                return Ok(new { success = true, message = "El Estado Fue Creado Con Exito", result = estadoRes });
            }

           
            return Ok(new { success = false, message = "El Estado Ya Está Cargado", result = estadoRes });
        }

        #endregion

        #region METODOS PUT

        /// <summary>
        /// Marca un estado como eliminado.
        /// </summary>
        /// <param name="id">ID del estado a eliminar.</param>
        /// <returns>Estado eliminado o mensaje de estado no encontrado.</returns>
        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedEstado(int idEstado)
        {
            var estado = _uow.RepositorioJuncalEstadosReclamo.GetById(idEstado);

            EstadosReclamoResponse estadoRes = new();

            if (estado != null && estado.Isdelete == false)
            {
                estado.Isdelete = true;
                _uow.RepositorioJuncalEstadosReclamo.Update(estado);
                
                _mapper.Map(estado, estadoRes);

                return Ok(new { success = true, message = "El Estado Fue Eliminado", result = estadoRes });
            }

            return Ok(new { success = false, message = "El Estado No Se Encontró", result = estadoRes });
        }

        /// <summary>
        /// Edita un estado existente.
        /// </summary>
        /// <param name="id">ID del estado a editar.</param>
        /// <param name="estadoEdit">Datos actualizados del estado.</param>
        /// <returns>Estado editado o mensaje de estado no encontrado.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditEstado(int idEstado, EstadosReclamoRequerido estadoEdit)
        {
            var estado = _uow.RepositorioJuncalEstadosReclamo.GetById(idEstado);

            EstadosReclamoResponse estadoRes = new();

            if (estado != null && estado.Isdelete == false)
            {
                _mapper.Map(estadoEdit, estado);
                _uow.RepositorioJuncalEstadosReclamo.Update(estado);
                
                _mapper.Map(estado, estadoRes);
               
                return Ok(new { success = true, message = "El Estado Fue Actualizado", result = estadoRes });
            }

            return Ok(new { success = false, message = "El Estado No Fue Encontrado", result = estadoRes });
        }

        #endregion
    }
}
