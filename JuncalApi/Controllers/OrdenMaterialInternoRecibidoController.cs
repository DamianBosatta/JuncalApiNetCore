using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenMaterialInternoRecibidoController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public OrdenMaterialInternoRecibidoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenMaterialInternoRecibidoRespuesta>>> GetOrdenMateriales(int idOrdenInterno)
        {

            var ListaOrdenMateriales = _uow.RepositorioJuncalOrdenMaterialInternoRecibido.GetAllByCondition(c => c.IdOrdenInterno == idOrdenInterno && c.Isdeleted == false).ToList();

            if (ListaOrdenMateriales.Count() > 0)
            {
                List<OrdenMaterialInternoRecibidoRespuesta> listaOrdenMaterialRespuesta = _mapper.Map<List<OrdenMaterialInternoRecibidoRespuesta>>(ListaOrdenMateriales);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaOrdenMaterialRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<OrdenMaterialInternoRecibidoRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarOrdenMaterial([FromBody] List<OrdenMaterialInternoRecibidoRequerido> listOrdenMaterialReq)
        {
            
            foreach (var item in listOrdenMaterialReq)
            {
                JuncalOrdenMaterialInternoRecibido ordenMaterialNuevo = new();
                ordenMaterialNuevo = _mapper.Map<JuncalOrdenMaterialInternoRecibido>(item);
                _uow.RepositorioJuncalOrdenMaterialInternoRecibido.Insert(ordenMaterialNuevo);

            }

            if (listOrdenMaterialReq.Count() > 0)
            {
                return Ok(new { success = true, message = "La Lista Orden Material Fue Creado Con Exito", result = Ok() });
            }


            return Ok(new { success = false, message = " Ocurrio Un Error En La Carga ", result = new List<OrdenMaterialInternoRecibidoRespuesta>() == null });


        }




        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrdenMaterial(int id)
        {

            var ordenMaterial = _uow.RepositorioJuncalOrdenMaterialInternoRecibido.GetById(id);
            if (ordenMaterial != null && ordenMaterial.Isdeleted == false)
            {
                ordenMaterial.Isdeleted = true;
                _uow.RepositorioJuncalOrdenMaterialInternoRecibido.Update(ordenMaterial);
                OrdenMaterialInternoRecibidoRespuesta ordenMaterialRes = new();
                _mapper.Map(ordenMaterial, ordenMaterialRes);

                return Ok(new { success = true, message = "La Orden Material Fue Eliminada ", result = ordenMaterialRes });

            }
            return Ok(new { success = false, message = "La Orden Material  No Fue Encontrada", result = new OrdenMaterialInternoRecibidoRespuesta() == null });

        }

        [HttpPut]
        public async Task<IActionResult> EditOrdenMaterial(List<OrdenMaterialInternoRecibidoRequerido> listOrdenMaterialEdits)
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

                return Ok(new { success = true, message = "La Orden Material Fue Actualizada" });


            }


            return Ok(new { success = false, message = "La Lista a Actualizar No Contiene Datos ", result = new OrdenMaterialInternoRecibidoRespuesta() == null });




        }
    }
}

