using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

    public partial class JuncalCamion
    {


    [NotMapped]
    public string NombreChofer { get; set; }

    [NotMapped]
    public string NombreTransportista { get; set; }

    [NotMapped]
    public string DescripcionTipoCamion { get; set; }

    public JuncalCamion() { }

    public JuncalCamion(int pId,string pPatente,string pMarca,int pTara,int pIdChofer,int pIdTransportista,
        int pIdInterno,int pIdTipoCamion): this()
    {
        Id=pId;
        Patente= pPatente;
        Marca=pMarca;
        Tara=pTara;
        IdChofer=pIdChofer;
        IdTransportista=pIdTransportista;
        IdInterno=pIdInterno;
        IdTipoCamion=pIdTipoCamion;
    }

    public JuncalCamion(int pId, string pPatente, string pMarca, int pTara, int pIdChofer, int pIdTransportista,
        int pIdInterno, int pIdTipoCamion,string pNombreChofer,string pNombreTransportista,string pDescripcionTipoCamion):this(pId, pPatente,pMarca,pTara,pIdChofer,pIdTransportista,
        pIdInterno,pIdTipoCamion)
    {
        NombreChofer=pNombreChofer;
        NombreTransportista=pNombreTransportista;
        DescripcionTipoCamion = pDescripcionTipoCamion;

    }



    }

