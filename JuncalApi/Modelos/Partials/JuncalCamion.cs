using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

    public partial class JuncalCamion
    {



    [NotMapped]
    public string? NombreTransportista { get; set; }

    [NotMapped]
    public string? DescripcionTipoCamion { get; set; }

    public JuncalCamion() 
    { 
    Isdeleted = false;
    }

    public JuncalCamion(int pId,string pPatente,string pMarca,int pTara,int pIdTransportista,
        int pIdInterno,int pIdTipoCamion): this()
    {
        Id=pId;
        Patente= pPatente;
        Marca=pMarca;
        Tara=pTara;
       
        IdTransportista=pIdTransportista;
        IdInterno=pIdInterno;
        IdTipoCamion=pIdTipoCamion;
    }

    public JuncalCamion(int pId, string pPatente, string pMarca, int pTara, int pIdTransportista,
        int pIdInterno, int pIdTipoCamion,string pNombreTransportista,string pDescripcionTipoCamion):this(pId, pPatente,pMarca,pTara,pIdTransportista,
        pIdInterno,pIdTipoCamion)
    {
       
        NombreTransportista=pNombreTransportista;
        DescripcionTipoCamion = pDescripcionTipoCamion;

    }



    }

