namespace JuncalApi.Modelos
{
    public partial class JuncalPreFacturar
    {

        public JuncalPreFacturar() { Facturado = false;}

        public JuncalPreFacturar(int pIdOrden,int pIdMaterialEnviado,int pIdMaterialRecibido,decimal pPeso,
            decimal pPesoTara,decimal pPesoBruto,decimal pPesoNeto,string pRemito):this()
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
    }
}
