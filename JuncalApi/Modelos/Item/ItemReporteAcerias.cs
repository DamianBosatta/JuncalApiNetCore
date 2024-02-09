namespace JuncalApi.Modelos.Item
{
    public class ItemReporteAcerias
    {
     
      

        public string DescripcionAceria { get; set; } = "Sin Descripcion";

        public List<ItemMaterialReporte> MaterialesDetalle { get; set; }

 
       
        public ItemReporteAcerias() { }
    }
}
