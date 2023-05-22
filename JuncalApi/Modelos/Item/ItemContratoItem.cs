namespace JuncalApi.Modelos.Item
{
    public class ItemContratoItem
    {
        public JuncalContratoItem ItemContrato { get; set; }

        public JuncalMaterial Material { get; set; }

        public ItemContratoItem() { }

        public ItemContratoItem(JuncalContratoItem _itemContrato,JuncalMaterial _material):this()
        { 
            ItemContrato = _itemContrato;
            Material = _material;
        
        }

        public int Id { get { return ItemContrato is null ? 0 : ItemContrato.Id; } }

        public int IdContrato { get { return ItemContrato is null ? 0 : ItemContrato.IdContrato; } }

        public int IdMaterial { get { return ItemContrato is null ? 0 : ItemContrato.IdMaterial; } }

        public decimal Precio { get { return ItemContrato is null ? 0 : ItemContrato.Precio; } }

        public string? MaterialDescripcion { get { return Material is null ? string.Empty : Material.Nombre is null ? string.Empty : Material.Nombre;} }
    }
}
