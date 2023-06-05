namespace JuncalApi.Dto.DtoRequerido
{
    public class TipoAcopladoRequerido
    {       
        public string Nombre { get; set; }= string.Empty;

        public TipoAcopladoRequerido(string nombre)
        {
            Nombre = nombre is null ? string.Empty:nombre;
        }
    }
}
