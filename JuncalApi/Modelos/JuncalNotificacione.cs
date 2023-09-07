using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalNotificacione
{
    public DateOnly Fecha { get; set; }

    public int id { get; set; }

    public int cantidadContratos { get; set; }
}
