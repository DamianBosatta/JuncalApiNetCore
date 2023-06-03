using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalOrdenMaterialInternoRecibido
{
    public int Id { get; set; }

    public int IdOrdenInterno { get; set; }

    public int IdMaterial { get; set; }

    public decimal Peso { get; set; }

    public bool Isdeleted { get; set; }

    public virtual JuncalMaterial IdMaterialNavigation { get; set; } = null!;

    public virtual JuncalOrdenInterno IdOrdenInternoNavigation { get; set; } = null!;
}
