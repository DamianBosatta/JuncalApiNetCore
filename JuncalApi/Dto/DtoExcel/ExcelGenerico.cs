

using JuncalApi.Modelos.Item;

namespace JuncalApi.Dto.DtoExcel
{
    public class ExcelGenerico
    {
        ItemDataMateriales? DataMateriales { get; set; }

        ExcelMapper? ExcelAcerbrag { get; set; }


        public ExcelGenerico(ItemDataMateriales? dataMateriales, ExcelMapper? excelAcerbrag)
        {
            DataMateriales = dataMateriales;
            ExcelAcerbrag = excelAcerbrag;
        }

        public int? IdOrden { get { return DataMateriales is null ? 0 : DataMateriales.IdOrden; } }

        public DateTime? FechaRemitoJuncal
        {
            get
            {
                if (DataMateriales?.Orden?.Fecha != null)
                {
                    return DataMateriales.Orden.Fecha;
                }
                return null;
            }
        }
        public string? FechaRemitoAceria
        {
            get
            {
                if (ExcelAcerbrag?.Fecha != null)
                {
                    return ExcelAcerbrag?.Fecha;
                }
                return null;
            }
        }

        public string? Remito { get { return DataMateriales is null ? "Sin numero Remito": DataMateriales.Remito; } }  

        public string? NombreMaterialJuncal{ get { return DataMateriales is null ? "Sin nombre de material de juncal ": DataMateriales.NombreMaterialJuncal; }}

        public string? NombreMaterialAceria { get { return ExcelAcerbrag?.NombreMaterial; } }
        
        public string? CodigoMaterial { get { return DataMateriales?.Cod; } }

        public decimal? PesoDescargadoAceria
        {
            get
            {
                decimal result;
                if (ExcelAcerbrag != null && decimal.TryParse(ExcelAcerbrag.Descargado, out result))
                {
                    return result;
                }
                return 0;
            }
        }
        public decimal? PesoEnviadoJuncal { get { return DataMateriales is null ? 0 : DataMateriales.PesoEnviado; } }
       
        public decimal? DiferenciaPeso { get { return ((decimal)(PesoEnviadoJuncal - PesoDescargadoAceria)); } }

        public bool? DiferenciaMaterial { get { return NombreMaterialJuncal.Equals(NombreMaterialAceria) ? false : true; } }

        public bool? DiferenciaPesoBool { get { return DiferenciaPeso >= 400 && DiferenciaMaterial is false ? true : false; } }

    }
}
