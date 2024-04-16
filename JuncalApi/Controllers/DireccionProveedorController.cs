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

    [Route("api/[controller]")]
    [ApiController]
    public class DireccionProveedorController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DireccionProveedorController> _logger;

        public DireccionProveedorController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<DireccionProveedorController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DireccionProveedorRespuesta>>> GetDireccionProveedor(int idProveedor)
        {
            try
            {
                var ListaDirecciones = _uow.RepositorioJuncalDireccionProveedor.GetAllByCondition(c => c.Isdelete == false && c.IdProveedor == idProveedor).ToList();

                if (ListaDirecciones.Count() > 0)
                {
                    List<DireccionProveedorRespuesta> listaDireccionesRespuesta = _mapper.Map<List<DireccionProveedorRespuesta>>(ListaDirecciones);
                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaDireccionesRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<DireccionProveedorRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener direcciones del proveedor con ID: {ProveedorId}", idProveedor);
                return StatusCode(500, "Ocurrió un error al obtener direcciones del proveedor");
            }
        }

        [HttpPost]
        public ActionResult CargarDireccion([FromBody] DireccionProveedorRequerido direccionReq)
        {
            try
            {
                var DireccionProveedor = _uow.RepositorioJuncalDireccionProveedor.GetByCondition(c => c.Direccion == direccionReq.Direccion && c.IdProveedor == direccionReq.IdProveedor && c.Isdelete == false);

                if (DireccionProveedor is null)
                {
                    JuncalDireccionProveedor direccionNueva = _mapper.Map<JuncalDireccionProveedor>(direccionReq);

                    _uow.RepositorioJuncalDireccionProveedor.Insert(direccionNueva);
                    DireccionProveedorRespuesta direccionProveedorRes = new();
                    _mapper.Map(direccionNueva, direccionProveedorRes);
                    return Ok(new { success = true, message = "La Direccion Fue Creada Con Exito", result = direccionProveedorRes });
                }

                DireccionProveedorRespuesta direccionProveedorExiste = new();
                _mapper.Map(DireccionProveedor, direccionProveedorExiste);
                return Ok(new { success = false, message = " La Direccion Ya Existe ", result = direccionProveedorExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la dirección del proveedor");
                return StatusCode(500, "Ocurrió un error al cargar la dirección del proveedor");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeleteDireccion(int id)
        {
            try
            {
                var direccion = _uow.RepositorioJuncalDireccionProveedor.GetById(id);

                if (direccion != null && direccion.Isdelete == false)
                {
                    direccion.Isdelete = true;
                    _uow.RepositorioJuncalDireccionProveedor.Update(direccion);
                    DireccionProveedorRespuesta direccionProveedorRes = new();
                    _mapper.Map(direccion, direccionProveedorRes);

                    return Ok(new { success = true, message = "La Direccion Fue Eliminada ", result = direccionProveedorRes });
                }

                return Ok(new { success = false, message = "La Direccion no fue encontrada", result = new DireccionProveedorRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la dirección con ID: {DireccionId}", id);
                return StatusCode(500, "Ocurrió un error al eliminar la dirección");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditDireccion(int id, DireccionProveedorRequerido direccionEdit)
        {
            try
            {
                var direccion = _uow.RepositorioJuncalDireccionProveedor.GetById(id);

                if (direccion != null && direccion.Isdelete == false)
                {
                    _mapper.Map(direccionEdit, direccion);
                    _uow.RepositorioJuncalDireccionProveedor.Update(direccion);
                    DireccionProveedorRespuesta direccionProveedorRes = new();
                    _mapper.Map(direccion, direccionProveedorRes);
                    return Ok(new { success = true, message = "La Direccion fue actualizada", result = direccionProveedorRes });
                }

                return Ok(new { success = false, message = "La Direccion no fue encontrada ", result = new DireccionProveedorRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la dirección con ID: {DireccionId}", id);
                return StatusCode(500, "Ocurrió un error al editar la dirección");
            }
        }
    }
}
