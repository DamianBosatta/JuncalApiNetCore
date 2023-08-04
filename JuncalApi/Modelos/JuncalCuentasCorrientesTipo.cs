using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalCuentasCorrientesTipo
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    /// <summary>
    /// 1- Suma 2 - Resta
    /// </summary>
    public int Tipo { get; set; }

    public bool? Isdeleted { get; set; }
}
