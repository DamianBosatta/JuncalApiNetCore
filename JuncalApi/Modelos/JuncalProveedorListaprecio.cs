using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalProveedorListaprecio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int? IdProveedor { get; set; }

    public DateTime? FechaVigencia { get; set; }

    public DateTime? FechaVencimiento { get; set; }

    public int? IdUsuario { get; set; }

    public bool? Activo { get; set; }

    public virtual JuncalProveedor? IdProveedorNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<JuncalProveedorListapreciosMateriale> JuncalProveedorListapreciosMateriales { get; } = new List<JuncalProveedorListapreciosMateriale>();
}
