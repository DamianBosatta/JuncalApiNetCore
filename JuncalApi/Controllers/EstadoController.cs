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
    public class EstadoController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public EstadoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoRespuesta>>> GetEstados()
        {

            var ListaEstados = _uow.RepositorioJuncalEstado.GetAllByCondition(c => c.Isdeleted == false);

            if (ListaEstados.Count() > 0)
            {
                List<EstadoRespuesta> listaEstadoRespuesta = _mapper.Map<List<EstadoRespuesta>>(ListaEstados);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaEstadoRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<EstadoRespuesta>() == null });


        }

        [HttpPost]
        public ActionResult CargarEstado([FromBody] EstadoRequerido estadoRequerido)
        {
            var estado = _uow.RepositorioJuncalEstado.GetByCondition(c => c.Nombre.Equals(estadoRequerido.Nombre) && c.Isdeleted == false);

            if (estado is null)
            {
                JuncalEstado estadoNuevo = _mapper.Map<JuncalEstado>(estadoRequerido);

                _uow.RepositorioJuncalEstado.Insert(estadoNuevo);
                return Ok(new { success = true, message = "El Estado Fue Creado Con Exito ", result = estadoNuevo });
            }

            return Ok(new { success = false, message = " El Estado Ya Esta Cargado ", result = estado });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedEstado(int id)
        {

            var estado = _uow.RepositorioJuncalEstado.GetById(id);
            if (estado != null && estado.Isdeleted == false)
            {
                estado.Isdeleted = true;
                _uow.RepositorioJuncalEstado.Update(estado);

                return Ok(new { success = true, message = "El estado Fue Eliminado ", result = estado.Isdeleted });

            }

            return Ok(new { success = false, message = "La Estado No Se Encontro ", result = new JuncalEstado() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEstado(int id, EstadoRequerido estadoEdit)
        {
            var estado = _uow.RepositorioJuncalEstado.GetById(id);

            if (estado != null && estado.Isdeleted == false)
            {
                _mapper.Map(estadoEdit, estado);
                _uow.RepositorioJuncalEstado.Update(estado);
                return Ok(new { success = true, message = "La Estado fue Actualizado ", result = estado });
            }

            return Ok(new { success = false, message = "El Estado No Fue Encontrado ", result = new JuncalEstado() == null });


        }





    }
}
