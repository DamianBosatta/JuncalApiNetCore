using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalCamion
{
    public int Id { get; set; }

    public string Patente { get; set; } = null!;

    public string? Marca { get; set; }

    public int? Tara { get; set; }

    public int? IdChofer { get; set; }

    public int? IdTransportista { get; set; }

    public int? IdInterno { get; set; }

    public bool? Isdeleted { get; set; }

    public int? IdTipoCamion { get; set; }

    public virtual JuncalChofer? IdChoferNavigation { get; set; }

    public virtual JuncalTipoCamion? IdTipoCamionNavigation { get; set; }

    public virtual JuncalTransportistum? IdTransportistaNavigation { get; set; }

    public virtual ICollection<JuncalOrden> JuncalOrdens { get; } = new List<JuncalOrden>();
}
