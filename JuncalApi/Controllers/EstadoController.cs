using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

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
                EstadoRespuesta estadoRes = new();
                _mapper.Map(estadoNuevo, estadoRes);
                return Ok(new { success = true, message = "El Estado Fue Creado Con Exito ", result = estadoRes });
            }
            EstadoRespuesta estadoExiste = new();
            _mapper.Map(estado, estadoExiste);
            return Ok(new { success = false, message = " El Estado Ya Esta Cargado ", result = estadoExiste });

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
                EstadoRespuesta estadoRes = new();
                _mapper.Map(estado, estadoRes);

                return Ok(new { success = true, message = "El Estado Fue Eliminado ", result = estadoRes });

            }

            return Ok(new { success = false, message = "La Estado No Se Encontro ", result = new EstadoRespuesta() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEstado(int id, EstadoRequerido estadoEdit)
        {
            var estado = _uow.RepositorioJuncalEstado.GetById(id);

            if (estado != null && estado.Isdeleted == false)
            {
                _mapper.Map(estadoEdit, estado);
                _uow.RepositorioJuncalEstado.Update(estado);
                EstadoRespuesta estadoRes = new();
                _mapper.Map(estado, estadoRes);
                return Ok(new { success = true, message = "La Estado fue Actualizado ", result = estadoRes });
            }

            return Ok(new { success = false, message = "El Estado No Fue Encontrado ", result = new EstadoRespuesta() == null });


        }





    }
}
