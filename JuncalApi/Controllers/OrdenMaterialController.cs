﻿using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JuncalApi.Controllers
{
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

                return Ok(new { success = true, message = "La Orden Material Fue Eliminada ", result = ordenMaterial.Isdeleted });

            }
            return Ok(new { success = false, message = "La Orden Material  No Fue Encontrada", result = new JuncalOrdenMarterial() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrdenMaterial(int idOrden,List<OrdenMaterialRequerido> listOrdenMaterialEdits)
        {
            if (listOrdenMaterialEdits.Count > 0)
            {

                var listaEditada = new List<JuncalOrdenMarterial>();

                var materialNuevo = new JuncalOrdenMarterial();

                foreach (var material in listOrdenMaterialEdits)
                {
                    if (material.Id == 0 && material.IdOrden == idOrden)
                    {
                        _mapper.Map(material, materialNuevo);
                        _uow.RepositorioJuncalOrdenMarterial.Insert(materialNuevo);
                        listaEditada.Add(materialNuevo);

                    }
                    else if (material.IdOrden == idOrden)
                    {
                        _mapper.Map(material, materialNuevo);
                        listaEditada.Add(materialNuevo);

                    }


                }

                foreach (var material in listaEditada)
                {
                    _uow.RepositorioJuncalOrdenMarterial.Update(material);

                }

                return Ok(new { success = true, message = "La Orden Material Fue Actualizada", result = listaEditada });


            }


            return Ok(new { success = false, message = "La Lista a Actualizar No Contiene Datos ", result = new JuncalOrdenMarterial() == null });




        }

    }
}
