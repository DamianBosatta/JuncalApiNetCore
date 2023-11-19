using System.ComponentModel.DataAnnotations.Schema;

namespace JuncalApi.Modelos;

public partial class JuncalProveedorCuentaCorriente
{
    [NotMapped]
    public string NombreTipo { get; set; } = string.Empty;

    public JuncalProveedorCuentaCorriente() { }

}
