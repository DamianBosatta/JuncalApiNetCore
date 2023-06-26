using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

public partial class JuncalOrden
{
    [NotMapped]
    public string? NombreAceria { get; set; }
    [NotMapped]
    public string? DireccionAceria {get;set;}
    [NotMapped]
    public string? CuitAceria {get;set;}
    [NotMapped]
    public string? CodigoProveedorAceria { get; set; }
    [NotMapped]
    public string? NumeroContrato {get;set;}
    [NotMapped]
    public string? PatenteCamion { get; set; }
    [NotMapped]
    public int? IdChofer { get; set; }
    [NotMapped]
    public string? NombreChofer { get;set;}
    [NotMapped]
    public string? ApellidoChofer { get; set; }
    [NotMapped]
    public int? LicenciaChofer { get;set;}
    [NotMapped]
    public int? IdTransportista { get; set; }
    [NotMapped]
    public string? NombreTransportista { get; set; }
    [NotMapped]
    public string? PatenteAcoplado { get; set; }
    [NotMapped]
    public string? DescripcionEstado { get; set; }
    [NotMapped]
    public string? NombreProveedor { get; set; }
    [NotMapped]
    public string? DireccionProveedor { get; set; }


    public JuncalOrden() 
    {
        Isdeleted = false;
    
    }


    public JuncalOrden(int pId,int pIdAceria,int pIdContrato,string pRemito,int pIdCamion,int pIdEstado,
    DateTime pFecha,int pIdProveedor,int pIdAcoplado, string pObservaciones,int pIdDireccionProveedor) :this()
    {
        Id = pId;
        IdAceria = pIdAceria;
        IdContrato = pIdContrato;
        Remito = pRemito;
        IdCamion = pIdCamion;      
        IdEstado = pIdEstado;
        Fecha=pFecha;
        IdProveedor = pIdProveedor;
        IdAcoplado = pIdAcoplado;
        Observaciones = pObservaciones;
        IdDireccionProveedor= pIdDireccionProveedor;

    }

    public JuncalOrden(int pId, int pIdAceria, int pIdContrato, string pRemito,
    int pIdCamion, int pIdEstado,DateTime pFecha, int pIdProveedor, int pIdAcoplado,
    string pObservaciones, int pIdDireccionProveedor ,string pNombreAceria,
    string pDireccionAceria,string pCuitAceria, string pCodigoProveedorAceria,
    string pNumeroContrato,string pPatenteCamion,string pNombreChofer,int pLicenciaChofer,
    string pNombreTransportista,string pPatenteAcoplado,string pDescripcionEstado,
    string pNombreProveedor,string pDireccionProveedor, string pApellidoChofer,int pIdChofer,int pIdTransportista):this( pId, pIdAceria,  pIdContrato,pRemito,
     pIdCamion, pIdEstado, pFecha, pIdProveedor,pIdAcoplado,
    pObservaciones,pIdDireccionProveedor)
    {
        NombreAceria = pNombreAceria;
        DireccionAceria = pDireccionAceria;
        CuitAceria = pCuitAceria;
        CodigoProveedorAceria = pCodigoProveedorAceria;
        NumeroContrato = pNumeroContrato;
        PatenteCamion = pPatenteCamion;
        NombreChofer = pNombreChofer;
        LicenciaChofer = pLicenciaChofer;
        NombreTransportista = pNombreTransportista;
        PatenteAcoplado = pPatenteAcoplado;
        DescripcionEstado = pDescripcionEstado;
        NombreProveedor = pNombreProveedor;
        DireccionProveedor = pDireccionProveedor;
        ApellidoChofer = pApellidoChofer;
        IdChofer = pIdChofer;
        IdTransportista = pIdTransportista;
    }








    }

