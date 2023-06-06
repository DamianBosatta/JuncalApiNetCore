

namespace JuncalApi.Modelos;

public partial class JuncalOrdenInterno
{
    public int Id { get; set; }

    public int? IdAceria { get; set; }

    public int? IdContrato { get; set; }

    public string Remito { get; set; } = null!;

    public int? IdCamion { get; set; }

    public int? IdEstadoInterno { get; set; }

    public DateTime Fecha { get; set; }

    public bool? Isdeleted { get; set; }

    public string? Observaciones { get; set; } = null!;

    public int? IdDireccionProveedor { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdAcoplado { get; set; }

    public virtual JuncalAcerium IdAceriaNavigation { get; set; } = null!;

    public virtual JuncalAcoplado IdAcopladoNavigation { get; set; } = null!;

    public virtual JuncalCamion IdCamionNavigation { get; set; } = null!;

    public virtual JuncalContrato IdContratoNavigation { get; set; } = null!;

    public virtual JuncalDireccionProveedor IdDireccionProveedorNavigation { get; set; } = null!;

    public virtual JuncalEstadosInterno IdEstadoInternoNavigation { get; set; } = null!;

    public virtual JuncalProveedor IdProveedorNavigation { get; set; } = null!;

    public virtual ICollection<JuncalOrdenMaterialInternoRecibido> JuncalOrdenMaterialInternoRecibidos { get; } = new List<JuncalOrdenMaterialInternoRecibido>();

    public virtual ICollection<JuncalOrdenMaterialInternoRecogido> JuncalOrdenMaterialInternoRecogidos { get; } = new List<JuncalOrdenMaterialInternoRecogido>();
}
