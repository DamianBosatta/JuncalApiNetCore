namespace JuncalApi.Dto.DtoRequerido
{
    public class RolesRequerido
    {
        public string? Nombre { get; set; }
        
        public RolesRequerido(string _nombre)
        {
            Nombre = _nombre is null ? string.Empty:_nombre;

        }
    }
}
