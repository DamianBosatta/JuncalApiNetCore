namespace JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido
{
    public class Referencia
    {
        public int IdOrden { get; set; }

        public List<ReferenciaMaterialesEnviados> MaterialesEnviados { get; set; }= new List<ReferenciaMaterialesEnviados>();
    }

    public class ReferenciaMaterialesEnviados
    {
        public int idMaterial { get; set; }

        public int idPrefactura { get; set; }


    }

    public class Factura
    {
        public string Destinatario { get; set; }

        public string Direccion { get;set;}

        public string Cuit { get;set;}

        public string ContratoNumero { get; set; }  

        public string ContratoNombre { get;set;}

        public string NumeroFactura { get;set;}

        public string Fecha { get;set;}

        public string TotalFactura { get;set;}

        public string NombreUsuario { get;set;}


       public List<Materiales> listaMateriales { get; set; }


    }

    public class Materiales
    {


       public string NombreMaterial { get; set; }

       public decimal Peso { get; set; }

      public  decimal SubTotal { get; set; }

      




    }
}
