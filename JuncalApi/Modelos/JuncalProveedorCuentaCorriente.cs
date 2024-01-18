using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalProveedorCuentaCorriente
{
    public int Id { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdTipoMovimiento { get; set; }

    public int? IdUsuario { get; set; }

    public DateTime? Fecha { get; set; }

    public bool? Isdeleted { get; set; }

    public decimal? Importe { get; set; }

    public double? Peso { get; set; }

    public int? IdMaterial { get; set; }

    public string? Observacion { get; set; }

    public int? IdRemitoInterno { get; set; }

    public int? IdRemitoExterno { get; set; }

    public bool MaterialBool { get; set; }

    public bool? Pendiente { get; set; }

    public virtual JuncalProveedorListapreciosMateriale? IdMaterialNavigation { get; set; }

    public virtual JuncalProveedor? IdProveedorNavigation { get; set; }

    public virtual JuncalCcTiposMovimiento? IdTipoMovimientoNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<JuncalCcMovimientoRemito> JuncalCcMovimientoRemitos { get; } = new List<JuncalCcMovimientoRemito>();
}
