using JuncalApi.Dto.DtoExcel;
using JuncalApi.Modelos;

namespace JuncalApi.Servicios.Excel
{
    public interface IServicioExcel
    {

        public List<ExcelGenerico> GetExcel(IFormFile formFile,int idAceria);
        
        public List<ExcelAcerbrag> MapeoExcelAcerbrag(IFormFile formFile);

        public string NombreMaterial(int idAceria, string CodigoMaterial);
        
        public bool DiferenciaMaterial(int idAceria, int idOrden, string codigoMaterial);

        public bool DiferenciaPeso(int idAceria, int idOrden, string codigoMaterial, decimal kgDescargado);
              
        public JuncalOrden ObtenerOrden(string remito);

        public DateTime ConvertirFecha(string fecha);

    }
}
