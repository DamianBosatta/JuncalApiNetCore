using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

    public partial class JuncalContrato
    {
    [NotMapped]
    public string NombreAceria { get; set; }

    public JuncalContrato() 
    { 
    Isdeleted = false;
    
    }


    public JuncalContrato(int pId, string pNombre,string pNumero,DateTime pFechaVigencia, 
    DateTime pFechaVencimiento,int pIdAceria,bool pActivo,decimal pValorFlete ):this()
    {
        Id = pId;
        Nombre= pNombre;
        Numero= pNumero;
        FechaVigencia= pFechaVigencia;
        FechaVencimiento= pFechaVencimiento;
        IdAceria= pIdAceria;
        Activo= pActivo;
        ValorFlete= pValorFlete;

    }


    public JuncalContrato(int pId, string pNombre, string pNumero, DateTime pFechaVigencia,
    DateTime pFechaVencimiento, int pIdAceria, bool pActivo, decimal pValorFlete,string pNombreAceria):
    this(pId,pNombre,pNumero,pFechaVigencia,pFechaVencimiento, pIdAceria,pActivo,pValorFlete)
    {
       
        NombreAceria= pNombreAceria;

    }

    }

