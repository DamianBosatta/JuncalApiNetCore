using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalEstadosReclamo
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Isdelete { get; set; }

    public virtual ICollection<JuncalRemitosReclamado> JuncalRemitosReclamados { get; } = new List<JuncalRemitosReclamado>();
}
