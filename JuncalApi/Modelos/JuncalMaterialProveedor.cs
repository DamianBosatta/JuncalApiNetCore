using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalMaterialProveedor
{
    public int Id { get; set; }

    public int IdMaterial { get; set; }

    public int IdProveedor { get; set; }

    public decimal? Precio { get; set; }

    public DateTime? Fecha { get; set; }

    public bool? Activo { get; set; }

    public bool Isdeleted { get; set; }

    public virtual JuncalMaterial IdMaterialNavigation { get; set; } = null!;

    public virtual JuncalProveedor IdProveedorNavigation { get; set; } = null!;
}
