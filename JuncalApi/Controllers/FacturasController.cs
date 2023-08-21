using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public FacturasController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaRespuesta>>> GetFacturas()
        {

            var ListaFacturas = _uow.RepositorioJuncalFactura.JuncalFacturaList();

            if (ListaFacturas.Count() > 0)
            {
               
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = ListaFacturas });

            }
           
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<FacturaRespuesta>() == null });


        }


        [HttpGet("{numFactura}")]
        public ActionResult GetFactura(string numFactura)
        {
            var factura = _uow.RepositorioJuncalFactura.getByNum(numFactura);

            if (factura != null)
            {
                return Ok(new { success = true, message = "Factura encontrada", result = factura });
            }

            return Ok(new { success = false, message = "La factura no fue encontrada ", result = new FacturaRespuesta() == null });


        }

    }
}
