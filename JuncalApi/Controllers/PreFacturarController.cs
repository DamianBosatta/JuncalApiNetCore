using AutoMapper;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PreFacturarController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public PreFacturarController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreFacturadoRespuesta>>> GetPreFacturado()
        {

            var ListaPreFacturado = _uow.RepositorioJuncalPreFactura.GetAllByCondition(c => c.IsDelete == false &&
            c.Facturado==false).ToList();

            if (ListaPreFacturado.Count() > 0)
            {
                List<PreFacturadoRespuesta> listaPreFacturadoRespuesta = _mapper.Map<List<PreFacturadoRespuesta>>(ListaPreFacturado);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaPreFacturadoRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<PreFacturadoRespuesta>() == null });


        }


        [HttpGet]
        public async Task<ActionResult<IGrouping<int,ItemFacturado>>> GetAgrupacionFacturado(List<JuncalPreFacturar> listaPreFacturado)
        {

            var ListaAgrupada = _uow.RepositorioJuncalPreFactura.GetAgrupamientoFacturacion(listaPreFacturado);

            if (ListaAgrupada.Count() > 0)
            {
              
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = ListaAgrupada });

            }
           
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = ListaAgrupada});


        }



    }
}
