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

                SucursalRespuesta sucursalRes = new ();
                _mapper.Map(sucursalNueva, sucursalRes);
                return Ok(new { success = true, message = "La Sucursal Fue  Creada Con Exito", result = sucursalRes });
            }
           
            SucursalRespuesta sucursalExiste = new();
            _mapper.Map(sucursal, sucursalExiste);
            return Ok(new { success = false, message = " La Sucursal Ya Existe ", result = sucursalExiste });

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
                SucursalRespuesta sucursalRes = new();
                _mapper.Map(sucursal, sucursalRes);

                return Ok(new { success = true, message = "La Sucursal Fue Eliminada ", result = sucursalRes});

            }

            return Ok(new { success = false, message = "La Sucursal No Se Encontro ", result = new SucursalRespuesta() == null });

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditSucursal(int id, SucursalRequerida sucursalEdit)
        {
            var sucursal = _uow.RepositorioJuncalSucursal.GetById(id);

            if (sucursal != null)
            {
                _mapper.Map(sucursalEdit, sucursal);
                _uow.RepositorioJuncalSucursal.Update(sucursal);
                SucursalRespuesta sucursalRes = new();
                _mapper.Map(sucursal, sucursalRes);
                return Ok(new { success = true, message = "La Sucursal Fue Actualizada ", result = sucursalRes });
            }

            return Ok(new { success = false, message = "La Sucursal No Fue Encontrada ", result = new SucursalRespuesta() == null });


        }

    }
}
