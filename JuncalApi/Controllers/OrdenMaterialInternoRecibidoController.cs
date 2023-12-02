using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenMaterialInternoRecibidoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdenMaterialInternoRecibidoController> _logger;

        public OrdenMaterialInternoRecibidoController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<OrdenMaterialInternoRecibidoController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenMaterialInternoRecibidoRespuesta>>> GetOrdenMateriales(int idOrdenInterno)
        {
            try
            {
                var ListaOrdenMateriales = _uow.RepositorioJuncalOrdenMaterialInternoRecibido
                    .GetAllByCondition(c => c.IdOrdenInterno == idOrdenInterno && c.Isdeleted == false)
                    .ToList();

                if (ListaOrdenMateriales.Count() > 0)
                {
                    List<OrdenMaterialInternoRecibidoRespuesta> listaOrdenMaterialRespuesta =
                        _mapper.Map<List<OrdenMaterialInternoRecibidoRespuesta>>(ListaOrdenMateriales);

                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = listaOrdenMaterialRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<OrdenMaterialInternoRecibidoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de órdenes de materiales internos");
                return StatusCode(500, "Ocurrió un error al obtener la lista de órdenes de materiales internos");
            }
        }

        [HttpPost]
        public ActionResult CargarOrdenMaterial([FromBody] List<OrdenMaterialInternoRecibidoRequerido> listOrdenMaterialReq)
        {
            try
            {
                foreach (var item in listOrdenMaterialReq)
                {
                    JuncalOrdenMaterialInternoRecibido ordenMaterialNuevo = _mapper.Map<JuncalOrdenMaterialInternoRecibido>(item);
                    _uow.RepositorioJuncalOrdenMaterialInternoRecibido.Insert(ordenMaterialNuevo);
                }

                if (listOrdenMaterialReq.Count() > 0)
                {
                    return Ok(new { success = true, message = "La Lista de Orden Material fue creada con éxito" });
                }

                return Ok(new { success = false, message = "Ocurrió un error en la carga", result = new List<OrdenMaterialInternoRecibidoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la lista de órdenes de materiales internos");
                return StatusCode(500, "Ocurrió un error al cargar la lista de órdenes de materiales internos");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrdenMaterial(int id)
        {
            try
            {
                var ordenMaterial = _uow.RepositorioJuncalOrdenMaterialInternoRecibido.GetById(id);
                if (ordenMaterial != null && ordenMaterial.Isdeleted == false)
                {
                    ordenMaterial.Isdeleted = true;
                    _uow.RepositorioJuncalOrdenMaterialInternoRecibido.Update(ordenMaterial);
                    OrdenMaterialInternoRecibidoRespuesta ordenMaterialRes = new();
                    _mapper.Map(ordenMaterial, ordenMaterialRes);

                    return Ok(new { success = true, message = "La Orden Material fue eliminada", result = ordenMaterialRes });
                }

                return Ok(new { success = false, message = "La Orden Material no fue encontrada", result = new OrdenMaterialInternoRecibidoRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la orden de material interno");
                return StatusCode(500, "Ocurrió un error al eliminar la orden de material interno");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditOrdenMaterial(List<OrdenMaterialInternoRecibidoRequerido> listOrdenMaterialEdits)
        {
            try
            {
                if (listOrdenMaterialEdits.Count > 0)
                {
                    foreach (var material in listOrdenMaterialEdits)
                    {
                        if (material.IdMaterial == 0)
                        {
                            var materialNuevo = new JuncalOrdenMaterialInternoRecibido();
                            _mapper.Map(material, materialNuevo);
                            _uow.RepositorioJuncalOrdenMaterialInternoRecibido.Insert(materialNuevo);
                        }
                        else
                        {
                            var materialEdit = new JuncalOrdenMaterialInternoRecibido();
                            _mapper.Map(material, materialEdit);
                            _uow.RepositorioJuncalOrdenMaterialInternoRecibido.Update(materialEdit);
                        }
                    }

                    return Ok(new { success = true, message = "La Orden Material fue actualizada" });
                }

                return Ok(new { success = false, message = "La lista a actualizar no contiene datos" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la lista de órdenes de materiales internos");
                return StatusCode(500, "Ocurrió un error al editar la lista de órdenes de materiales internos");
            }
        }
    }
}
