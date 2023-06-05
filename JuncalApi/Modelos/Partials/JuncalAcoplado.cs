using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

    public partial class JuncalAcoplado
    {
    [NotMapped]
     public string TipoAcoplado { get; set; }

    public JuncalAcoplado() 
    { 
    Isdeleted = false;
    }

    public JuncalAcoplado(int pId,string pPatente,string pMarca, string pAño,int pIdTipo):this()
    {
        Id = pId;
        Patente = pPatente;
        Marca = pMarca;
        Año=pAño;
        IdTipo = pIdTipo;

    }

    public JuncalAcoplado(int pId, string pPatente, string pMarca, string pAño, int pIdTipo,string pTipoAcoplado) :this(pId,pPatente,pMarca,pAño,pIdTipo)
    { 
    
    TipoAcoplado=pTipoAcoplado;
    
    }


    }

