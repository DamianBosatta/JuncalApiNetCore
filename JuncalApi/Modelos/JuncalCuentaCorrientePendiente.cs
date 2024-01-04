using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalCuentaCorrientePendiente
{
    public int Id { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdMaterial { get; set; }

    public int? IdRemito { get; set; }

    public int? IdUsuario { get; set; }

    public decimal? Kg { get; set; }

    public bool? Pendiente { get; set; }

    public virtual JuncalProveedorListapreciosMateriale? IdMaterialNavigation { get; set; }

    public virtual JuncalProveedor? IdProveedorNavigation { get; set; }

    public virtual JuncalOrden? IdRemitoNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioNavigation { get; set; }
}
