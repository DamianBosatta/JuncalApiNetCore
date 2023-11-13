using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalNotificacione
{
    public int Id { get; set; }

    public DateOnly Fecha { get; set; }

    public int CantidadContratos { get; set; }
}
