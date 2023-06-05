using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos
{
    public partial class JuncalOrdenMaterialInternoRecogido
    {
  
        [NotMapped]
        public string? NombreMaterial { get; set; }

        public JuncalOrdenMaterialInternoRecogido()
        {
            Isdeleted = false;
        }

        public JuncalOrdenMaterialInternoRecogido(int _idOrdenInterno,int _idMaterial , decimal _peso):this()
        {
            IdOrdenInterno = _idOrdenInterno;
            IdMaterial = _idMaterial;
            Peso = _peso;
        }

        public JuncalOrdenMaterialInternoRecogido(int _idOrdenInterno, int _idMaterial, decimal _peso,string _nombreMaterial) : this(_idOrdenInterno, _idMaterial, _peso)
        {
            NombreMaterial = _nombreMaterial;
        }

    }
}
