using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalTransportistum
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Cuit { get; set; } = null!;

    public bool? Isdeleted { get; set; }

    public virtual ICollection<JuncalCamion> JuncalCamions { get; } = new List<JuncalCamion>();
}
