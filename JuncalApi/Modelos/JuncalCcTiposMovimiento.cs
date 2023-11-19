using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalCcTiposMovimiento
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<JuncalProveedorCuentaCorriente> JuncalProveedorCuentaCorrientes { get; } = new List<JuncalProveedorCuentaCorriente>();
}
