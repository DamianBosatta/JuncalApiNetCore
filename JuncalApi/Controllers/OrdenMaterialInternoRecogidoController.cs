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
    public class OrdenMaterialInternoRecogidoController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public OrdenMaterialInternoRecogidoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }
        [Route("listaMateriales")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenMaterialInternoRecogidoRespuesta>>> GetOrdenMaterialesInternoRecogido(int idOrdenInterno)
        {

            var ListaOrdenMateriales = _uow.RepositorioJuncalOrdenMaterialInternoRecogido.listaMaterialesRecogidos(idOrdenInterno);

            if (ListaOrdenMateriales.Count() > 0)
            {
                List<OrdenMaterialInternoRecogidoRespuesta> listaOrdenMaterialRespuesta = _mapper.Map<List<OrdenMaterialInternoRecogidoRespuesta>>(ListaOrdenMateriales);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaOrdenMaterialRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<OrdenMaterialInternoRecogidoRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarOrdenMaterial([FromBody] List<OrdenMaterialInternoRecogidoRequerido> listOrdenMaterialReq)
        {
            JuncalOrdenMaterialInternoRecogido ordenMaterialNuevo = new();
            foreach (var item in listOrdenMaterialReq)
            {
                ordenMaterialNuevo = _mapper.Map<JuncalOrdenMaterialInternoRecogido>(item);
                _uow.RepositorioJuncalOrdenMaterialInternoRecogido.Insert(ordenMaterialNuevo);

            }

            if (listOrdenMaterialReq.Count() > 0)
            {
                return Ok(new { success = true, message = "La Lista Orden Material Fue Creado Con Exito", result = Ok() });
            }


            return Ok(new { success = false, message = " Ocurrio Un Error En La Carga ", result = new List<OrdenMaterialInternoRecogidoRespuesta>() == null });


        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrdenMaterial(int id)
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

        [HttpPut]
        public async Task<IActionResult> EditOrdenMaterial(List<OrdenMaterialInternoRecogidoRequerido> listOrdenMaterialEdits)
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

    }
}
