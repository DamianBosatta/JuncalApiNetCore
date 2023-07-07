namespace JuncalApi.Dto.DtoRespuesta
{
    public class ProveedorPresupuestoMaterialRespuesta
    {
        public int Id { get; set; }

        public int IdPresupuesto { get; set; }

        public int IdMaterial { get; set; }

        public double PrecioCif { get; set; }

        public double PrecioFob { get; set; }

 
    }
}
