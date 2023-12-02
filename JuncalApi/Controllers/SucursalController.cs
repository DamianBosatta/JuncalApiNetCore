using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class SucursalController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<SucursalController> _logger;

        public SucursalController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<SucursalController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SucursalRespuesta>>> GetSucursal()
        {
            try
            {
                var ListaSucursales = _uow.RepositorioJuncalSucursal.GetAll().ToList();

                if (ListaSucursales.Count > 0)
                {
                    List<SucursalRespuesta> listaSucursalesRespuesta = _mapper.Map<List<SucursalRespuesta>>(ListaSucursales);
                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaSucursalesRespuesta });
                }
                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<SucursalRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al obtener las sucursales");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public ActionResult CargarSucursal([FromBody] SucursalRequerida sucursalReq)
        {
            try
            {
                var sucursal = _uow.RepositorioJuncalSucursal.GetByCondition(c => c.Numero.Equals(sucursalReq.Numero));

                if (sucursal is null)
                {
                    JuncalSucursal sucursalNueva = _mapper.Map<JuncalSucursal>(sucursalReq);
                    _uow.RepositorioJuncalSucursal.Insert(sucursalNueva);

                    SucursalRespuesta sucursalRes = _mapper.Map<SucursalRespuesta>(sucursalNueva);
                    return Ok(new { success = true, message = "La Sucursal Fue Creada Con Exito", result = sucursalRes });
                }

                SucursalRespuesta sucursalExiste = _mapper.Map<SucursalRespuesta>(sucursal);
                return Ok(new { success = false, message = "La Sucursal Ya Existe", result = sucursalExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al cargar la sucursal");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedSucursal(int id)
        {
            try
            {
                var sucursal = _uow.RepositorioJuncalEstado.GetById(id);
                if (sucursal != null && sucursal.Isdeleted == false)
                {
                    sucursal.Isdeleted = true;
                    _uow.RepositorioJuncalEstado.Update(sucursal);

                    SucursalRespuesta sucursalRes = _mapper.Map<SucursalRespuesta>(sucursal);
                    return Ok(new { success = true, message = "La Sucursal Fue Eliminada", result = sucursalRes });
                }

                return Ok(new { success = false, message = "La Sucursal No Se Encontro" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al eliminar la sucursal");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSucursal(int id, SucursalRequerida sucursalEdit)
        {
            try
            {
                var sucursal = _uow.RepositorioJuncalSucursal.GetById(id);

                if (sucursal != null)
                {
                    _mapper.Map(sucursalEdit, sucursal);
                    _uow.RepositorioJuncalSucursal.Update(sucursal);

                    SucursalRespuesta sucursalRes = _mapper.Map<SucursalRespuesta>(sucursal);
                    return Ok(new { success = true, message = "La Sucursal Fue Actualizada", result = sucursalRes });
                }

                return Ok(new { success = false, message = "La Sucursal No Fue Encontrada" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al editar la sucursal");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
