namespace JuncalApi.Dto.DtoRespuesta
{
    public class ChoferRespuesta
    {
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Dni { get; set; }

    public string? Telefono { get; set; }
}
}
