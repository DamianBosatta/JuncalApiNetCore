using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalContratoItem
{
    public int Id { get; set; }

    public int IdContrato { get; set; }

    public bool Isdeleted { get; set; }

    public int IdMaterial { get; set; }

    public decimal Precio { get; set; }

    public virtual JuncalContrato IdContratoNavigation { get; set; } = null!;

    public virtual JuncalAceriaMaterial IdMaterialNavigation { get; set; } = null!;
}
