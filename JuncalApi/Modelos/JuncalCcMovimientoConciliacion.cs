using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalCcMovimientoConciliacion
{
    public int Id { get; set; }

    public int? IdMovimiento { get; set; }

    public decimal? Importe { get; set; }

    public virtual JuncalProveedorCcMovimiento? IdMovimientoNavigation { get; set; }
}
