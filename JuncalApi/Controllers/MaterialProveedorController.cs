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
    public class MaterialProveedorController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public MaterialProveedorController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialProveedorRespuesta>>> GetMaterialesProveedor(int idProveedor)
        {

            var ListaMaterialesProveedor = _uow.RepositorioJuncalMaterialProveedor.GetAllByCondition(c => c.IdProveedor == idProveedor && c.Isdeleted == false).ToList();

            if (ListaMaterialesProveedor.Count() > 0)
            {
                List<MaterialProveedorRespuesta> listaMaterialProveedorRespuesta = _mapper.Map<List<MaterialProveedorRespuesta>>(ListaMaterialesProveedor);
                return Ok(new { success = true, message = "La Lista Puede Ser Utilizada", result = listaMaterialProveedorRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<MaterialProveedorRespuesta>() == null });


        }


        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdMaterialProveedor(int id)
        {
            var materialProveedor = _uow.RepositorioJuncalMaterialProveedor.GetById(id);

            if (materialProveedor is null || materialProveedor.Isdeleted == true)
            {
                return Ok(new { success = false, message = "No Se Encontro El Material Proveedor", result = new MaterialProveedorRespuesta() == null });
            }
            MaterialProveedorRespuesta materialProvRes = new MaterialProveedorRespuesta();

            _mapper.Map(materialProveedor, materialProvRes);

            return Ok(new { success = true, message = "Material Proveedor Encontrado", result = materialProvRes });

        }


        [HttpPost]
        public ActionResult CargarMaterialProveedor([FromBody] MaterialProveedorRequerido materialProvReq)
        {
            var materialProveedor = _uow.RepositorioJuncalMaterialProveedor.GetByCondition(c => c.IdProveedor == materialProvReq.IdProveedor
            && c.IdMaterial == materialProvReq.IdMaterial
            && c.Isdeleted == false);

            if (materialProveedor is null)
            {
                JuncalMaterialProveedor materialProveedorNuevo = _mapper.Map<JuncalMaterialProveedor>(materialProvReq);
                _uow.RepositorioJuncalMaterialProveedor.Insert(materialProveedorNuevo);
                return Ok(new { success = true, message = "El Material Proveedor fue Creado Con Exito", result = materialProveedorNuevo });
            }

            return Ok(new { success = false, message = " El Material Proveedor Ya Esta Cargado ", result = materialProveedor });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedMaterialProveedor(int id)
        {

            var materialProveedor = _uow.RepositorioJuncalMaterialProveedor.GetById(id);
            if (materialProveedor != null && materialProveedor.Isdeleted == false)
            {
                materialProveedor.Isdeleted = true;
                _uow.RepositorioJuncalMaterialProveedor.Update(materialProveedor);

                return Ok(new { success = true, message = "El Material Proveedor Fue Eliminado ", result = materialProveedor.Isdeleted });

            }
            return Ok(new { success = false, message = "El Material Proveedor No Fue Encontrado", result = new JuncalMaterialProveedor() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaterialProveedor(int id, MaterialProveedorRequerido materialProvEdit)
        {
            var materialProveedor = _uow.RepositorioJuncalMaterialProveedor.GetById(id);

            if (materialProveedor != null && materialProveedor.Isdeleted == false)
            {
                _mapper.Map(materialProvEdit, materialProveedor);
                _uow.RepositorioJuncalMaterialProveedor.Update(materialProveedor);
                return Ok(new { success = true, message = "El Material Proveedor Fue Actualizado", result = materialProveedor });
            }

            return Ok(new { success = false, message = "El Material Proveedor No Fue Encontrado ", result = new JuncalMaterialProveedor() == null });


        }

    }
}
