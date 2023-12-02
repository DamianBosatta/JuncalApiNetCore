using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuncalApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PreFacturarController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<PreFacturarController> _logger;

        public PreFacturarController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<PreFacturarController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PreFacturadoRespuesta>>> GetPreFacturado()
        {
            try
            {
                var ListaPreFacturado = _uow.RepositorioJuncalPreFactura.GetAllPreFacturar();

                if (ListaPreFacturado.Count() > 0)
                {
                    List<PreFacturadoRespuesta> listaPreFacturadoRespuesta = _mapper.Map<List<PreFacturadoRespuesta>>(ListaPreFacturado);
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = listaPreFacturadoRespuesta });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<PreFacturadoRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista pre-facturada");
                return StatusCode(500, new { success = false, message = "Error al obtener la lista pre-facturada", result = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult CargarPreFacturado([FromBody] PreFacturadoRequerido preFacturadoReq)
        {
            try
            {
                var preFacturado = _uow.RepositorioJuncalPreFactura.GetByCondition(c => c.IdOrden == preFacturadoReq.IdOrden &&
                c.IdMaterialEnviado == preFacturadoReq.IdMaterialEnviado && c.Remito == preFacturadoReq.Remito && c.IsDelete == false);

                PreFacturadoRespuesta preFacturarNuevo = new PreFacturadoRespuesta();

                if (preFacturado is null)
                {
                    var preFacturarobj = _mapper.Map<JuncalPreFacturar>(preFacturadoReq);
                    _uow.RepositorioJuncalPreFactura.Insert(preFacturarobj);
                    preFacturarNuevo = _mapper.Map<PreFacturadoRespuesta>(preFacturarobj);
                    return Ok(new { success = true, message = "Pre Facturado Con Éxito", result = preFacturarNuevo });
                }

                return Ok(new { success = false, message = "El Dato Enviado Ya Está Pre Facturado", result = preFacturarNuevo });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar pre-facturado");
                return StatusCode(500, new { success = false, message = "Error al cargar pre-facturado", result = ex.Message });
            }
        }
    }
}
