using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalRemitosReclamado
{
    public int Id { get; set; }

    public int IdEstadoReclamo { get; set; }

    public int IdRemito { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Observacion { get; set; }

    public DateTime? FechaReclamo { get; set; }

    public string? ObservacionReclamo { get; set; }

    public DateTime? FechaFinalizado { get; set; }

    public string? ObservacionFinalizado { get; set; }

    public int? IdUsuarioReclamo { get; set; }

    public sbyte IsDeleted { get; set; }

    public int? IdUsuarioIngreso { get; set; }

    public int? IdUsuarioFinalizado { get; set; }

    public int IdAceria { get; set; }

    public virtual JuncalAcerium IdAceriaNavigation { get; set; } = null!;

    public virtual JuncalEstadosReclamo IdEstadoReclamoNavigation { get; set; } = null!;

    public virtual JuncalOrden IdRemitoNavigation { get; set; } = null!;

    public virtual JuncalUsuario? IdUsuarioFinalizadoNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioIngresoNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioReclamoNavigation { get; set; }
}
