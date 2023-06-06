using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos.Item;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenInternoController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public OrdenInternoController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [Route("TodosLosRemitos/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoResponse>>> GetOrdenes()
        {

            var ListaOrdenes = _uow.RepositorioJuncalOrdenInterno.GetAllRemitos().ToList();

            if (ListaOrdenes.Count() > 0)
            {
                List<RemitoResponse> listaOrdenesRespuesta = _mapper.Map<List<RemitoResponse>>(ListaOrdenes);
                return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = listaOrdenesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoResponse>() == null });


        }
        [Route("MaterialesRecogidos/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoResponse>>> GetOrdenesRecogidos()
        {

            var ListaOrdenes = _uow.RepositorioJuncalOrdenInterno.GetAllRemitos().Where(a=> a.IdEstado==2).ToList();

            if (ListaOrdenes.Count() > 0)
            {
                List<RemitoResponse> listaOrdenesRespuesta = _mapper.Map<List<RemitoResponse>>(ListaOrdenes);
                return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = listaOrdenesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoResponse>() == null });


        }



        [HttpPost]
        public ActionResult CargarOrdenes([FromBody] OrdenInternaRequerida ordenReq)
        {
            var orden = _uow.RepositorioJuncalOrdenInterno.GetByCondition(c => c.Remito==ordenReq.Remito);

            if (orden is null)
            {
                JuncalOrdenInterno ordenNuevo = _mapper.Map<JuncalOrdenInterno>(ordenReq);
                _uow.RepositorioJuncalOrdenInterno.Insert(ordenNuevo);
                OrdenInternaResponse ordenRes = new();
                _mapper.Map(ordenNuevo, ordenRes);
                return Ok(new { success = true, message = "La Orden Interna Fue Creada Con Exito", result = ordenRes });
            }
            OrdenInternaResponse ordenExiste = new();
            _mapper.Map(orden, ordenExiste);
            return Ok(new { success = false, message = " Ya Hay Esta Cargado Ese Numero De Remito ", result = ordenExiste });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrdenInterna(int id)
        {

            var orden = _uow.RepositorioJuncalOrdenInterno.GetById(id);
            if (orden != null && orden.Isdeleted == false)
            {
                orden.Isdeleted = true;
                _uow.RepositorioJuncalOrdenInterno.Update(orden);
                OrdenInternaResponse ordenRes = new();
                _mapper.Map(orden, ordenRes);

                return Ok(new { success = true, message = "La Orden Fue Eliminada ", result = ordenRes });

            }
            return Ok(new { success = false, message = "La Orden No Fue Encontrado", result = new OrdenInternaResponse() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrden(int id, OrdenRequerido ordenEdit)
        {
            var orden = _uow.RepositorioJuncalOrdenInterno.GetById(id);

            if (orden != null && orden.Isdeleted == false)
            {
                _mapper.Map(ordenEdit, orden);
                _uow.RepositorioJuncalOrdenInterno.Update(orden);
                OrdenRespuesta ordenRes = new();
                _mapper.Map(orden, ordenRes);
                return Ok(new { success = true, message = "La Orden Fue Actualizada", result = ordenRes });
            }

            return Ok(new { success = false, message = "La Orden No Fue Encontrada ", result = new OrdenInternaResponse() == null });


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRemitoInternoById(int id)
        {

            ItemRemitoInterno orden = _uow.RepositorioJuncalOrdenInterno.GetRemito(id);

            if (orden != null)
            {
                RemitoResponse response = new RemitoResponse();
                _mapper.Map(orden, response);

                return Ok(new { success = true, message = "Response Confirmado", result = response });

            }


            return Ok(new { success = false, message = "No Se Encontro Remito", result = new RemitoResponse() });

        }
    }
}
