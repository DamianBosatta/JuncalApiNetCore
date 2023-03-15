using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ProveedorController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorRespuesta>>> GetProveedores()
        {

            var ListaProveedores = _uow.RepositorioJuncalProveedor.GetAllByCondition(c => c.Isdeleted == false).ToList();

            if (ListaProveedores.Count() > 0)
            {
                List<ProveedorRespuesta> listaProveedorRespuesta = _mapper.Map<List<ProveedorRespuesta>>(ListaProveedores);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaProveedorRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ProveedorRespuesta>() == null });


        }
        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdProveedor(int id)
        {
            var proveedor = _uow.RepositorioJuncalProveedor.GetById(id);

            if (proveedor is null || proveedor.Isdeleted == true)
            {
                return Ok(new { success = false, message = "No Se Encontro El Proveedor", result = new ProveedorRespuesta() == null });
            }
            
            ProveedorRespuesta proveedorRes = new ProveedorRespuesta();

            _mapper.Map(proveedor, proveedorRes);

            return Ok(new { success = true, message = "Proveedor Encontrado", result = proveedorRes });



        }


        [HttpPost]
        public ActionResult CargarProveedor([FromBody] ProveedorRequerido proveedorReq)
        {
            var proveedor = _uow.RepositorioJuncalProveedor.GetByCondition(c => c.Nombre.Equals(proveedorReq.Nombre) && c.Isdeleted == false);

            if (proveedor is null)
            {
                JuncalProveedor proveedorNuevo = _mapper.Map<JuncalProveedor>(proveedorReq);

                _uow.RepositorioJuncalProveedor.Insert(proveedorNuevo);
                return Ok(new { success = true, message = "El Proveedor Fue Creado Con Exito", result = proveedorNuevo });
            }

            return Ok(new { success = false, message = " El Proveedor Ya Existe ", result = proveedor });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedProveedor(int id)
        {

            var proveedor = _uow.RepositorioJuncalProveedor.GetById(id);
            if (proveedor != null && proveedor.Isdeleted == false)
            {
                proveedor.Isdeleted = true;
                _uow.RepositorioJuncalProveedor.Update(proveedor);

                return Ok(new { success = true, message = "El Proveedor Fue Eliminado ", result = proveedor.Isdeleted });


            }


            return Ok(new { success = false, message = " El Proveedor No Fue Encontrado ", result = new JuncalProveedor() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedor(int id, ProveedorRequerido proveedorEdit)
        {
            var proveedor = _uow.RepositorioJuncalProveedor.GetById(id);

            if (proveedor != null && proveedor.Isdeleted == false)
            {
                _mapper.Map(proveedorEdit, proveedor);
                _uow.RepositorioJuncalProveedor.Update(proveedor);
                return Ok(new { success = true, message = "El Proveedor Fue Actualizado", result = proveedor });
            }

            return Ok(new { success = false, message = "El Proveedor No Fue Encontrado ", result = new JuncalProveedor() == null });


        }





    }
}
