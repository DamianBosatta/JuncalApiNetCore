using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalProveedorCcMovimiento
{
    public int Id { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdTipo { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime? Fecha { get; set; }

    public bool? Isdeleted { get; set; }

    public decimal? Importe { get; set; }

    public virtual JuncalProveedor? IdProveedorNavigation { get; set; }

    public virtual JuncalCcTiposMovimiento? IdTipoNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<JuncalCcMovimeintoAdelanto> JuncalCcMovimeintoAdelantos { get; } = new List<JuncalCcMovimeintoAdelanto>();

    public virtual ICollection<JuncalCcMovimientoConciliacion> JuncalCcMovimientoConciliacions { get; } = new List<JuncalCcMovimientoConciliacion>();

    public virtual ICollection<JuncalCcMovimientoRemito> JuncalCcMovimientoRemitos { get; } = new List<JuncalCcMovimientoRemito>();
}
