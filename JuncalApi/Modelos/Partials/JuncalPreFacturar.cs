using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos
{
    public partial class JuncalPreFacturar
    {
        [NotMapped]
        public int IdAceria { get; set; } = 0;
        [NotMapped]
        public int IdContrato{ get; set; } = 0;
        [NotMapped]
        public JuncalAcerium Aceria { get; set; }
        [NotMapped]
        public string NombreContrato { get; set; }
        [NotMapped]
        public string NumeroContrato { get; set; }
        [NotMapped]
        public string NombreMaterial { get; set; }
        [NotMapped]
        public decimal PrecioMaterial { get; set; }
        [NotMapped]
        public string? NombreUsuario { get; set; }


        public JuncalPreFacturar() { Facturado = false;}

        public JuncalPreFacturar(int pIdOrden,int pIdMaterialEnviado,int pIdMaterialRecibido,decimal pPeso,
            decimal pPesoTara,decimal pPesoBruto,decimal pPesoNeto,string pRemito) :this()
        {         
          IdOrden = pIdOrden;
          IdMaterialEnviado = pIdMaterialEnviado;
          IdMaterialRecibido= pIdMaterialRecibido;
          Peso= pPeso;
          PesoTara= pPesoTara;
          PesoBruto= pPesoBruto;
          PesoNeto= pPesoNeto;
          Remito = pRemito;
            
        }

        public JuncalPreFacturar(int pIdOrden, int pIdMaterialEnviado, int pIdMaterialRecibido, decimal pPeso,
       decimal pPesoTara, decimal pPesoBruto, decimal pPesoNeto, string pRemito,int pIdAceria,int pIdContrato,int id) : this(pIdOrden,  pIdMaterialEnviado,  pIdMaterialRecibido,  pPeso,
       pPesoTara, pPesoBruto,pPesoNeto, pRemito)
        {
            IdAceria= pIdAceria;
            IdContrato= pIdContrato;
            Id=id;
           
        }
    }
}
