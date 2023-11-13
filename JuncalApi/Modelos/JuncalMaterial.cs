﻿using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalMaterial
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool Isdeleted { get; set; }

    public virtual ICollection<JuncalAceriaMaterial> JuncalAceriaMaterials { get; } = new List<JuncalAceriaMaterial>();

    public virtual ICollection<JuncalOrdenMarterial> JuncalOrdenMarterials { get; } = new List<JuncalOrdenMarterial>();

    public virtual ICollection<JuncalOrdenMaterialInternoRecibido> JuncalOrdenMaterialInternoRecibidos { get; } = new List<JuncalOrdenMaterialInternoRecibido>();

    public virtual ICollection<JuncalOrdenMaterialInternoRecogido> JuncalOrdenMaterialInternoRecogidos { get; } = new List<JuncalOrdenMaterialInternoRecogido>();

    public virtual ICollection<JuncalProveedorListapreciosMateriale> JuncalProveedorListapreciosMateriales { get; } = new List<JuncalProveedorListapreciosMateriale>();
}
