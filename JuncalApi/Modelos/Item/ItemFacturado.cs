namespace JuncalApi.Modelos.Item
{
    public class ItemFacturado
    {
        public int IdOrden { get; set; }

        public int IdAceria { get; set; }

        public string Contrato { get; set; }

        public string Aceria { get; set; }

        public string Remito { get; set; }

        public decimal PesoEnviado { get; set; }

        public decimal PesoRecibido { get; set; }

        public List<JuncalOrdenMarterial> ListaMateriales { get; set; }



        
    }
}
