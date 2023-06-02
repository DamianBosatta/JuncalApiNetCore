using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace JuncalApi.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<RemitoResponse>>> GetOrdenes()
        {

            var ListaOrdenes = _uow.RepositorioJuncalOrden.GetAllRemitos();

            if (ListaOrdenes.Count() > 0)
            {
                List<RemitoResponse> listaOrdenesRespuesta = _mapper.Map<List<RemitoResponse>>(ListaOrdenes);
                return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = listaOrdenesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoResponse>() == null });


        }


        [HttpPost]
        public ActionResult CargarOrdenes([FromBody] OrdenRequerido ordenReq)
        {
            var orden = _uow.RepositorioJuncalOrden.GetByCondition(c => c.IdAceria == ordenReq.IdAceria
            && c.IdCamion == ordenReq.IdCamion
            && c.IdContrato == ordenReq.IdContrato
            && c.IdProveedor == ordenReq.IdProveedor
            && c.Isdeleted == false);

            if (orden is null)
            {
                JuncalOrden ordenNuevo = _mapper.Map<JuncalOrden>(ordenReq);
                _uow.RepositorioJuncalOrden.Insert(ordenNuevo);
                OrdenRespuesta ordenRes = new();
                _mapper.Map(ordenNuevo, ordenRes);
                return Ok(new { success = true, message = "La Orden Fue Creada Con Exito", result = ordenRes });
            }
            OrdenRespuesta ordenExiste = new();
            _mapper.Map(orden, ordenExiste);
            return Ok(new { success = false, message = " La Orden Ya Esta Cargada ", result = ordenExiste });

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
                OrdenRespuesta ordenRes = new();
                _mapper.Map(orden, ordenRes);

                return Ok(new { success = true, message = "La Orden Fue Eliminada ", result = ordenRes });

            }
            return Ok(new { success = false, message = "La Orden No Fue Encontrado", result = new OrdenRespuesta() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrden(int id, OrdenRequerido ordenEdit)
        {
            var orden = _uow.RepositorioJuncalOrden.GetById(id);

            if (orden != null && orden.Isdeleted == false)
            {
                _mapper.Map(ordenEdit, orden);
                _uow.RepositorioJuncalOrden.Update(orden);
                OrdenRespuesta ordenRes = new();
                _mapper.Map(orden, ordenRes);
                return Ok(new { success = true, message = "La Orden Fue Actualizada", result = ordenRes });
            }

            return Ok(new { success = false, message = "La Orden No Fue Encontrada ", result = new OrdenRespuesta() == null });


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRemitoById(int id)
        {

            ItemRemito orden = _uow.RepositorioJuncalOrden.GetRemito(id);

            if (orden != null)
            {
                RemitoResponse response = new RemitoResponse();
                _mapper.Map(orden,response);

                return Ok(new { success = true, message = "Response Confirmado", result = response });

            }


            return Ok(new { success = false, message = "No Se Encontro Remito", result = new RemitoResponse() });

        }


    }
}
