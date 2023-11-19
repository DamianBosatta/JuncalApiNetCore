using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalCcMovimientoRemito
{
    public int Id { get; set; }

    public int? IdMovimiento { get; set; }

    public int? IdMaterial { get; set; }

    public decimal? Pesaje1 { get; set; }

    public decimal? Pesaje2 { get; set; }

    public decimal? Finalizado { get; set; }

    public int? IdRemito { get; set; }

    public virtual JuncalProveedorCuentaCorriente? IdMovimientoNavigation { get; set; }
}
