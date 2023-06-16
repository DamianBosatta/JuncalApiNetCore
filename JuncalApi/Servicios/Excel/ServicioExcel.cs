using AutoMapper;
using JuncalApi.Dto.DtoExcel;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Codigos_Utiles;
using JuncalApi.Modelos.Item;
using JuncalApi.UnidadDeTrabajo;
using OfficeOpenXml;
using System.Globalization;
using System.Reflection;

namespace JuncalApi.Servicios.Excel
{
    public class ServicioExcel : IServicioExcel
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ServicioExcel(IUnidadDeTrabajo uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }


        public List<ExcelGenerico> GetExcel(IFormFile formFile,int idAceria)
        {
            List<ExcelGenerico> listaExcelGenerico = new List<ExcelGenerico>();

            switch (idAceria)
            {

                case Codigos.IdAceriaAcerbrag:

                   var listaMapeoExcel = MapeoExcelAcerbrag(formFile);
                    
                    
                    foreach(var objExcel in listaMapeoExcel)
                    {
                       ExcelGenerico excelGenerico = new ExcelGenerico();
                        var remito = ObtenerOrden(objExcel.RemitoNotaEntrega);

                        if (remito != null)
                        {
                            ItemRemito remitoJuncal = new ItemRemito();

                            remitoJuncal = _uow.RepositorioJuncalOrden.GetRemito(remito.Id);
                            if (remitoJuncal != null)
                            {

                                excelGenerico.Chofer = objExcel.Chofer is null ? "Sin Chofer Cargado": objExcel.Chofer;
                            excelGenerico.NombreMaterial = objExcel.NombreMaterialJuncal is null ? "sin Nombre Material": objExcel.NombreMaterialJuncal;
                            excelGenerico.FechaRemito =ConvertirFecha((string)objExcel.Fecha);
                            excelGenerico.Remito = objExcel.RemitoNotaEntrega is null ? "Sin Remito ": objExcel.RemitoNotaEntrega;
                            excelGenerico.NombreCliente = remitoJuncal is null ?"Sin Cliente": remitoJuncal.NombreProveedor;
                            excelGenerico.DescripcionContrato = remitoJuncal is null ? "Sin Contrato Registrado": remitoJuncal.Contrato.Nombre;
                            excelGenerico.KgBruto = objExcel.KgBruto is null ? 0 : decimal.Parse(objExcel.KgBruto);
                            excelGenerico.KgDescargado = decimal.Parse(objExcel.KgDescargados);
                            excelGenerico.KgTara = objExcel.KgTara is null ? 0 : decimal.Parse(objExcel.KgTara);
                           
                             excelGenerico.DiferenciaMaterial = DiferenciaMaterial(remitoJuncal.IdAceria, remitoJuncal.Id,
                             objExcel.CodigoMaterial);
                             excelGenerico.DiferenciaPeso = DiferenciaPeso(remitoJuncal.IdAceria, remitoJuncal.Id,
                             objExcel.CodigoMaterial, decimal.Parse(objExcel.KgDescargados));
                             listaExcelGenerico.Add(excelGenerico);
                            }
                        

                        }

                        
                    }
                                                                              
                    break;
               
            }

            return listaExcelGenerico;

        }

        #region MAPEO EXCEL ACERBRAG
       
        public List<ExcelAcerbrag> MapeoExcelAcerbrag(IFormFile formFile)
        {

            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage(formFile.OpenReadStream()))
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
                        RemitoNotaEntrega = !string.IsNullOrEmpty(worksheet.Cells[row, 3].Value?.ToString()) ?
                        worksheet.Cells[row, 3].Value?.ToString().Substring(0, (int)(worksheet.Cells[row, 3].Value?.ToString().IndexOf("/"))) : null,
                        Fecha = worksheet.Cells[row, 4].Value?.ToString(),
                        CodigoProveedor = worksheet.Cells[row, 5].Value?.ToString(),
                        NombreProveedor = worksheet.Cells[row, 6].Value?.ToString(),
                        CodigoMaterial = worksheet.Cells[row, 7].Value?.ToString().TrimStart('0'),
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
                        NombreMaterialJuncal = NombreMaterial(7, worksheet.Cells[row, 7].Value?.ToString().TrimStart('0')),
                        // Mapea las propiedades adicionales según la estructura de tu archivo Excel
                    };

                    excelDataList.Add(excelData);
                }

                return excelDataList;

            }

        }
        #endregion


        #region METODOS PRIVADOS

        public bool DiferenciaMaterial(int idAceria, int idOrden, string codigoMaterial)
        {
           var material = _uow.RepositorioJuncalOrdenMarterial.DataMaterial(idAceria,idOrden,codigoMaterial);


            return material is null ? false : true;

        }


   
        public string NombreMaterial(int idAceria, string CodigoMaterial)
        {
            var material = _uow.RepositorioJuncalAceriaMaterial.GetByCondition(a => a.Cod == CodigoMaterial && a.IdAceria == idAceria);

            if (material != null)
            {
                PropertyInfo nombreProperty = material.GetType().GetProperty("Nombre");
                if (nombreProperty != null)
                {
                    var nombreValue = nombreProperty.GetValue(material);
                    if (nombreValue != null)
                    {
                        return nombreValue.ToString();
                    }
                }
            }

            return string.Empty;


        }



        public bool DiferenciaPeso(int idAceria,int idOrden,string codigoMaterial, decimal kgDescargado)
        {
            ItemDataMateriales material = _uow.RepositorioJuncalOrdenMarterial.DataMaterial(idAceria, idOrden, codigoMaterial);

            if (material != null)
            {
                    decimal diferencia = (decimal)(material.Peso - kgDescargado);
                    if (diferencia >= 400)
                    {
                        return true;
                    }
            }          
          
            return false;

        }


        public JuncalOrden ObtenerOrden(string remito)
        {

            return _uow.RepositorioJuncalOrden.GetByCondition(a => a.Remito == remito);

        }

        public DateTime ConvertirFecha(string fecha)
        {

            DateTime resultado;

            if (DateTime.TryParseExact(fecha, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out resultado))
            {
                return resultado;
            }

            return new DateTime();
        }

        #endregion



    }
}
