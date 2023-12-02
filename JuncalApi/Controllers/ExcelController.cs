using AutoMapper;
using JuncalApi.Dto.DtoExcel;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Servicios.Excel;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServicioExcel _excel;
        private readonly ILogger<ExcelController> _logger;

        public ExcelController(IUnidadDeTrabajo uow, IMapper mapper, IServicioExcel excel, ILogger<ExcelController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _excel = excel;
            _logger = logger;
        }

        [HttpPost("map")]
        public IActionResult MapExcel(IFormFile file, int idAceria)
        {
            try
            {
                var listaMapeada = _excel.GetExcel(file, idAceria).OrderBy(a => a.IdOrden).ToList();

                if (listaMapeada.Count() > 0)
                {
                    return Ok(new { success = true, message = "Lista Cargada", result = listaMapeada });
                }
                return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ExcelGenerico>() });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar el archivo Excel");
                return StatusCode(500, "Ocurrió un error al procesar el archivo Excel");
            }
        }
    }
}
