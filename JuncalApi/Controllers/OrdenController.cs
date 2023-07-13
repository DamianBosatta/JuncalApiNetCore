using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Servicios.Remito;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServiceRemito _serviceRemito; 

        public OrdenController(IUnidadDeTrabajo uow, IMapper mapper,IServiceRemito serviceRemito)
        {

            _mapper = mapper;
            _uow = uow;
            _serviceRemito = serviceRemito; 
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoRespuesta>>> GetOrdenes()
        {

            var ListaOrdenes = _uow.RepositorioJuncalOrden.GetRemito(0);

            if (ListaOrdenes.Count() > 0)
            {
               
                return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = ListaOrdenes });

            }
           
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = ListaOrdenes= new List<RemitoRespuesta>()});


        }



        
        [HttpGet]
        [Route("api/pendientes")]
        public async Task<ActionResult<IEnumerable<RemitosPendientesRespuesta>>> GetPendientes()
        {

            var ListaOrdenesPendientes = _uow.RepositorioJuncalOrden.GetRemitosPendientes();

            if (ListaOrdenesPendientes.Count() > 0)
            {

                return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = ListaOrdenesPendientes });

            }

            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = ListaOrdenesPendientes = new List<RemitosPendientesRespuesta>() });


        }


        [HttpPost]
        public ActionResult CargarOrdenes([FromBody] OrdenRequerido ordenReq)
        {
            var orden = _uow.RepositorioJuncalOrden.GetByCondition(c => c.Remito==ordenReq.Remito);

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
            return Ok(new { success = false, message = " Ya esta Cargado Ese Numero De Remito ", result = ordenExiste });

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
     
        [HttpGet("api/remitos/{idOrden}")]
        public async Task<IActionResult> GetRemitoById(int idOrden)
        {
            var orden = await Task.Run(() => _serviceRemito.GetRemitos(idOrden));

            if (orden != null)
            {
                return Ok(new { success = true, message = "Response Confirmado", result = orden });
            }

            return Ok(new { success = false, message = "No Se Encontro Remito", result = orden });
        }


    }
}
