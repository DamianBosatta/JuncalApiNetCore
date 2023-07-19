using AutoMapper;
using JuncalApi.Dto.DtoExcel;
using JuncalApi.Modelos;
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

        /// <summary>
        /// Obtiene una lista de objetos ExcelGenerico que representan los datos mapeados desde un archivo Excel para una acería específica.
        /// </summary>
        /// <param name="formFile">Archivo Excel</param>
        /// <param name="idAceria">ID de la acería</param>
        /// <returns>Lista de objetos ExcelGenerico</returns>
        public List<ExcelGenerico> GetExcel(IFormFile formFile, int idAceria)
        {
            List<ExcelGenerico> listaExcelGenerico = new List<ExcelGenerico>();

            #region LOGICA MAPEO EXCEL

            var listaMapeoExcel = MapeoExcelAcerbrag(idAceria, formFile); // Mapeamos El Excel

            var listaRemito = (from l in listaMapeoExcel
                               select l.Remito).Distinct().ToList(); // Obtenemos La Lista De Los Remitos Del Excel

            var remitosComparar = _uow.RepositorioJuncalOrdenMarterial.GetDatosMaterialesAndRemitoExcel(idAceria, listaRemito); // Query En Base De Datos

            listaExcelGenerico = ComparadorRemitoExcel(listaMapeoExcel, remitosComparar); // Comparamos Excel Con Query En Base De Datos

            CargarPreFacturado(listaExcelGenerico); // Cargamos El Pre Facturado
           
            #endregion

            return listaExcelGenerico;
        }

        #endregion

        #region MAPEO EXCEL ACERIA

        /// <summary>
        /// Mapea los datos de un archivo Excel para una acería específica.
        /// </summary>
        /// <param name="idAceria">ID de la acería</param>
        /// <param name="formFile">Archivo Excel</param>
        /// <returns>Lista de objetos ExcelMapper</returns>
        public List<ExcelMapper> MapeoExcelAcerbrag(int idAceria, IFormFile formFile)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;

                using (var package = new ExcelPackage(formFile.OpenReadStream()))
                {
                    // Verificar si el archivo de Excel contiene la hoja esperada
                    if (package.Workbook.Worksheets.Count < 1)
                    {
                        // Devolver un valor predeterminado o una lista vacía en lugar de lanzar una excepción
                        return new List<ExcelMapper>();
                    }

                    var worksheet = package.Workbook.Worksheets[0]; // Suponiendo que el archivo contiene solo una hoja

                    var excelDataList = new List<ExcelMapper>();

                    var configExcel = _uow.RepositorioJuncalExcelConfig.GetByCondition(a => a.IdAceria == idAceria);

                    if (configExcel != null)
                    {
                        for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Empieza desde la segunda fila para omitir los encabezados
                        {
                            var excelData = new ExcelMapper
                            {
                                Remito = ObtenerSubString(configExcel.ConfigRemitoDesde,configExcel.ConfigRemitoCantidad,worksheet.Cells[row, configExcel.Remito].Value?.ToString()),
                                Fecha = (worksheet.Cells[row, configExcel.Fecha].Value is double fechaNumero) ? DateTime.FromOADate(fechaNumero).ToString("dd/MM/yyyy") : string.Empty,
                                CodigoMaterial = ObtenerSubString(configExcel.ConfigMaterialCantidad, configExcel.ConfigMaterialHasta, worksheet.Cells[row, configExcel.MaterialCodigo].Value?.ToString()),
                                NombreMaterial = worksheet.Cells[row, configExcel.MaterialNombre].Value?.ToString(),
                                Bruto = worksheet.Cells[row, configExcel.Bruto].Value?.ToString(),
                                Tara = worksheet.Cells[row, configExcel.Tara].Value?.ToString(),
                                Descuento = worksheet.Cells[row, configExcel.Descuento].Value?.ToString(),
                                DescuentoDetalle = worksheet.Cells[row, configExcel.DescuentoDetalle].Value?.ToString(),
                                Descargado = worksheet.Cells[row, configExcel.Neto].Value?.ToString(),
                            };

                            excelDataList.Add(excelData);
                        }

                        return ComprobarSiRepetimosMaterial(excelDataList);
                    }

                    return excelDataList; // Devolvemos una lista vacía si no tiene configuración el mapeo de Excel
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mapear el archivo de Excel.", ex);
                // Aquí puedes realizar las acciones necesarias para manejar la excepción de manera profesional,
                // como registrar el error en un archivo de registro, enviar una notificación por correo electrónico, etc.
                // También puedes lanzar una excepción personalizada o retornar una lista vacía, dependiendo de tus necesidades.
                // Por ejemplo, para lanzar una excepción personalizada:
                // throw new CustomException("Ocurrió un error durante el mapeo del archivo Excel.", ex);
                // O para retornar una lista vacía:
                // return new List<ExcelMapper>();
                // Asegúrate de elegir la estrategia de manejo de excepciones que se ajuste mejor a tu aplicación.
            }
        }

        #endregion

        #region METODOS PRIVADOS

        /// <summary>
        /// Compara los remitos del Excel con los datos de materiales y remitos en la base de datos.
        /// </summary>
        /// <param name="listaExcel">Lista de objetos ExcelMapper</param>
        /// <param name="ListaDataMateriales">Lista de objetos ItemDataMateriales</param>
        /// <returns>Lista de objetos ExcelGenerico</returns>
        private List<ExcelGenerico> ComparadorRemitoExcel(List<ExcelMapper> listaExcel, List<ItemDataMateriales> ListaDataMateriales)
        {
            var query = (from excel in listaExcel
                         join Remito in ListaDataMateriales on excel.Remito equals Remito.Remito
                         select new ExcelGenerico(Remito, excel)).ToList();

            return query;
        }

        /// <summary>
        /// Calcula la suma de pesos para cada remito y código de material en la lista de ExcelMapper.
        /// </summary>
        /// <param name="listaExcel">Lista de objetos ExcelMapper</param>
        /// <returns>Diccionario con la clave remito-código de material y el valor suma de pesos</returns>
        private Dictionary<string, int> CalcularSumaPesos(List<ExcelMapper> listaExcel)
        {
            Dictionary<string, int> sumaPesos = new Dictionary<string, int>();

            foreach (var objExcel in listaExcel)
            {
                string clave = $"{objExcel.Remito}-{objExcel.CodigoMaterial}";

                if (int.TryParse(objExcel.Descargado, out int peso))
                {
                    if (sumaPesos.ContainsKey(clave))
                    {
                        sumaPesos[clave] += peso;
                    }
                    else
                    {
                        sumaPesos.Add(clave, peso);
                    }
                }
                else
                {
                    // Puedes manejar el error o ignorar el registro aquí
                }
            }

            return sumaPesos;
        }

        /// <summary>
        /// Actualiza los pesos en la lista de ExcelMapper utilizando la suma de pesos calculada.
        /// </summary>
        /// <param name="listaExcel">Lista de objetos ExcelMapper</param>
        /// <param name="sumaPesos">Diccionario con la clave remito-código de material y el valor suma de pesos</param>
        /// <returns>Lista de objetos ExcelMapper actualizados</returns>
        private List<ExcelMapper> ActualizarPesos(List<ExcelMapper> listaExcel, Dictionary<string, int> sumaPesos)
        {
            List<ExcelMapper> listaResultado = new List<ExcelMapper>();

            foreach (var objExcel in listaExcel)
            {
                string clave = $"{objExcel.Remito}-{objExcel.CodigoMaterial}";

                if (sumaPesos.ContainsKey(clave))
                {
                    ExcelMapper nuevoObjeto = new ExcelMapper
                    {
                        Remito = objExcel.Remito,
                        CodigoMaterial = objExcel.CodigoMaterial,
                        Descargado = sumaPesos[clave].ToString(),
                        Fecha = objExcel.Fecha,
                        NombreMaterial = objExcel.NombreMaterial,
                        Bruto = objExcel.Bruto,
                        Tara = objExcel.Tara,
                        Descuento = objExcel.Descuento,
                        DescuentoDetalle = objExcel.DescuentoDetalle
                    };

                    listaResultado.Add(nuevoObjeto);
                    sumaPesos.Remove(clave);
                }
            }

            return listaResultado;
        }

        /// <summary>
        /// Verifica si hay repetición de materiales en la lista de ExcelMapper y actualiza los pesos en consecuencia.
        /// </summary>
        /// <param name="listaExcel">Lista de objetos ExcelMapper</param>
        /// <returns>Lista de objetos ExcelMapper actualizados</returns>
        private List<ExcelMapper> ComprobarSiRepetimosMaterial(List<ExcelMapper> listaExcel)
        {
            Dictionary<string, int> sumaPesos = CalcularSumaPesos(listaExcel);
            List<ExcelMapper> listaResultado = ActualizarPesos(listaExcel, sumaPesos);

            return listaResultado;
        }

        private string ObtenerSubString(int desde, int total, string mapperString)
        {
            // Asegurarse de que el parámetro mapperString no sea nulo
            if (mapperString == null)
            {
                return string.Empty; // Devolver cadena vacía si el parámetro es nulo
            }

            // Asegurarse de que el rango especificado esté dentro de los límites de la cadena original
            if (desde < 0 || desde >= mapperString.Length || total <= 0)
            {
                return string.Empty; // Devolver cadena vacía si el rango no es válido
            }

            // Calcular el índice de finalización del rango
            int hasta = desde + total - 1;
            hasta = Math.Min(hasta, mapperString.Length - 1);

            string respuesta = string.Empty;

            // Recorrer la cadena original y cargar la nueva palabra en el rango especificado
            for (int i = 0; i < mapperString.Length; i++)
            {
                if (i >= desde && i <= hasta)
                {
                    respuesta += mapperString[i];
                }
            }

            return respuesta;
        }


        private void CargarPreFacturado(List<ExcelGenerico> listaExcelRemitoMapeado)
        {
            foreach(var obj in listaExcelRemitoMapeado)
            {
              JuncalPreFacturar newPreFacturar = new JuncalPreFacturar((int)obj.IdOrden,obj.IdMaterial,int.Parse(obj.CodigoMaterialJuncal),
              (decimal)obj.PesoEnviadoJuncal,(decimal)obj.PesoTara,(decimal)obj.PesoBruto,(decimal)obj.PesoDescargadoAceria,obj.Remito);

                _uow.RepositorioJuncalPreFactura.Insert(newPreFacturar);

            }

        }


        #endregion


    }

}
