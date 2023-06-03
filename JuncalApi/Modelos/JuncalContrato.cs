using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalContrato
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Numero { get; set; }

    public DateTime? FechaVigencia { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public int? IdAceria { get; set; }

    public bool Activo { get; set; }

    public bool Isdeleted { get; set; }

    public decimal ValorFlete { get; set; }

    public virtual JuncalAcerium? IdAceriaNavigation { get; set; }

    public virtual ICollection<JuncalContratoItem> JuncalContratoItems { get; } = new List<JuncalContratoItem>();

    public virtual ICollection<JuncalOrdenInterno> JuncalOrdenInternos { get; } = new List<JuncalOrdenInterno>();

    public virtual ICollection<JuncalOrden> JuncalOrdens { get; } = new List<JuncalOrden>();
}
