using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalCcMovimeintoAdelanto
{
    public int Id { get; set; }

    public decimal? Importe { get; set; }

    public int? IdMovimiento { get; set; }

    public virtual JuncalProveedorCcMovimiento? IdMovimientoNavigation { get; set; }
}
