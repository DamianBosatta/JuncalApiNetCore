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
    public class ProveedorListaPreciosMaterialesController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ProveedorListaPreciosMaterialesController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorListaPrecioMaterialRespuesta>>> GetProveedorListaPrecioMaterial()
        {

            var ListaProveedorListaPrecioMaterial = _uow.RepositorioJuncalProveedorListaPreciosMateriales.GetListaPreciosMateriales();
            
            if (ListaProveedorListaPrecioMaterial.Any())
            {
                
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaProveedorListaPrecioMaterial });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<ProveedorListaPrecioMaterialRespuesta>() == null });


        }
        [Route("ProveedorListaPrecio/{id?}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorListaPrecioMaterialRespuesta>>> GetProveedorListaPrecioMaterial(int idProveedorListaPrecio)
        {

            var ListaProveedorListaPrecioMaterial = _uow.RepositorioJuncalProveedorListaPreciosMateriales.GetListaPreciosMateriales().Where
            (a => a.IdProveedorListaprecios == idProveedorListaPrecio);

            if (ListaProveedorListaPrecioMaterial.Any())
            {
                
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada ", result = ListaProveedorListaPrecioMaterial });

            }

            return Ok(new { success = false, message = "La Lista Esta Vacia ", result = new List<ProveedorListaPrecioMaterialRespuesta>() == null });


        }

        [Route("Buscar/{id?}")]
        [HttpGet]
        public ActionResult GetByIdroveedorListaPrecioMaterial(int id)
        {
            var ProveedorListaPrecioMaterial = _uow.RepositorioJuncalProveedorListaPreciosMateriales.GetById(id);

            if (ProveedorListaPrecioMaterial is null)
            {
                return Ok(new { success = false, message = "No Se Encontro La Lista de precio de materiales del Proveedor", result = new ProveedorListaPrecioMaterialRespuesta() == null });
            }

           var ProveedorListaPrecioMaterialRes =_mapper.Map<ProveedorListaPrecioMaterialRespuesta>(ProveedorListaPrecioMaterial);

            return Ok(new { success = true, message = "Lista de precio de materiales del Proveedor Encontrada", result = ProveedorListaPrecioMaterialRes });

        }
        [HttpPost]
        public ActionResult CargarProveedorListaPrecioMaterial([FromBody] List<ProveedorListaPrecioMaterialRequerido> ProveedorListaPrecioMaterialReq)
        {
            if (ProveedorListaPrecioMaterialReq.Any())
            {


                var ProveedorListaPrecioMaterial = _mapper.Map<List<JuncalProveedorListapreciosMateriale>>(ProveedorListaPrecioMaterialReq);

                _uow.RepositorioJuncalProveedorListaPreciosMateriales.InsertRange(ProveedorListaPrecioMaterial);

                var ProveedorListaPrecioMaterialRes = _mapper.Map<List<ProveedorListaPrecioMaterialRespuesta>>(ProveedorListaPrecioMaterial);

                return Ok(new { success = true, message = " La lista de precio de Materiales de Proveedor Fue Creada Con Exito ", result = ProveedorListaPrecioMaterialRes });
            }
            return Ok(new { success = false, message = "Lista de precio de materiales llego vacia", result = new List<ProveedorListaPrecioMaterialRespuesta>() == null });
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> EditProveedorListaPrecioMaterial(int id, ProveedorListaPrecioMaterialRequerido ProveedorListaPrecioMaterialReq)
        {
            var ProveedorListaPrecioMaterial = _uow.RepositorioJuncalProveedorListaPreciosMateriales.GetById(id);

            if (ProveedorListaPrecioMaterial != null)
            {
                ProveedorListaPrecioMaterial = _mapper.Map<JuncalProveedorListapreciosMateriale>(ProveedorListaPrecioMaterialReq);
                _uow.RepositorioJuncalProveedorListaPreciosMateriales.Update(ProveedorListaPrecioMaterial);

                var ProveedorListaPrecioMaterialRes = _mapper.Map<ProveedorListaPrecioMaterialRespuesta>(ProveedorListaPrecioMaterial);

                return Ok(new { success = true, message = " La Lista de precio de material Del Proveedor Ha Sido Actualizada ", result = ProveedorListaPrecioMaterialRes });
            }

            return Ok(new { success = false, message = "No Se Encontro la lista de precio de material Del Proveedor  ", result = new ProveedorListaPrecioMaterialRespuesta() == null });


        }


    }
}
