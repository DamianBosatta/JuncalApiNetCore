using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalPreFacturar
{
    public int Id { get; set; }

    public int IdOrden { get; set; }

    public int IdMaterialEnviado { get; set; }

    public int IdMaterialRecibido { get; set; }

    public decimal Peso { get; set; }

    public decimal PesoTara { get; set; }

    public decimal PesoBruto { get; set; }

    public decimal PesoNeto { get; set; }

    public bool Facturado { get; set; }

    public bool IsDelete { get; set; }

    public string Remito { get; set; } = null!;

    public int? IdUsuarioFacturacion { get; set; }

    public virtual JuncalOrdenMarterial IdMaterialEnviadoNavigation { get; set; } = null!;

    public virtual JuncalAceriaMaterial IdMaterialRecibidoNavigation { get; set; } = null!;

    public virtual JuncalOrden IdOrdenNavigation { get; set; } = null!;

    public virtual JuncalUsuario? IdUsuarioFacturacionNavigation { get; set; }
}
