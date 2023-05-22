using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

public partial class JuncalAcerium
{
    [NotMapped]
    public string NombreAceria { get; set; }

    public JuncalAcerium() { }

    public JuncalAcerium(int pId, string pNombre, string pDireccion, string pCuit, string pCodProveedor)
    :this()
    {
        Id = pId;
        Nombre = pNombre;
        Direccion = pDireccion;
        Cuit = pCuit;
        CodProveedor = pCodProveedor;

    }

    public JuncalAcerium(int pId, string pNombre, string pDireccion, string pCuit, string pCodProveedor,
    string pNombreAceria) : this(pId, pNombre, pDireccion, pCuit, pCodProveedor)
    {
        NombreAceria = pNombreAceria;

    }

}