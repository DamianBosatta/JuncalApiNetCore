namespace JuncalApi.Dto.DtoRequerido
{
    public class TipoCamionRequerido
    {     
        public string Nombre { get; set; } = string.Empty;

        public TipoCamionRequerido(string nombre)
        {
            Nombre = nombre is null ? string.Empty:nombre;
        }
    }
}
