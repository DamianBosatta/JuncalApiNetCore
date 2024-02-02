namespace JuncalApi.Modelos.Item
{
    public class ItemReporteAcerias
    {
     
        public int IdAceria { get; set; } = 0;

        public string DescripcionAceria { get; set; } = "Sin Descripcion";

        public int IdMaterial { get; set; } = 0;

        public string DescripcionMaterial { get; set; } = "Sin Descripcion";

        public int IdEstado { get; set; } = 0;

        public string DescripcionEstado { get; set; } = "Sin Descripcion";

        public string KgEnviados { get; set; } = "Sin Peso Calculado";

        public string KgRecibidos { get; set; } = "Sin Facturar";
       
        public ItemReporteAcerias() { }
    }
}
