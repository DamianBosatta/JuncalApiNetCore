using System;
using System.Collections.Generic;

namespace JuncalApi.Modelos;

public partial class JuncalUsuario
{
    public int Id { get; set; }

    public string Usuario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Dni { get; set; }

    public string Email { get; set; } = null!;

    public byte[] PasswordHash { get; set; } = null!;

    public int? IdRol { get; set; }

    public bool Isdeleted { get; set; }

    public byte[] PasswordSalt { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime? TokenCreated { get; set; }

    public DateTime? TokenExpires { get; set; }

    public virtual JuncalRole? IdRolNavigation { get; set; }

    public virtual ICollection<JuncalOrden> JuncalOrdenIdUsuarioCreacionNavigations { get; } = new List<JuncalOrden>();

    public virtual ICollection<JuncalOrden> JuncalOrdenIdUsuarioFacturacionNavigations { get; } = new List<JuncalOrden>();

    public virtual ICollection<JuncalPreFacturar> JuncalPreFacturars { get; } = new List<JuncalPreFacturar>();

    public virtual ICollection<JuncalProveedorCcMovimiento> JuncalProveedorCcMovimientos { get; } = new List<JuncalProveedorCcMovimiento>();

    public virtual ICollection<JuncalProveedorListaprecio> JuncalProveedorListaprecios { get; } = new List<JuncalProveedorListaprecio>();

    public virtual ICollection<JuncalRemitosReclamado> JuncalRemitosReclamadoIdUsuarioFinalizadoNavigations { get; } = new List<JuncalRemitosReclamado>();

    public virtual ICollection<JuncalRemitosReclamado> JuncalRemitosReclamadoIdUsuarioIngresoNavigations { get; } = new List<JuncalRemitosReclamado>();

    public virtual ICollection<JuncalRemitosReclamado> JuncalRemitosReclamadoIdUsuarioReclamoNavigations { get; } = new List<JuncalRemitosReclamado>();
}
