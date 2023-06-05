namespace JuncalApi.Dto.DtoRequerido
{
    public class TransportistaRequerido
    {
        public string? Nombre { get; set; }

        public string? Cuit { get; set; }

        public TransportistaRequerido(string? nombre, string? cuit)
        {
            Nombre = nombre is null ? string.Empty:nombre;
            Cuit = cuit is null ? string.Empty:cuit;
        }
    }
}
