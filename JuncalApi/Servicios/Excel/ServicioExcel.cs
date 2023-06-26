using AutoMapper;
using JuncalApi.Dto.DtoExcel;
using JuncalApi.Modelos.Item;
using JuncalApi.UnidadDeTrabajo;
using OfficeOpenXml;


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

        #region METODO DONDE MAPEAMOS EL EXCEL DE TODAS LAS ACERIAS
        public List<ExcelGenerico> GetExcel(IFormFile formFile, int idAceria)
        {
            List<ExcelGenerico> listaExcelGenerico = new List<ExcelGenerico>();


                    #region Logica Mapeo Excel

                    var listaMapeoExcel = MapeoExcelAcerbrag(idAceria,formFile); //Mapeamos El Excel

                    var listaRemito = (from l in listaMapeoExcel
                                       select l.Remito).Distinct().ToList(); // Obtenemos La Lista De Los Remitos Del Excel


                    var remitosComparar = _uow.RepositorioJuncalOrdenMarterial.DataMaterial(idAceria, listaRemito);//Query En Base De Dato



                   listaExcelGenerico = ComparadorRemitoExcel(listaMapeoExcel, remitosComparar);//Comparamos Excel Con Query En Base De Dato


                    #endregion


            return listaExcelGenerico;

        }

        #endregion

        #region MAPEO EXCEL ACERIA

        public List<ExcelMapper> MapeoExcelAcerbrag(int idAceria,IFormFile formFile)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;

                using (var package = new ExcelPackage(formFile.OpenReadStream()))
                {
                    var worksheet = package.Workbook.Worksheets[0]; // Suponiendo que el archivo contiene solo una hoja

                    var excelDataList = new List<ExcelMapper>();

                    var configExcel = _uow.RepositorioJuncalExcelConfig.GetByCondition(a=>a.IdAceria==idAceria); 

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Empieza desde la segunda fila para omitir los encabezados
                    {
                        var excelData = new ExcelMapper
                        {
                          
                            Remito = worksheet.Cells[row, configExcel.Remito].Value?.ToString()?.Split('/')[0],
                            Fecha = (worksheet.Cells[row, configExcel.Fecha].Value is double fechaNumero) ? DateTime.FromOADate(fechaNumero).ToString("dd/MM/yyyy") : string.Empty,
                            CodigoMaterial = worksheet.Cells[row, configExcel.MaterialCodigo].Value?.ToString().TrimStart('0'),
                            NombreMaterial = worksheet.Cells[row, configExcel.MaterialNombre].Value?.ToString(),      
                            Bruto = worksheet.Cells[row, configExcel.Bruto].Value?.ToString(),
                            Tara = worksheet.Cells[row, configExcel.Tara].Value?.ToString(),
                            Descuento = worksheet.Cells[row, configExcel.Descuento].Value?.ToString(),
                            DescuentoDetalle = worksheet.Cells[row, configExcel.DescuentoDetalle].Value?.ToString(),
                            Descargado = worksheet.Cells[row, configExcel.Neto].Value?.ToString(),
                        };

                        excelDataList.Add(excelData);
                    }

                    return excelDataList;
                }
            }
            catch (Exception ex)
            {

               
                // Aquí puedes realizar las acciones necesarias para manejar la excepción de manera profesional,
                // como registrar el error en un archivo de registro, enviar una notificación por correo electrónico, etc.
                // También puedes lanzar una excepción personalizada o retornar una lista vacía, dependiendo de tus necesidades.

                // Por ejemplo, para lanzar una excepción personalizada:
                //throw new CustomException("Ocurrió un error durante el mapeo del archivo Excel.", ex);

                //O para retornar una lista vacía:
                return new List<ExcelMapper>();

                // Asegúrate de elegir la estrategia de manejo de excepciones que se ajuste mejor a tu aplicación.
            }
        }

        #endregion

        #region METODOS PRIVADOS


        private List<ExcelGenerico> ComparadorRemitoExcel(List<ExcelMapper> listaExcel ,List<ItemDataMateriales>ListaDataMateriales)
        {

            var query = (from excel in listaExcel
                        join Remito in ListaDataMateriales
                        on excel.Remito equals Remito.Remito
                        select new ExcelGenerico(Remito, excel)).ToList();



            return query;
        }


        #endregion

    }
}

