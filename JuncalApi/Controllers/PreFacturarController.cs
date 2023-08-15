using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
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

            var ListaPreFacturado = _uow.RepositorioJuncalPreFactura.GetAllPreFacturar();

            if (ListaPreFacturado.Count() > 0)
            {
                List<PreFacturadoRespuesta> listaPreFacturadoRespuesta = _mapper.Map<List<PreFacturadoRespuesta>>(ListaPreFacturado);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaPreFacturadoRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<PreFacturadoRespuesta>() == null });


        }
        
  

        [HttpPost]
        public ActionResult CargarPreFacturado([FromBody] PreFacturadoRequerido preFacturadoReq)
        {
            var preFacturado = _uow.RepositorioJuncalPreFactura.GetByCondition(c => c.IdOrden==preFacturadoReq.IdOrden && 
            c.IdMaterialEnviado==preFacturadoReq.IdMaterialEnviado && c.Remito==preFacturadoReq.Remito && c.IsDelete == false);

            PreFacturadoRespuesta preFacturarNuevo = new PreFacturadoRespuesta();

            if (preFacturado is null)
            {
                var preFacturarobj = _mapper.Map<JuncalPreFacturar>(preFacturadoReq);

                _uow.RepositorioJuncalPreFactura.Insert(preFacturarobj);
               
                preFacturarNuevo =  _mapper.Map<PreFacturadoRespuesta>(preFacturarobj);
                
                return Ok(new { success = true, message = "Pre Facturar Con Exito", result = preFacturarNuevo });
            }
       
         
            return Ok(new { success = false, message = " El Dato Enviado Ya Esta Pre Facturado ", result = preFacturarNuevo});

        }

    }
}
