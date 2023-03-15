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
    public class SucursalController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public SucursalController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SucursalRespuesta>>> GetSucursal()
        {

            var ListaSucursales = _uow.RepositorioJuncalSucursal.GetAll().ToList();

            if (ListaSucursales.Count() > 0)
            {
                List<SucursalRespuesta> listaSucursalesRespuesta = _mapper.Map<List<SucursalRespuesta>>(ListaSucursales);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaSucursalesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<SucursalRespuesta>() == null });


        }



        [HttpPost]
        public ActionResult CargarSucursal([FromBody] SucursalRequerida sucursalReq)
        {
            var sucursal = _uow.RepositorioJuncalSucursal.GetByCondition(c => c.Numero.Equals(sucursalReq.Numero));

            if (sucursal is null)
            {
                JuncalSucursal sucursalNueva = _mapper.Map<JuncalSucursal>(sucursalReq);

                _uow.RepositorioJuncalSucursal.Insert(sucursalNueva);
                return Ok(new { success = true, message = "La Sucursal Fue  Creada Con Exito", result = sucursalNueva });
            }

            else return Ok(new { success = false, message = " La Sucursal Ya Existe ", result = sucursal });

        }

        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedSucursal(int id)
        {

            var sucursal = _uow.RepositorioJuncalEstado.GetById(id);
            if (sucursal != null && sucursal.Isdeleted == false)
            {
                sucursal.Isdeleted = true;
                _uow.RepositorioJuncalEstado.Update(sucursal);

                return Ok(new { success = true, message = "La Sucursal Fue Eliminada ", result = sucursal.Isdeleted });

            }

            return Ok(new { success = false, message = "La Sucursal No Se Encontro ", result = new JuncalSucursal() == null });

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditSucursal(int id, SucursalRequerida sucursalEdit)
        {
            var sucursal = _uow.RepositorioJuncalSucursal.GetById(id);

            if (sucursal != null)
            {
                _mapper.Map(sucursalEdit, sucursal);
                _uow.RepositorioJuncalSucursal.Update(sucursal);
                return Ok(new { success = true, message = "La Sucursal Fue Actualizada ", result = sucursal });
            }

            return Ok(new { success = false, message = "La Sucursal No Fue Encontrada ", result = new JuncalSucursal() == null });


        }

    }
}
