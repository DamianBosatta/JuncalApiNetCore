using AutoMapper;
using JuncalApi.Dto.DtoExcel;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace JuncalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelController : Controller
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ExcelController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpPost("map")]
        public IActionResult MapExcel(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage(file.OpenReadStream()))
            {
                var worksheet = package.Workbook.Worksheets[0]; // Suponiendo que el archivo contiene solo una hoja

                var excelDataList = new List<ExcelAcerbrag>();
                string fecha = string.Empty;

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Empieza desde la segunda fila para omitir los encabezados
                {
                    var excelData = new ExcelAcerbrag
                    {
                        Contrato = worksheet.Cells[row, 1].Value?.ToString(),
                        Entrega = worksheet.Cells[row, 2].Value?.ToString(),
                        RemitoNotaEntrega = worksheet.Cells[row, 3].Value?.ToString(),
                        Fecha = worksheet.Cells[row, 4].Value?.ToString(),
                        CodigoProveedor = worksheet.Cells[row, 5].Value?.ToString(),
                        NombreProveedor = worksheet.Cells[row, 6].Value?.ToString(),
                        CodigoMaterial = worksheet.Cells[row, 7].Value?.ToString(),
                        NombreMaterial = worksheet.Cells[row, 8].Value?.ToString(),
                        Cantidad = worksheet.Cells[row, 9].Value?.ToString(),
                        UnidadMedida = worksheet.Cells[row, 10].Value?.ToString(),
                        IncoTerms = worksheet.Cells[row, 11].Value?.ToString(),
                        Transporte = worksheet.Cells[row, 12].Value?.ToString(),
                        Chofer = worksheet.Cells[row, 13].Value?.ToString(),
                        Camion = worksheet.Cells[row, 14].Value?.ToString(),
                        Acoplado = worksheet.Cells[row, 15].Value?.ToString(),
                        KgBruto = worksheet.Cells[row, 16].Value?.ToString(),
                        KgTara = worksheet.Cells[row, 17].Value?.ToString(),
                        KgDescuento = worksheet.Cells[row, 18].Value?.ToString(),
                        Descuento = worksheet.Cells[row, 19].Value?.ToString(),
                        KgDescargados = worksheet.Cells[row, 20].Value?.ToString(),
                        NombreMaterialJuncal =_uow.RepositorioJuncalAceriaMaterial.NombreMaterial(7, worksheet.Cells[row, 7].Value?.ToString().TrimStart('0'))
                        // Mapea las propiedades adicionales según la estructura de tu archivo Excel
                    };

                    excelDataList.Add(excelData);
                }

                return Ok(excelDataList);
            }





        }
    }
}
