using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalProveedorPresupuestoMateriale
{
    public int Id { get; set; }

    public int IdPresupuesto { get; set; }

    public int IdMaterial { get; set; }

    public double PrecioCif { get; set; }

    public double PrecioFob { get; set; }

    public int Isdeleted { get; set; }

    public virtual JuncalMaterial IdMaterialNavigation { get; set; } = null!;

    public virtual JuncalProveedorPresupuesto IdPresupuestoNavigation { get; set; } = null!;
}
