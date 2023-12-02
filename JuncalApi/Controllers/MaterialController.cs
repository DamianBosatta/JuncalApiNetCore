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
    public class MaterialController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<MaterialController> _logger;

        public MaterialController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<MaterialController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialRespuesta>>> GetMateriales()
        {
            try
            {
                var ListaMateriales = _uow.RepositorioJuncalMaterial.GetAllByCondition(c => c.Isdeleted == false).ToList();

                if (ListaMateriales.Count() > 0)
                {
                    List<MaterialRespuesta> listaMaterialRespuesta = _mapper.Map<List<MaterialRespuesta>>(ListaMateriales);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = listaMaterialRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<MaterialRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de materiales");
                return StatusCode(500, "Ocurrió un error al obtener la lista de materiales");
            }
        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdMaterial(int id)
        {
            try
            {
                var material = _uow.RepositorioJuncalMaterial.GetById(id);

                if (material is null || material.Isdeleted == true)
                {
                    return Ok(new { success = false, message = "No Se Encontró El Material", result = new MaterialRespuesta() });
                }

                MaterialRespuesta materialRes = new MaterialRespuesta();
                _mapper.Map(material, materialRes);

                return Ok(new { success = true, message = "Material Encontrado", result = materialRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el material por ID");
                return StatusCode(500, "Ocurrió un error al obtener el material por ID");
            }
        }

        [HttpPost]
        public ActionResult CargarMaterial([FromBody] MaterialRequerido materialReq)
        {
            try
            {
                var material = _uow.RepositorioJuncalMaterial.GetByCondition(c => c.Nombre.Equals(materialReq.Nombre) && c.Isdeleted == false);

                if (material is null)
                {
                    JuncalMaterial materialNuevo = _mapper.Map<JuncalMaterial>(materialReq);
                    _uow.RepositorioJuncalMaterial.Insert(materialNuevo);

                    MaterialRespuesta materialRes = new MaterialRespuesta();
                    _mapper.Map(materialNuevo, materialRes);

                    return Ok(new { success = true, message = "El Material fue Creado Con Éxito", result = materialRes });
                }

                MaterialRespuesta materialExiste = new MaterialRespuesta();
                _mapper.Map(material, materialExiste);

                return Ok(new { success = false, message = "El Material Ya Está Cargado", result = materialExiste });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar el material");
                return StatusCode(500, "Ocurrió un error al cargar el material");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedMaterial(int id)
        {
            try
            {
                var material = _uow.RepositorioJuncalMaterial.GetById(id);

                if (material != null && material.Isdeleted == false)
                {
                    material.Isdeleted = true;
                    _uow.RepositorioJuncalMaterial.Update(material);

                    MaterialRespuesta materialRes = new MaterialRespuesta();
                    _mapper.Map(material, materialRes);

                    return Ok(new { success = true, message = "El Material Fue Eliminado", result = materialRes });
                }

                return Ok(new { success = false, message = "El Material No Fue Encontrado", result = new MaterialRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el material");
                return StatusCode(500, "Ocurrió un error al eliminar el material");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaterial(int id, MaterialRequerido materialEdit)
        {
            try
            {
                var material = _uow.RepositorioJuncalMaterial.GetById(id);

                if (material != null && material.Isdeleted == false)
                {
                    _mapper.Map(materialEdit, material);
                    _uow.RepositorioJuncalMaterial.Update(material);

                    MaterialRespuesta materialRes = new MaterialRespuesta();
                    _mapper.Map(material, materialRes);

                    return Ok(new { success = true, message = "El Material fue actualizado", result = materialRes });
                }

                return Ok(new { success = false, message = "El Material no fue encontrado", result = new MaterialRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar el material");
                return StatusCode(500, "Ocurrió un error al editar el material");
            }
        }
    }
}
