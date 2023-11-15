using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

public partial class JuncalProveedorCcMovimiento
{
    [NotMapped]
    public string NombreTipo { get; set; } = string.Empty;

    public JuncalProveedorCcMovimiento() { }

}
