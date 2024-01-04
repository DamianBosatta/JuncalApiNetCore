using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalOrden
{
    public int Id { get; set; }

    public int IdAceria { get; set; }

    public int? IdContrato { get; set; }

    public string Remito { get; set; } = null!;

    public int? IdCamion { get; set; }

    public int IdEstado { get; set; }

    public DateTime Fecha { get; set; }

    public bool? Isdeleted { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdAcoplado { get; set; }

    public string Observaciones { get; set; } = null!;

    public int? IdDireccionProveedor { get; set; }

    public bool? Facturado { get; set; }

    public DateTime? FechaFacturacion { get; set; }

    public int? IdUsuarioCreacion { get; set; }

    public int? IdUsuarioFacturacion { get; set; }

    public virtual JuncalAcerium IdAceriaNavigation { get; set; } = null!;

    public virtual JuncalAcoplado? IdAcopladoNavigation { get; set; }

    public virtual JuncalCamion? IdCamionNavigation { get; set; }

    public virtual JuncalContrato? IdContratoNavigation { get; set; }

    public virtual JuncalDireccionProveedor? IdDireccionProveedorNavigation { get; set; }

    public virtual JuncalEstado IdEstadoNavigation { get; set; } = null!;

    public virtual JuncalProveedor? IdProveedorNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioCreacionNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioFacturacionNavigation { get; set; }

    public virtual ICollection<JuncalCuentaCorrientePendiente> JuncalCuentaCorrientePendientes { get; } = new List<JuncalCuentaCorrientePendiente>();

    public virtual ICollection<JuncalOrdenMarterial> JuncalOrdenMarterials { get; } = new List<JuncalOrdenMarterial>();

    public virtual ICollection<JuncalPreFacturar> JuncalPreFacturars { get; } = new List<JuncalPreFacturar>();

    public virtual ICollection<JuncalRemitosReclamado> JuncalRemitosReclamados { get; } = new List<JuncalRemitosReclamado>();
}
