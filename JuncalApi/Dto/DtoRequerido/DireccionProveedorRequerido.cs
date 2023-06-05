namespace JuncalApi.Dto.DtoRequerido
{
    public class DireccionProveedorRequerido
    {   
        public string? Direccion { get; set; }

        public int? IdProveedor { get; set; }

        public DireccionProveedorRequerido(string? direccion, int? idProveedor)
        {
            Direccion = direccion is null ? string.Empty: direccion;
            IdProveedor = idProveedor ==0? null : idProveedor;
        }
    }
}
