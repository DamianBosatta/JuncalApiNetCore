using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalRole
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<JuncalUsuario> JuncalUsuarios { get; } = new List<JuncalUsuario>();
}
