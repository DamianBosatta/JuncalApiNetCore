using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos
{
    public partial class JuncalOrdenMarterial
    {

      

        public JuncalOrdenMarterial()
        {
          Isdeleted = false;
        }

        public JuncalOrdenMarterial(int id, int _idMaterial, decimal _peso,string numFactura,bool facturadoParcial) : this()
        {
        
            Id = id;
            IdMaterial = _idMaterial;
            Peso = _peso;
           NumFactura = numFactura;
           FacturadoParcial =facturadoParcial;

    }
    }
}
