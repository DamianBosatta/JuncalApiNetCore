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
    public class DireccionProveedorController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public DireccionProveedorController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DireccionProveedorRespuesta>>> GetAcerias(int idProveedor)
        {

            var ListaDirecciones = _uow.RepositorioJuncalDireccionProveedor.GetAllByCondition(c => c.Isdelete == false&& c.IdProveedor==idProveedor).ToList();

            if (ListaDirecciones.Count() > 0)
            {
                List<DireccionProveedorRespuesta> listaDireccionesRespuesta = _mapper.Map<List<DireccionProveedorRespuesta>>(ListaDirecciones);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaDireccionesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<DireccionProveedorRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarDireccion([FromBody] DireccionProveedorRequerido direccionReq)
        {
            var DireccionProveedor = _uow.RepositorioJuncalDireccionProveedor.GetByCondition(c => c.Direccion== direccionReq.Direccion && c.Isdelete == false);

            if (DireccionProveedor is null)
            {
                JuncalDireccionProveedor direccionNueva = _mapper.Map<JuncalDireccionProveedor>(direccionReq);

                _uow.RepositorioJuncalDireccionProveedor.Insert(direccionNueva);
                return Ok(new { success = true, message = "La Direccion Fue Creada Con Exito", result = direccionNueva });
            }

            return Ok(new { success = false, message = " La Direccion Ya Existe ", result = DireccionProveedor });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeleteDireccion(int id)
        {

            var direccion = _uow.RepositorioJuncalDireccionProveedor.GetById(id);
          
             if (direccion != null && direccion.Isdelete == false)
            {
                direccion.Isdelete = true;
                _uow.RepositorioJuncalDireccionProveedor.Update(direccion);

                return Ok(new { success = true, message = "La Direccion Fue Eliminada ", result = direccion.Isdelete });


            }


            return Ok(new { success = false, message = "La Direccion no fue encontrada", result = new JuncalDireccionProveedor() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditDireccion(int id, DireccionProveedorRequerido direccionEdit)
        {
            var direccion = _uow.RepositorioJuncalDireccionProveedor.GetById(id);

            if (direccion != null && direccion.Isdelete == false)
            {
                _mapper.Map(direccionEdit, direccion);
                _uow.RepositorioJuncalDireccionProveedor.Update(direccion);
                return Ok(new { success = true, message = "La Direccion fue actualizada", result = direccion });
            }

            return Ok(new { success = false, message = "La Direccion no fue encontrada ", result = new JuncalDireccionProveedor() == null });


        }
    }
}
