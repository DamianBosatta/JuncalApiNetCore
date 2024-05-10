using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalOrdenInterno
{
    public int Id { get; set; }

    public int IdAceria { get; set; }

    public int? IdContrato { get; set; }

    public string Remito { get; set; } = null!;

    public int? IdCamion { get; set; }

    public int IdChofer { get; set; }

    public int IdEstadoInterno { get; set; }

    public DateTime Fecha { get; set; }

    public bool Isdeleted { get; set; }

    public string Observaciones { get; set; } = null!;

    public int? IdDireccionProveedor { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdAcoplado { get; set; }

    public virtual JuncalAcoplado? IdAcopladoNavigation { get; set; }

    public virtual JuncalCamion? IdCamionNavigation { get; set; }

    public virtual JuncalDireccionProveedor? IdDireccionProveedorNavigation { get; set; }

    public virtual JuncalProveedor? IdProveedorNavigation { get; set; }

    public virtual ICollection<JuncalOrdenMaterialInternoRecibido> JuncalOrdenMaterialInternoRecibidos { get; } = new List<JuncalOrdenMaterialInternoRecibido>();

    public virtual ICollection<JuncalOrdenMaterialInternoRecogido> JuncalOrdenMaterialInternoRecogidos { get; } = new List<JuncalOrdenMaterialInternoRecogido>();
}
