using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace JuncalApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : Controller
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public OrdenController(IUnidadDeTrabajo uow, IMapper mapper)
        {

            _mapper = mapper;
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenRespuesta>>> GetOrdenes()
        {

            var ListaOrdenes = _uow.RepositorioJuncalOrden.GetAllByCondition(c => c.Isdeleted == false).ToList();

            if (ListaOrdenes.Count() > 0)
            {
                List<OrdenRespuesta> listaOrdenesRespuesta = _mapper.Map<List<OrdenRespuesta>>(ListaOrdenes);
                return Ok(new { success = true, message = "La Lista Esta Lista Para Ser Utilizada", result = listaOrdenesRespuesta });

            }
            return Ok(new { success = false, message = "La Lista No Contiene Datos", result = new List<OrdenRespuesta>() == null });


        }


        [HttpPost]
        public ActionResult CargarOrdenes([FromBody] OrdenRequerido ordenReq)
        {
            var orden = _uow.RepositorioJuncalOrden.GetByCondition(c => c.IdAceria == ordenReq.IdAceria
            && c.IdCamion == ordenReq.IdCamion
            && c.IdContrato == ordenReq.IdContrato
            && c.IdProveedor == ordenReq.IdProveedor
            && c.Isdeleted == false);

            if (orden is null)
            {
                JuncalOrden ordenNuevo = _mapper.Map<JuncalOrden>(ordenReq);
                _uow.RepositorioJuncalOrden.Insert(ordenNuevo);
                OrdenRespuesta ordenRes = new();
                _mapper.Map(ordenNuevo, ordenRes);
                return Ok(new { success = true, message = "La Orden Fue Creada Con Exito", result = ordenRes });
            }
            OrdenRespuesta ordenExiste = new();
            _mapper.Map(orden, ordenExiste);
            return Ok(new { success = false, message = " La Orden Ya Esta Cargada ", result = ordenExiste });

        }


        [Route("Borrar/{id?}")]
        [HttpPut]
        public IActionResult IsDeletedOrden(int id)
        {

            var orden = _uow.RepositorioJuncalOrden.GetById(id);
            if (orden != null && orden.Isdeleted == false)
            {
                orden.Isdeleted = true;
                _uow.RepositorioJuncalOrden.Update(orden);
                OrdenRespuesta ordenRes = new();
                _mapper.Map(orden, ordenRes);

                return Ok(new { success = true, message = "La Orden Fue Eliminada ", result = ordenRes });

            }
            return Ok(new { success = false, message = "La Orden No Fue Encontrado", result = new OrdenRespuesta() == null });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOrden(int id, OrdenRequerido ordenEdit)
        {
            var orden = _uow.RepositorioJuncalOrden.GetById(id);

            if (orden != null && orden.Isdeleted == false)
            {
                _mapper.Map(ordenEdit, orden);
                _uow.RepositorioJuncalOrden.Update(orden);
                OrdenRespuesta ordenRes = new();
                _mapper.Map(orden, ordenRes);
                return Ok(new { success = true, message = "La Orden Fue Actualizada", result = ordenRes });
            }

            return Ok(new { success = false, message = "La Orden No Fue Encontrada ", result = new OrdenRespuesta() == null });


        }

        [HttpPost]
        public IActionResult ProcesarArchivoExcel(IFormFile archivoExcel)
        {

            using (var stream = new MemoryStream())
            {
                archivoExcel.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    // Obtener la hoja de cálculo en la que se encuentran los datos
                    var worksheet = package.Workbook.Worksheets["NombreHojaCalculo"];

                    // Obtener el rango de celdas que contienen los datos
                    var startRow = 2; // El primer registro está en la fila 2
                    var endRow = worksheet.Dimension.End.Row;
                    var startCol = 1;
                    var endCol = worksheet.Dimension.End.Column;
                    var cellRange = worksheet.Cells[startRow, startCol, endRow, endCol];

                    // Mapear los datos a un modelo de datos
                    var datos = new List<PruebaExcel>();

                    

                    foreach (var row in cellRange)
                    {
                        var dato = new PruebaExcel();
                        {
                            dato.nombre= row[1].ToString();
                            dato.apellido = row[startCol + 1].Value.ToString();
                           
                        };

                        datos.Add(dato);
                    }

                    // Hacer algo con los datos mapeados
                    // ...

                    return Ok();
                }
            }
        }

        public class PruebaExcel
        {

           public string nombre { get; set; } = string.Empty;
            public string apellido { get; set; } = string.Empty;
           public  string edad { get; set; } = string.Empty;




        }

    }
}
