﻿using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

    public partial class JuncalCamion
    {

    [NotMapped]
    public string ? ApellidoChofer { get; set; }
    [NotMapped]
    public string? NombreChofer { get; set; }

    [NotMapped]
    public string? NombreTransportista { get; set; }

    [NotMapped]
    public string? DescripcionTipoCamion { get; set; }

    public JuncalCamion() 
    { 
    Isdeleted = false;
    }

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
        int pIdInterno, int pIdTipoCamion,string pApellidoChofer,string pNombreChofer,string pNombreTransportista,string pDescripcionTipoCamion):this(pId, pPatente,pMarca,pTara,pIdChofer,pIdTransportista,
        pIdInterno,pIdTipoCamion)
    {
        ApellidoChofer = pApellidoChofer;
        NombreChofer=pNombreChofer;
        NombreTransportista=pNombreTransportista;
        DescripcionTipoCamion = pDescripcionTipoCamion;

    }



    }

