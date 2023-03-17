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
    public class MaterialController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public MaterialController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialRespuesta>>> GetMateriales()
        {

            var ListaMateriales = _uow.RepositorioJuncalMaterial.GetAllByCondition(c => c.Isdeleted == false).ToList();

            if (ListaMateriales.Count() > 0)
            {
                List<MaterialRespuesta> listaMaterialRespuesta = _mapper.Map<List<MaterialRespuesta>>(ListaMateriales);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaMaterialRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<MaterialRespuesta>() == null });


        }


        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdMaterial(int id)
        {
            var material = _uow.RepositorioJuncalMaterial.GetById(id);

            if (material is null || material.Isdeleted==true)
            {
                return Ok(new { success = false, message = "No Se Encontro El Material", result = new MaterialRespuesta() == null });
            }
            MaterialRespuesta materialRes = new MaterialRespuesta();

            _mapper.Map(material,materialRes);

            return Ok(new { success = true, message = "Aceria Encontrada", result = materialRes });

        }


        [HttpPost]
        public ActionResult CargarMaterial([FromBody] MaterialRequerido materialReq)
        {
            var material = _uow.RepositorioJuncalMaterial.GetByCondition(c => c.Nombre.Equals(materialReq.Nombre)&& c.Isdeleted==false);

            if (material is null)
            {
                JuncalMaterial materialNuevo = _mapper.Map<JuncalMaterial>(materialReq);
                _uow.RepositorioJuncalMaterial.Insert(materialNuevo);
                return Ok(new { success = true, message = "El Material fue Creado Con Exito", result = materialNuevo });
            }
           
            return Ok(new { success = false, message = " El Material Ya Esta Cargado ", result = material });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedMaterial(int id)
        {

            var  material = _uow.RepositorioJuncalMaterial.GetById(id);
            if (material != null && material.Isdeleted == false)
            {
                material.Isdeleted = true;
                _uow.RepositorioJuncalMaterial.Update(material);

                return Ok(new { success = true, message = "El Material Fue Eliminado ", result = material.Isdeleted });

            }
            return Ok(new { success = false, message = "El Material No Fue encontrado", result = new JuncalMaterial() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaterial(int id, MaterialRequerido materialEdit)
        {
            var material = _uow.RepositorioJuncalMaterial.GetById(id);

            if (material != null && material.Isdeleted == false)
            {
                material = _mapper.Map(materialEdit,material);
                _uow.RepositorioJuncalMaterial.Update(material);
                return Ok(new { success = true, message = "El Material fue actualizado", result = material });
            }

            return Ok(new { success = false, message = "El Material no fue encontrado ", result = new JuncalMaterial() == null });


        }

    }
}
