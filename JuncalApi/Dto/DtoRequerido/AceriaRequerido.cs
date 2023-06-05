namespace JuncalApi.Dto.DtoRequerido
{
    public class AceriaRequerido
    {

        public string Nombre { get; set; } = string.Empty;

        public string? Direccion { get; set; }

        public string? Cuit { get; set; }

        public string? CodProveedor { get; set; }



        public AceriaRequerido(string nombre, string? direccion, string? cuit, string? codProveedor)
        {
            Nombre = nombre is null ? string.Empty : nombre ;
            Direccion = direccion is null ? string.Empty : direccion;
            Cuit = cuit is null ? string.Empty : cuit;
            CodProveedor = codProveedor is null ? string.Empty : codProveedor;
        }
    }
}
