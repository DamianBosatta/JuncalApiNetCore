using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalProveedor
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Origen { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<JuncalDireccionProveedor> JuncalDireccionProveedors { get; } = new List<JuncalDireccionProveedor>();

    public virtual ICollection<JuncalMaterialProveedor> JuncalMaterialProveedors { get; } = new List<JuncalMaterialProveedor>();

    public virtual ICollection<JuncalOrden> JuncalOrdens { get; } = new List<JuncalOrden>();
}
