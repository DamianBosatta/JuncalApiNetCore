using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalChofer
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Dni { get; set; }

    public string? Telefono { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<JuncalCamion> JuncalCamions { get; } = new List<JuncalCamion>();
}
