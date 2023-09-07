using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalCuentasCorriente
{
    public int Id { get; set; }

    public int IdProvedoor { get; set; }

    public int IdTipoMoviento { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public string Descripcion { get; set; } = null!;

    public decimal Importe { get; set; }

    public int IdUsuario { get; set; }

    public bool? Isdeleted { get; set; }
}
