using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

    public partial class JuncalAceriaMaterial
    {
    
    [NotMapped]
    public string? MaterialDescripcion { get; set; }

    public JuncalAceriaMaterial() 
    {
      Isdeleted = false;    
    }

    public JuncalAceriaMaterial(int pId,string pNombre,int pIdAceria,int pIdMaterial,string pCod)
    :this()
    {
        Id = pId;
        Nombre = pNombre;
        IdAceria = pIdAceria;
        IdMaterial = pIdMaterial;
        Cod=pCod;

    }
    public JuncalAceriaMaterial(int pId, string pNombre, int pIdAceria, int pIdMaterial,
    string pCod,string pMaterialDescripcion):this(pId,pNombre,pIdAceria,pIdMaterial,pCod)
    {
        MaterialDescripcion = pMaterialDescripcion;
    }
  }

