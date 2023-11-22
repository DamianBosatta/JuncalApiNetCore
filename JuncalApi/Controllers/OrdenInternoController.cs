using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos.Item;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JuncalApi.Servicios.Remito;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenInternoController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServiceRemito _serviceRemito;

        public OrdenInternoController(IUnidadDeTrabajo uow, IMapper mapper,IServiceRemito serviceRemito)
        {

            _mapper = mapper;
            _uow = uow;
            _serviceRemito = serviceRemito;
        }


        [Route("TodosLosRemitos/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoRespuesta>>> GetOrdenes()
        {

            var ListaOrdenes = _uow.RepositorioJuncalOrdenInterno.GetAllRemitos().ToList();

            if (ListaOrdenes.Count() > 0)
            {
                List<RemitoRespuesta> listaOrdenesRespuesta = _mapper.Map<List<RemitoRespuesta>>(ListaOrdenes);
                return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = listaOrdenesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoRespuesta>() == null });


        }
        [Route("MaterialesRecogidos/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RemitoRespuesta>>> GetOrdenesRecogidos()
        {

            var ListaOrdenes = _uow.RepositorioJuncalOrdenInterno.GetAllRemitos().Where(a=> a.IdEstado==2).ToList();

            if (ListaOrdenes.Count() > 0)
            {
                List<RemitoRespuesta> listaOrdenesRespuesta = _mapper.Map<List<RemitoRespuesta>>(ListaOrdenes);
                return Ok(new { success = true, message = "Lista Para Ser Utilizada", result = listaOrdenesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<RemitoRespuesta>() == null });


        }



        [HttpPost]
        public ActionResult CargarOrdenes([FromBody] OrdenInternaRequerida ordenReq)
        {
            var orden = _uow.RepositorioJuncalOrdenInterno.GetByCondition(c => c.Remito==ordenReq.Remito);

            if (orden is null)
            {
                JuncalOrdenInterno ordenNuevo = _mapper.Map<JuncalOrdenInterno>(ordenReq);
                _uow.RepositorioJuncalOrdenInterno.Insert(ordenNuevo);
                OrdenInternaRespuesta ordenRes = new();
                _mapper.Map(ordenNuevo, ordenRes);
                return Ok(new { success = true, message = "La Orden Interna Fue Creada Con Exito", result = ordenRes });
            }
            OrdenInternaRespuesta ordenExiste = new();
            _mapper.Map(orden, ordenExiste);
            return Ok(new { success = false, message = " Ya Hay Esta Cargado Ese Numero De Remito ", result = ordenExiste });

        }

        [Route("Facturar/")]
        [HttpPost]
        public ActionResult FacturarOrdenes([FromBody] FacturarRemitoInternoRequerido facturarRequerido)
        {
            if (facturarRequerido == null)
            {
                return Ok(new { success = false, message = "El Objeto Requerido Ingresó Nulo", result = 204 });
            }

            try
            {
                if (facturarRequerido.Cerrar)
                {
                    var ordenInterna = _uow.RepositorioJuncalOrdenInterno.GetById(facturarRequerido.OrdenInterno.Id);

                    if (ordenInterna != null)
                    {
                        ordenInterna.IdEstadoInterno = 2;
                        _uow.RepositorioJuncalOrdenInterno.Update(ordenInterna);
                        return Ok(new { success = true, message = "El remito fue cerrado", result = 200 });
                    }
                }
                else
                {
                    var cuentaCorriente = _serviceRemito.FacturarRemitoInterno(facturarRequerido);

                    if (cuentaCorriente != null)
                    {
                        _uow.RepositorioJuncalCuentasCorriente.Insert(cuentaCorriente);
                        return Ok(new { success = true, message = $"El remito fue facturado por un total de: ${cuentaCorriente.Importe}", result = 200 });
                    }
                }

                return Ok(new { success = false, message = "Operación no válida", result = 204 });
            }
            catch (Exception ex)
            {
                // Manejar excepciones y devolver un mensaje de error apropiado
                return Ok(new { success = false, message = "Ocurrió un error al procesar la solicitud", result = 500 });
            }
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
                OrdenInternaRespuesta ordenRes = new();
                _mapper.Map(orden, ordenRes);

                return Ok(new { success = true, message = "La Orden Fue Eliminada ", result = ordenRes });

            }
            return Ok(new { success = false, message = "La Orden No Fue Encontrado", result = new OrdenInternaRespuesta() == null });

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

            return Ok(new { success = false, message = "La Orden No Fue Encontrada ", result = new OrdenInternaRespuesta() == null });


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRemitoInternoById(int id)
        {

            ItemRemitoInterno orden = _uow.RepositorioJuncalOrdenInterno.GetRemito(id);

            if (orden != null)
            {
                RemitoRespuesta response = new RemitoRespuesta();
                _mapper.Map(orden, response);

                return Ok(new { success = true, message = "Response Confirmado", result = response });

            }


            return Ok(new { success = false, message = "No Se Encontro Remito", result = new RemitoRespuesta() });

        }
    }
}
