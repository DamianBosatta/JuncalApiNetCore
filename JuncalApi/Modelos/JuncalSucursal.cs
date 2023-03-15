using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalSucursal
{
    public int Id { get; set; }

    public int Numero { get; set; }

    public string Nombre { get; set; } = null!;
}
