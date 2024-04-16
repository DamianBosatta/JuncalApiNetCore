namespace JuncalApi.Modelos.Item
{
    public class ReporteAceria
    {
        public int IdAceria { get; set; } = 0;
        public string NombreAceria { get; set; } = string.Empty;
        public List<ItemMaterial> Materiales { get; set; }= new List<ItemMaterial>();


        public ReporteAceria()
        {
           
        }

        public class ItemMaterial
        {
            public int IdMaterial { get; set; } = 0;
            public string NombreMaterial { get; set; }= string.Empty;
            public decimal PesoRecibido { get; set; } = 0;
            public decimal PesoPendiente{ get; set; } = 0;

            public ItemMaterial(int idMaterial, string nombreMaterial, decimal pesoRecibido, decimal pesoPendiente)
            {
                IdMaterial = idMaterial;
                NombreMaterial = nombreMaterial;
                PesoRecibido = pesoRecibido;
                PesoPendiente = pesoPendiente;
            }

         
        }
    }
}
