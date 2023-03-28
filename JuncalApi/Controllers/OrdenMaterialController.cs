using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenMaterialController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public OrdenMaterialController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenMaterialRespuesta>>> GetOrdenMateriales(int idOrden)
        {

            var ListaOrdenMateriales = _uow.RepositorioJuncalOrdenMarterial.GetAllByCondition(c => c.IdOrden == idOrden && c.Isdeleted == false).ToList();

            if (ListaOrdenMateriales.Count() > 0)
            {
                List<OrdenMaterialRespuesta> listaOrdenMaterialRespuesta = _mapper.Map<List<OrdenMaterialRespuesta>>(ListaOrdenMateriales);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaOrdenMaterialRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<OrdenMaterialRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarOrdenMaterial([FromBody] List<OrdenMaterialRequerido> listOrdenMaterialReq)
        {
            JuncalOrdenMarterial ordenMaterialNuevo = new();
            foreach (var item in listOrdenMaterialReq)
            {
               ordenMaterialNuevo= _mapper.Map<JuncalOrdenMarterial>(item);
                _uow.RepositorioJuncalOrdenMarterial.Insert(ordenMaterialNuevo);

            }            
            
            if(listOrdenMaterialReq.Count() > 0)
            {
                return Ok(new { success = true, message = "La Lista Orden Material Fue Creado Con Exito", result = Ok() }) ;
            }


            return Ok(new { success = false, message = " Ocurrio Un Error En La Carga ", result =  new List<OrdenMaterialRespuesta>()==null });


        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrdenMaterial(int id)
        {

            var ordenMaterial = _uow.RepositorioJuncalOrdenMarterial.GetById(id);
            if (ordenMaterial != null && ordenMaterial.Isdeleted == false)
            {
                ordenMaterial.Isdeleted = true;
                _uow.RepositorioJuncalOrdenMarterial.Update(ordenMaterial);
                OrdenMaterialRespuesta ordenMaterialRes = new();
                _mapper.Map(ordenMaterial, ordenMaterialRes);

                return Ok(new { success = true, message = "La Orden Material Fue Eliminada ", result = ordenMaterialRes });

            }
            return Ok(new { success = false, message = "La Orden Material  No Fue Encontrada", result = new OrdenMaterialRespuesta() == null });

        }

        [HttpPut]
        public async Task<IActionResult> EditOrdenMaterial(List<OrdenMaterialRequerido> listOrdenMaterialEdits)
        {
            if (listOrdenMaterialEdits.Count > 0)
            {
                foreach (var material in listOrdenMaterialEdits)
                {
                    if (material.Id == 0)
                    {
                        var materialNuevo = new JuncalOrdenMarterial();
                        _mapper.Map(material, materialNuevo);
                        _uow.RepositorioJuncalOrdenMarterial.Insert(materialNuevo);

                    }
                    else
                    {
                        var materialEdit = new JuncalOrdenMarterial();
                        _mapper.Map(material, materialEdit);
                        _uow.RepositorioJuncalOrdenMarterial.Update(materialEdit);

                    }


                }

                return Ok(new { success = true, message = "La Orden Material Fue Actualizada" });


            }


            return Ok(new { success = false, message = "La Lista a Actualizar No Contiene Datos ", result = new OrdenMaterialRespuesta() == null });




        }

    }
}
