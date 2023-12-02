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
    public class MaterialProveedorController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<MaterialProveedorController> _logger;

        public MaterialProveedorController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<MaterialProveedorController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialProveedorRespuesta>>> GetMaterialesProveedor(int idProveedor)
        {
            try
            {
                var ListaMaterialesProveedor = _uow.RepositorioJuncalMaterialProveedor.GetAllByCondition(c => c.IdProveedor == idProveedor && c.Isdeleted == false).ToList();

                if (ListaMaterialesProveedor.Count() > 0)
                {
                    List<MaterialProveedorRespuesta> listaMaterialProveedorRespuesta = _mapper.Map<List<MaterialProveedorRespuesta>>(ListaMaterialesProveedor);
                    return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaMaterialProveedorRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<MaterialProveedorRespuesta>() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de materiales del proveedor");
                return StatusCode(500, "Ocurrió un error al obtener la lista de materiales del proveedor");
            }
        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdMaterialProveedor(int id)
        {
            try
            {
                var materialProveedor = _uow.RepositorioJuncalMaterialProveedor.GetById(id);

                if (materialProveedor is null || materialProveedor.Isdeleted == true)
                {
                    return Ok(new { success = false, message = "No Se Encontró El Material Proveedor", result = new MaterialProveedorRespuesta() == null });
                }

                MaterialProveedorRespuesta materialProvRes = new MaterialProveedorRespuesta();
                _mapper.Map(materialProveedor, materialProvRes);

                return Ok(new { success = true, message = "Material Proveedor Encontrado", result = materialProvRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el material proveedor por ID");
                return StatusCode(500, "Ocurrió un error al obtener el material proveedor por ID");
            }
        }

        [HttpPost]
        public ActionResult CargarMaterialProveedor([FromBody] MaterialProveedorRequerido materialProvReq)
        {
            try
            {
                var materialProveedor = _uow.RepositorioJuncalMaterialProveedor.GetByCondition(c => c.IdProveedor == materialProvReq.IdProveedor && c.IdMaterial == materialProvReq.IdMaterial && c.Isdeleted == false);

                if (materialProveedor is null)
                {
                    JuncalMaterialProveedor materialProveedorNuevo = _mapper.Map<JuncalMaterialProveedor>(materialProvReq);
                    _uow.RepositorioJuncalMaterialProveedor.Insert(materialProveedorNuevo);
                    MaterialProveedorRespuesta materialProveedorRes = new();
                    _mapper.Map(materialProveedorNuevo, materialProveedorRes);

                    return Ok(new { success = true, message = "El Material Proveedor fue Creado Con Exito", result = materialProveedorRes });
                }

                MaterialProveedorRespuesta materialProvExiste = new();
                _mapper.Map(materialProveedor, materialProvExiste);
                return Ok(new { success = false, message = " El Material Proveedor Ya Esta Cargado ", result = materialProvExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar el material proveedor");
                return StatusCode(500, "Ocurrió un error al cargar el material proveedor");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedMaterialProveedor(int id)
        {
            try
            {
                var materialProveedor = _uow.RepositorioJuncalMaterialProveedor.GetById(id);
                if (materialProveedor != null && materialProveedor.Isdeleted == false)
                {
                    materialProveedor.Isdeleted = true;
                    _uow.RepositorioJuncalMaterialProveedor.Update(materialProveedor);
                    MaterialProveedorRespuesta materialProveedorRes = new();
                    _mapper.Map(materialProveedor, materialProveedorRes);
                    return Ok(new { success = true, message = "El Material Proveedor Fue Eliminado ", result = materialProveedorRes });
                }
                return Ok(new { success = false, message = "El Material Proveedor No Fue Encontrado", result = new MaterialProveedorRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el material proveedor");
                return StatusCode(500, "Ocurrió un error al eliminar el material proveedor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaterialProveedor(int id, MaterialProveedorRequerido materialProvEdit)
        {
            try
            {
                var materialProveedor = _uow.RepositorioJuncalMaterialProveedor.GetById(id);
                if (materialProveedor != null && materialProveedor.Isdeleted == false)
                {
                    _mapper.Map(materialProvEdit, materialProveedor);
                    _uow.RepositorioJuncalMaterialProveedor.Update(materialProveedor);
                    MaterialProveedorRespuesta materialProveedorRes = new();
                    _mapper.Map(materialProveedor, materialProveedorRes);
                    return Ok(new { success = true, message = "El Material Proveedor Fue Actualizado", result = materialProveedorRes });
                }
                return Ok(new { success = false, message = "El Material Proveedor No Fue Encontrado ", result = new MaterialProveedorRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el material proveedor");
                return StatusCode(500, "Ocurrió un error al editar el material proveedor");
            }
        }
    }
}
