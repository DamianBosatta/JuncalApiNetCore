using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosReclamoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<EstadosReclamoController> _logger;

        public EstadosReclamoController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<EstadosReclamoController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadosReclamoResponse>>> GetEstados()
        {
            try
            {
                var ListaEstados = _uow.RepositorioJuncalEstadosReclamo.GetAllByCondition(c => c.Isdelete == false);

                List<EstadosReclamoResponse> listaEstadoRespuesta = new List<EstadosReclamoResponse>();

                if (ListaEstados.Count() > 0)
                {
                    listaEstadoRespuesta = _mapper.Map<List<EstadosReclamoResponse>>(ListaEstados);
                    return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaEstadoRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = listaEstadoRespuesta });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener estados de reclamo");
                return StatusCode(500, "Ocurrió un error al obtener estados de reclamo");
            }
        }

        [HttpPost]
        public ActionResult CargarEstado([FromBody] EstadosReclamoRequerido estadoRequerido)
        {
            try
            {
                var estado = _uow.RepositorioJuncalEstadosReclamo.GetByCondition(c => c.Nombre == estadoRequerido.Nombre && c.Isdelete == false);

                EstadosReclamoResponse estadoRes = new();

                if (estado is null)
                {
                    JuncalEstadosReclamo estadoNuevo = _mapper.Map<JuncalEstadosReclamo>(estadoRequerido);

                    _uow.RepositorioJuncalEstadosReclamo.Insert(estadoNuevo);
                    _mapper.Map(estadoNuevo, estadoRes);
                    return Ok(new { success = true, message = "El Estado Fue Creado Con Éxito", result = estadoRes });
                }

                return Ok(new { success = false, message = "El Estado Ya Está Cargado", result = estadoRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar estado");
                return StatusCode(500, "Ocurrió un error al cargar el estado");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedEstado(int idEstado)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar estado con ID: {EstadoId}", idEstado);
                return StatusCode(500, "Ocurrió un error al eliminar el estado");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEstado(int idEstado, EstadosReclamoRequerido estadoEdit)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar estado con ID: {EstadoId}", idEstado);
                return StatusCode(500, "Ocurrió un error al editar el estado");
            }
        }
    }
}
