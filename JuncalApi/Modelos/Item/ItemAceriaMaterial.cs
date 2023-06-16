namespace JuncalApi.Modelos.Item
{
    public class ItemAceriaMaterial
    {

        public JuncalAceriaMaterial AceriaMaterial { get; set; }

        public JuncalMaterial Material { get; set; }

      

        public ItemAceriaMaterial() { }


        public ItemAceriaMaterial(JuncalAceriaMaterial _aceriaMaterial , JuncalMaterial _material)
        {
            AceriaMaterial = _aceriaMaterial;
            Material = _material;

        }

        public int Id { get { return AceriaMaterial is null ? 0 : AceriaMaterial.Id; } }

        public string Nombre { get { return AceriaMaterial is null ? string.Empty : AceriaMaterial.Nombre is null ? string.Empty : AceriaMaterial.Nombre; } }

        public int IdAceria { get { return AceriaMaterial is null ? 0: AceriaMaterial.IdAceria;} }

        public int IdMaterial { get { return AceriaMaterial is null ? 0 : AceriaMaterial.IdMaterial; } }

        public string Cod { get { return AceriaMaterial is null ? string.Empty : AceriaMaterial.Cod is null ? string.Empty : AceriaMaterial.Cod; } }

        public string? MaterialDescripcion { get { return Material is null ? string.Empty : Material.Nombre is null ? string.Empty : Material.Nombre; } }

    }
}
