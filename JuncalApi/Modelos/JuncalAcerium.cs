using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalAcerium
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Cuit { get; set; }

    public bool? Isdeleted { get; set; }

    public string? CodProveedor { get; set; }

    public virtual ICollection<JuncalAceriaMaterial> JuncalAceriaMaterials { get; } = new List<JuncalAceriaMaterial>();

    public virtual ICollection<JuncalContrato> JuncalContratos { get; } = new List<JuncalContrato>();

    public virtual ICollection<JuncalOrden> JuncalOrdens { get; } = new List<JuncalOrden>();
}
