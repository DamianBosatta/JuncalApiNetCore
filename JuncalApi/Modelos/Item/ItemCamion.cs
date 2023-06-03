using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos.Item
{
    public class ItemCamion
    {
        public JuncalCamion Camion { get; set; }

        public JuncalChofer Chofer { get; set; }

        public JuncalTransportistum Transportista { get; set; }

        public JuncalTipoCamion TipoCamion { get; set; }
        
       
    
        public ItemCamion() { }

       public ItemCamion(JuncalCamion pCamion, JuncalChofer pJuncalChofer
       ,JuncalTransportistum pJuncalTransportista,JuncalTipoCamion pTipoCamion):this()
        { 
        Camion = pCamion;
        Chofer = pJuncalChofer;
        Transportista = pJuncalTransportista;
        TipoCamion = pTipoCamion;             
        }

        public int Id { get { return Camion is null ? 0 : (int)Camion.Id; } }

        public string Patente { get {return Camion is null ? string.Empty:Camion.Patente is null ? string.Empty:Camion.Patente;} }

        public string Marca { get { return Camion is null ? string.Empty : Camion.Marca is null ? string.Empty: Camion.Marca; } }

        public int Tara { get { return (int)(Camion is null ? 0 : Camion.Tara is null ? 0:Camion.Tara); } }

        public int IdChofer { get { return (int)(Camion is null ? 0 : Camion.IdChofer is null ? 0 : Camion.IdChofer); } }

        public int IdTransportista { get { return (int)(Camion is null ? 0 : Camion.IdTransportista is null ? 0 : Camion.IdTransportista); } }

        public int IdInterno { get { return (int)(Camion is null ? 0 : Camion.IdInterno is null ? 0 : Camion.IdInterno); } }

        public int IdTipoCamion { get { return (int)(Camion is null ? 0 : Camion.IdTipoCamion is null ? 0 : Camion.IdTipoCamion); } }
    
        public string NombreChofer { get{ return Chofer is null ? string.Empty : Chofer.Nombre; } }

        public string NombreTransportista { get { return Transportista is null ? string.Empty : Transportista.Nombre; } }

        public string DescripcionTipoCamion { get { return TipoCamion is null ? string.Empty : TipoCamion.Nombre; } }

    }
}
