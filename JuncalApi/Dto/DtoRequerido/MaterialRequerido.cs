namespace JuncalApi.Dto.DtoRequerido
{
    public class MaterialRequerido
    {       
       public string Nombre { get; set; } 

        public MaterialRequerido(string nombre)
        {
            Nombre = nombre is null ? string.Empty:nombre;
        }
    }
}
