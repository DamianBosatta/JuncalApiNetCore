namespace JuncalApi.Dto.DtoRequerido
{
    public class MaterialProveedorRequerido
    {
        public int? IdMaterial { get; set; }

        public int? IdProveedor { get; set; }

        public decimal? Precio { get; set; }

        public DateTime? Fecha { get; set; }

        public bool? Activo { get; set; }


        public MaterialProveedorRequerido(int? idMaterial, int? idProveedor, decimal? precio, DateTime? fecha, bool? activo)
        {
            IdMaterial = idMaterial==0?null:idMaterial;
            IdProveedor = idProveedor==0? null: idProveedor;
            Precio = precio is null ? 0:precio;
            Fecha = fecha is null ? new DateTime():fecha;
            Activo = activo is null ? false : activo;
        }
    }
}
