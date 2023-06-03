using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalAcoplado
{
    public int Id { get; set; }

    public string Patente { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public string? Marca { get; set; }

    public string? Año { get; set; }

    public int IdTipo { get; set; }

    public virtual JuncalTipoAcoplado IdTipoNavigation { get; set; } = null!;

    public virtual ICollection<JuncalOrdenInterno> JuncalOrdenInternos { get; } = new List<JuncalOrdenInterno>();

    public virtual ICollection<JuncalOrden> JuncalOrdens { get; } = new List<JuncalOrden>();
}
