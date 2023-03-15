using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalOrdenMarterial
{
    public int Id { get; set; }

    public int IdOrden { get; set; }

    public int IdMaterial { get; set; }

    public decimal? Peso { get; set; }

    public bool Isdeleted { get; set; }

    public virtual JuncalMaterial IdMaterialNavigation { get; set; } = null!;

    public virtual JuncalOrden IdOrdenNavigation { get; set; } = null!;
}
