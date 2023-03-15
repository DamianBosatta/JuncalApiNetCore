using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalTipoCamion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<JuncalCamion> JuncalCamions { get; } = new List<JuncalCamion>();
}
