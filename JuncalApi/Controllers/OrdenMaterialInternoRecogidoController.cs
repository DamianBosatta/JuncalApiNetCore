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
    public class OrdenMaterialInternoRecogidoController : ControllerBase
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdenMaterialInternoRecogidoController> _logger;

        public OrdenMaterialInternoRecogidoController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<OrdenMaterialInternoRecogidoController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [Route("listaMateriales")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenMaterialInternoRecogidoRespuesta>>> GetOrdenMaterialesInternoRecogido(int idOrdenInterno)
        {
            try
            {
                var ListaOrdenMateriales = _uow.RepositorioJuncalOrdenMaterialInternoRecogido.listaMaterialesRecogidos(idOrdenInterno);

                if (ListaOrdenMateriales.Count() > 0)
                {
                    List<OrdenMaterialInternoRecogidoRespuesta> listaOrdenMaterialRespuesta = _mapper.Map<List<OrdenMaterialInternoRecogidoRespuesta>>(ListaOrdenMateriales);
                    return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaOrdenMaterialRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<OrdenMaterialInternoRecogidoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de materiales internos recogidos");
                return StatusCode(500, "Ocurrió un error al obtener la lista de materiales internos recogidos");
            }
        }

        [HttpPost]
        public ActionResult CargarOrdenMaterial([FromBody] List<OrdenMaterialInternoRecogidoRequerido> listOrdenMaterialReq)
        {
            try
            {
                foreach (var item in listOrdenMaterialReq)
                {
                    JuncalOrdenMaterialInternoRecogido ordenMaterialNuevo = _mapper.Map<JuncalOrdenMaterialInternoRecogido>(item);
                    _uow.RepositorioJuncalOrdenMaterialInternoRecogido.Insert(ordenMaterialNuevo);
                }

                if (listOrdenMaterialReq.Count > 0)
                {
                    return Ok(new { success = true, message = "La Lista Orden Material Fue Creada Con Éxito" });
                }

                return Ok(new { success = false, message = "Ocurrió un Error en la Carga", result = new List<OrdenMaterialInternoRecogidoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la orden material interno recogido");
                return StatusCode(500, "Ocurrió un error al cargar la orden material interno recogido");
            }
        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrdenMaterial(int id)
        {
            try
            {
                var ordenMaterial = _uow.RepositorioJuncalOrdenMaterialInternoRecogido.GetById(id);
                if (ordenMaterial != null && ordenMaterial.Isdeleted == false)
                {
                    ordenMaterial.Isdeleted = true;
                    _uow.RepositorioJuncalOrdenMaterialInternoRecogido.Update(ordenMaterial);
                    OrdenMaterialInternoRecogidoRespuesta ordenMaterialRes = new();
                    _mapper.Map(ordenMaterial, ordenMaterialRes);

                    return Ok(new { success = true, message = "La Orden Material Fue Eliminada ", result = ordenMaterialRes });
                }

                return Ok(new { success = false, message = "La Orden Material  No Fue Encontrada", result = new OrdenMaterialInternoRecogidoRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la orden material interno recogido");
                return StatusCode(500, "Ocurrió un error al eliminar la orden material interno recogido");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditOrdenMaterial(List<OrdenMaterialInternoRecogidoRequerido> listOrdenMaterialEdits)
        {
            try
            {
                if (listOrdenMaterialEdits.Count > 0)
                {
                    foreach (var material in listOrdenMaterialEdits)
                    {
                        if (material.IdMaterial == 0)
                        {
                            var materialNuevo = new JuncalOrdenMaterialInternoRecogido();
                            _mapper.Map(material, materialNuevo);
                            _uow.RepositorioJuncalOrdenMaterialInternoRecogido.Insert(materialNuevo);
                        }
                        else
                        {
                            var materialEdit = new JuncalOrdenMaterialInternoRecogido();
                            _mapper.Map(material, materialEdit);
                            _uow.RepositorioJuncalOrdenMaterialInternoRecogido.Update(materialEdit);
                        }
                    }

                    return Ok(new { success = true, message = "La Orden Material Fue Actualizada" });
                }

                return Ok(new { success = false, message = "La Lista a Actualizar No Contiene Datos ", result = new OrdenMaterialInternoRecogidoRespuesta() == null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar la orden material interno recogido");
                return StatusCode(500, "Ocurrió un error al editar la orden material interno recogido");
            }
        }
    }
}
