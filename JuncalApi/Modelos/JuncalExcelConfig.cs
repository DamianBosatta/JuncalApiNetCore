using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalExcelConfig
{
    public int Id { get; set; }

    public int IdAceria { get; set; }

    public int Remito { get; set; }

    public int Fecha { get; set; }

    public int MaterialNombre { get; set; }

    public int MaterialCodigo { get; set; }

    public int Bruto { get; set; }

    public int Tara { get; set; }

    public int Descuento { get; set; }

    public int DescuentoDetalle { get; set; }

    public int Neto { get; set; }
}
