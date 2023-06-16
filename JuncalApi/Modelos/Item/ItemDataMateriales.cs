namespace JuncalApi.Modelos.Item
{
    public class ItemDataMateriales
    {
        public JuncalAceriaMaterial AceriaMaterial { get; set; }

        public JuncalOrdenMarterial OrdenMaterial { get; set; }



        public ItemDataMateriales() { }


        public ItemDataMateriales(JuncalAceriaMaterial _aceriaMaterial, JuncalOrdenMarterial _ordenMaterial)
        {
            AceriaMaterial = _aceriaMaterial;
            OrdenMaterial = _ordenMaterial;

        }

        public int Id { get { return AceriaMaterial is null ? 0 : AceriaMaterial.Id; } }

        public int IdOrden { get { return OrdenMaterial is null ? 0 : OrdenMaterial.IdOrden; } }

        public string Nombre { get { return AceriaMaterial is null ? string.Empty : AceriaMaterial.Nombre is null ? string.Empty : AceriaMaterial.Nombre; } }

        public int IdAceria { get { return AceriaMaterial is null ? 0 : AceriaMaterial.IdAceria; } }

        public int IdMaterial { get { return AceriaMaterial is null ? 0 : AceriaMaterial.IdMaterial; } }

        public string Cod { get { return AceriaMaterial is null ? string.Empty : AceriaMaterial.Cod is null ? string.Empty : AceriaMaterial.Cod; } }      

        public decimal Peso { get { return (decimal)(OrdenMaterial is null ? 0 : OrdenMaterial.Peso); } }
    }
}
