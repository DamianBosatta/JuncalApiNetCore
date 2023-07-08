using AutoMapper;
using JuncalApi.Dto.DtoExcel;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Servicios.Excel;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace JuncalApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly IServicioExcel _excel;

        public ExcelController(IUnidadDeTrabajo uow, IMapper mapper,IServicioExcel excel)
        {

            _mapper = mapper;
            _uow = uow;
            _excel = excel;
        }

        [HttpPost("map")]
        public IActionResult MapExcel(IFormFile file,int idAceria)
        {

           var listaMapeada = _excel.GetExcel(file, idAceria).OrderBy(a=>a.IdOrden).ToList();

            if (listaMapeada.Count() > 0)
            {

                return Ok(new { success = true, message = "Lista Cargada", result = listaMapeada});


            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<ExcelGenerico>() == null });




        }
    }
}
