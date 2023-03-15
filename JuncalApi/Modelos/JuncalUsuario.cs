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

    public byte[] PasswordHash { get; set; }

    public int? IdRol { get; set; }

    public bool Isdeleted { get; set; }

    public byte[] PasswordSalt { get; set; } = null!;

    public virtual JuncalRole? IdRolNavigation { get; set; }
}
