using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalEstado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Isdeleted { get; set; }

    public virtual ICollection<JuncalOrden> JuncalOrdens { get; } = new List<JuncalOrden>();
}
