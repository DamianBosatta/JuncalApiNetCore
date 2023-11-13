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

    public virtual ICollection<JuncalOrdenInterno> JuncalOrdenInternos { get; } = new List<JuncalOrdenInterno>();

    public virtual ICollection<JuncalOrden> JuncalOrdens { get; } = new List<JuncalOrden>();

    public virtual ICollection<JuncalProveedorCcMovimiento> JuncalProveedorCcMovimientos { get; } = new List<JuncalProveedorCcMovimiento>();

    public virtual ICollection<JuncalProveedorListaprecio> JuncalProveedorListaprecios { get; } = new List<JuncalProveedorListaprecio>();
}
