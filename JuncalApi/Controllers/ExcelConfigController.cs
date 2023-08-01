using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelConfigController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ExcelConfigController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }




        [HttpGet]
        public async Task<IActionResult> GetExcelConfigByAceria(int idAceria)
        {
            var excelConfig = await Task.Run(() => _uow.RepositorioJuncalExcelConfig.GetByCondition(a=>a.IdAceria==idAceria));
            var excelRespuesta = new ExcelConfigRespuesta();

            if (excelConfig != null)
            {

                _mapper.Map(excelConfig, excelRespuesta);
                return Ok(new { success = true, message = "Response Confirmado", result = excelRespuesta});
            }

            return Ok(new { success = false, message = "No Se Encontro La Configuracion Para El Identificador De Aceria Que Envio", result = excelRespuesta });
        }



        [HttpPost]
        public ActionResult CargarConfigExcel([FromBody] ExcelConfigRequerido excelConfigReq)
        {
            var excelConfig = _uow.RepositorioJuncalExcelConfig.GetByCondition(c => c.IdAceria==excelConfigReq.IdAceria);
            ExcelConfigRespuesta configRes = new();
            if (excelConfig is null)
            {
                JuncalExcelConfig configNuevo = _mapper.Map<JuncalExcelConfig>(excelConfigReq);

                _uow.RepositorioJuncalExcelConfig.Insert(configNuevo);
  
              
                _mapper.Map(configNuevo, configRes);
                return Ok(new { success = true, message = " La Configuracion Fue Creada Con Exito ", result = configRes });

            }
            
            
            return Ok(new { success = false, message = "La Aceria Ya Tiene Una Configuracion Cargada ", result = configRes });

        }
        [HttpPut]
        public async Task<IActionResult> EditConfig(int idAceria, ExcelConfigRequerido configExcelEdit)
        {
            var excelConfig = _uow.RepositorioJuncalExcelConfig.GetByCondition(a=>a.IdAceria==idAceria);
            ExcelConfigRespuesta excelConfigRes = new();
            if (excelConfig != null )
            {
                _mapper.Map(configExcelEdit, excelConfig);
                _uow.RepositorioJuncalExcelConfig.Update(excelConfig);
                
                _mapper.Map(excelConfig, excelConfigRes);
                return Ok(new { success = true, message = "La Configuracion fue Actualizada ", result = excelConfigRes });
            }

            return Ok(new { success = false, message = "No Se Encontro Configuracion De La Aceria Para Actualizar ", result = excelConfigRes });


        }


    }
}

