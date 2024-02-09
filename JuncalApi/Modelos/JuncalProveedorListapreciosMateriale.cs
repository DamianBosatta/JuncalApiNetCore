using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalProveedorListapreciosMateriale
{
    public int? IdProveedorListaprecios { get; set; }

    public int? IdMaterialJuncal { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public int Id { get; set; }

    public virtual JuncalMaterial? IdMaterialJuncalNavigation { get; set; }

    public virtual JuncalProveedorListaprecio? IdProveedorListapreciosNavigation { get; set; }

    public virtual ICollection<JuncalProveedorCuentaCorriente> JuncalProveedorCuentaCorrientes { get; } = new List<JuncalProveedorCuentaCorriente>();
}
