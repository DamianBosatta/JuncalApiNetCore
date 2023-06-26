using JuncalApi.Dto.DtoExcel;
using JuncalApi.Modelos;

namespace JuncalApi.Servicios.Excel
{
    public interface IServicioExcel
    {

        public List<ExcelGenerico> GetExcel(IFormFile formFile,int idAceria);
        
        public List<ExcelMapper> MapeoExcelAcerbrag(int idAceria,IFormFile formFile);


    }
}
