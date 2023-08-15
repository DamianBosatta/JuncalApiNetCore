using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalFacturaMateriale
{
    public int IdFactura { get; set; }

    public string NombreMaterial { get; set; } = null!;

    public decimal Peso { get; set; }

    public decimal SubTota { get; set; }

    public int Id { get; set; }

    public virtual JuncalFactura IdFacturaNavigation { get; set; } = null!;
}
