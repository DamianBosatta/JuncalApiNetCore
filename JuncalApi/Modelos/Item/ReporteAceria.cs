namespace JuncalApi.Modelos.Item
{
    public class ReporteAceria
    {
        public int IdAceria { get; set; } = 0;
        public string NombreAceria { get; set; } = string.Empty;
        public List<ItemMaterial> MaterialesPendientes { get; set; }= new List<ItemMaterial>();
        public List<ItemMaterial> MaterialesProcesados { get; set; } = new List<ItemMaterial>();

        public ReporteAceria()
        {
           
        }

        public class ItemMaterial
        {
            public int IdMaterial { get; set; } = 0;
            public string NombreMaterial { get; set; }= string.Empty;
            public decimal Peso { get; set; } = 0;

            public ItemMaterial(int idMaterial, string nombreMaterial, decimal peso)
            {
                IdMaterial = idMaterial;
                NombreMaterial = nombreMaterial;
                Peso = peso;
            }

         
        }
    }
}
