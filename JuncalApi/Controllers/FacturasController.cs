using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<FacturasController> _logger;

        public FacturasController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<FacturasController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaRespuesta>>> GetFacturas()
        {
            try
            {
                var ListaFacturas = _uow.RepositorioJuncalFactura.JuncalFacturaList();

                if (ListaFacturas.Count() > 0)
                {
                    return Ok(new { success = true, message = "La Lista Está Lista Para Ser Utilizada", result = ListaFacturas });
                }

                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<FacturaRespuesta>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de facturas");
                return StatusCode(500, "Ocurrió un error al obtener la lista de facturas");
            }
        }

        [HttpGet("{numFactura}")]
        public ActionResult GetFactura(string numFactura)
        {
            try
            {
                var factura = _uow.RepositorioJuncalFactura.GetByNumeroFactura(numFactura);

                if (factura != null)
                {
                    return Ok(new { success = true, message = "Factura encontrada", result = factura });
                }

                return Ok(new { success = false, message = "La factura no fue encontrada", result = new FacturaRespuesta() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la factura");
                return StatusCode(500, "Ocurrió un error al obtener la factura");
            }
        }
    }
}
