using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalAceriaMaterial
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdAceria { get; set; }

    public int IdMaterial { get; set; }

    public string Cod { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual JuncalAcerium IdAceriaNavigation { get; set; } = null!;

    public virtual JuncalMaterial IdMaterialNavigation { get; set; } = null!;

    public virtual ICollection<JuncalContratoItem> JuncalContratoItems { get; } = new List<JuncalContratoItem>();

    public virtual ICollection<JuncalPreFacturar> JuncalPreFacturars { get; } = new List<JuncalPreFacturar>();
}
