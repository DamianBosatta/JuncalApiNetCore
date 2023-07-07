using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalProveedorPresupuesto
{
    public int Id { get; set; }

    public DateTime FechaActualizacion { get; set; }

    public bool IsDeleted { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdAceria { get; set; }

    public int? IdUsuario { get; set; }

    public virtual JuncalAcerium? IdAceriaNavigation { get; set; }

    public virtual JuncalProveedor? IdProveedorNavigation { get; set; }

    public virtual JuncalUsuario? IdUsuarioNavigation { get; set; }

    public virtual ICollection<JuncalProveedorPresupuestoMateriale> JuncalProveedorPresupuestoMateriales { get; } = new List<JuncalProveedorPresupuestoMateriale>();
}
