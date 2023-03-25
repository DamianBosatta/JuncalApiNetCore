using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalTipoAcoplado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<JuncalAcoplado> JuncalAcoplados { get; } = new List<JuncalAcoplado>();
}
