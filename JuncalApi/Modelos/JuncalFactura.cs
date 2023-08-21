using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalFactura
{
    public int Id { get; set; }

    public string Destinatario { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Cuit { get; set; } = null!;

    public string ContratoNumero { get; set; } = null!;

    public string ContratoNombre { get; set; } = null!;

    public string NumeroFactura { get; set; } = null!;

    public string Fecha { get; set; } = null!;

    public decimal TotalFactura { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public virtual ICollection<JuncalFacturaMateriale> JuncalFacturaMateriales { get; } = new List<JuncalFacturaMateriale>();


}
