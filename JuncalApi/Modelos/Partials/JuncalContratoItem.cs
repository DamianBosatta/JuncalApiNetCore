using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

    public partial class JuncalContratoItem
    {
    [NotMapped]
    public string MaterialDescripcion { get; set; }

    public JuncalContratoItem() 
    { 
    Isdeleted = false;
    }


    public JuncalContratoItem(int pId,int pIdContrato,int pIdMaterial,decimal pPrecio):this()
    {
        Id = pId;
        IdContrato = pIdContrato;
        IdMaterial = pIdMaterial;
        Precio = pPrecio;
    }

    public JuncalContratoItem(int pId, int pIdContrato, int pIdMaterial, decimal pPrecio,string pMaterialDescripcion): 
    this(pId,pIdContrato,pIdMaterial,pPrecio)
    {
        MaterialDescripcion=pMaterialDescripcion;

    }



    }

