﻿namespace JuncalApi.Modelos.Item
{
    public class ItemDataMateriales
    {
        public JuncalAceriaMaterial AceriaMaterial { get; set; }

        public JuncalOrdenMarterial OrdenMaterial { get; set; }

        public JuncalOrden Orden { get; set; }

        public JuncalMaterial Material { get; set; }



        public ItemDataMateriales() { }


        public ItemDataMateriales(JuncalAceriaMaterial _aceriaMaterial, JuncalOrdenMarterial _ordenMaterial,JuncalOrden _orden, JuncalMaterial _material)
        {
            AceriaMaterial = _aceriaMaterial;
            OrdenMaterial = _ordenMaterial;
            Orden = _orden;
            Material = _material;
        }


        public int IdOrden => Orden?.Id ?? 0;
       
        public string Remito => Orden?.Remito ?? string.Empty;
       
        public int IdMaterial => AceriaMaterial?.IdMaterial ?? 0;
        
        public string NombreMaterialJuncal => Material?.Nombre ?? string.Empty;
        
        public string NombreMaterialAceria => AceriaMaterial?.Nombre ?? string.Empty;
       
        public int IdAceria => AceriaMaterial?.IdAceria ?? 0;
      
        public string Cod => AceriaMaterial?.Cod ?? string.Empty;
       
        public decimal PesoEnviado => OrdenMaterial?.Peso ?? 0;
    }
}
