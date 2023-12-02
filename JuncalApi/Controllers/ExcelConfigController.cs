using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelConfigController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ExcelConfigController> _logger;

        public ExcelConfigController(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ExcelConfigController> logger)
        {
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetExcelConfigByAceria(int idAceria)
        {
            try
            {
                var excelConfig = await Task.Run(() => _uow.RepositorioJuncalExcelConfig.GetByCondition(a => a.IdAceria == idAceria));
                var excelRespuesta = new ExcelConfigRespuesta();

                if (excelConfig != null)
                {
                    _mapper.Map(excelConfig, excelRespuesta);
                    return Ok(new { success = true, message = "Response Confirmado", result = excelRespuesta });
                }

                return Ok(new { success = false, message = "No Se Encontró La Configuración Para El Identificador De Acería Que Envío", result = excelRespuesta });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener configuración de Excel por Acería");
                return StatusCode(500, "Ocurrió un error al obtener la configuración de Excel por Acería");
            }
        }

        [HttpPost]
        public ActionResult CargarConfigExcel([FromBody] ExcelConfigRequerido excelConfigReq)
        {
            try
            {
                var excelConfig = _uow.RepositorioJuncalExcelConfig.GetByCondition(c => c.IdAceria == excelConfigReq.IdAceria);
                ExcelConfigRespuesta configRes = new();

                if (excelConfig is null)
                {
                    JuncalExcelConfig configNuevo = _mapper.Map<JuncalExcelConfig>(excelConfigReq);
                    _uow.RepositorioJuncalExcelConfig.Insert(configNuevo);
                    _mapper.Map(configNuevo, configRes);
                    return Ok(new { success = true, message = "La Configuración Fue Creada Con Éxito", result = configRes });
                }

                return Ok(new { success = false, message = "La Acería Ya Tiene Una Configuración Cargada", result = configRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar configuración de Excel");
                return StatusCode(500, "Ocurrió un error al cargar la configuración de Excel");
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditConfig(int idAceria, ExcelConfigRequerido configExcelEdit)
        {
            try
            {
                var excelConfig = _uow.RepositorioJuncalExcelConfig.GetByCondition(a => a.IdAceria == idAceria);
                ExcelConfigRespuesta excelConfigRes = new();

                if (excelConfig != null)
                {
                    _mapper.Map(configExcelEdit, excelConfig);
                    _uow.RepositorioJuncalExcelConfig.Update(excelConfig);
                    _mapper.Map(excelConfig, excelConfigRes);
                    return Ok(new { success = true, message = "La Configuración Fue Actualizada", result = excelConfigRes });
                }

                return Ok(new { success = false, message = "No Se Encontró Configuración De La Acería Para Actualizar", result = excelConfigRes });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al editar configuración de Excel por Acería");
                return StatusCode(500, "Ocurrió un error al editar la configuración de Excel por Acería");
            }
        }
    }
}

