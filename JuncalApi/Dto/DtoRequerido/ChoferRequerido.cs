namespace JuncalApi.Dto.DtoRequerido
{
    public class ChoferRequerido
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public int Dni { get; set; }

        public string? Telefono { get; set; }
    }
}
