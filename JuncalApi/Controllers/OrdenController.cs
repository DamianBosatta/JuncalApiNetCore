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
    public class OrdenController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public OrdenController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenRespuesta>>> GetOrdenes()
        {

            var ListaOrdenes = _uow.RepositorioJuncalOrden.GetAllByCondition(c => c.Isdeleted == false).ToList();

            if (ListaOrdenes.Count() > 0)
            {
                List<OrdenRespuesta> listaOrdenesRespuesta = _mapper.Map<List<OrdenRespuesta>>(ListaOrdenes);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaOrdenesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<OrdenRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarOrdenes([FromBody] OrdenRequerido ordenReq)
        {
            var orden = _uow.RepositorioJuncalOrden.GetByCondition(c => c.IdAceria == ordenReq.IdAceria 
            && c.IdCamion==ordenReq.IdCamion 
            && c.IdContrato== ordenReq.IdContrato 
            && c.IdProveedor== ordenReq.IdProveedor
            && c.Isdeleted == false);

            if (orden is null)
            {
                JuncalOrden ordenNuevo = _mapper.Map<JuncalOrden>(ordenReq);
                _uow.RepositorioJuncalOrden.Insert(ordenNuevo);
                return Ok(new { success = true, message = "La Orden Fue Creada Con Exito", result = ordenNuevo });
            }

            return Ok(new { success = false, message = " La Orden Ya Esta Cargada ", result = orden });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrden(int id)
        {

            var orden = _uow.RepositorioJuncalOrden.GetById(id);
            if (orden != null && orden.Isdeleted == false)
            {
                orden.Isdeleted = true;
                _uow.RepositorioJuncalOrden.Update(orden);

                return Ok(new { success = true, message = "La Orden Fue Eliminada ", result = orden.Isdeleted });

            }
            return Ok(new { success = false, message = "La Orden No Fue Encontrado", result = new JuncalOrden() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrden(int id, OrdenRequerido ordenEdit)
        {
            var orden = _uow.RepositorioJuncalOrden.GetById(id);

            if (orden != null && orden.Isdeleted == false)
            {
                _mapper.Map(ordenEdit, orden);
                _uow.RepositorioJuncalOrden.Update(orden);
                return Ok(new { success = true, message = "La Orden Fue Actualizada", result = orden });
            }

            return Ok(new { success = false, message = "La Orden No Fue Encontrada ", result = new JuncalOrden() == null });


        }
    }
}
