namespace JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido
{
    public class Referencia
    {
        public int IdOrden { get; set; }

        public List<ReferenciaMaterialesEnviados> MaterialesEnviados { get; set; }= new List<ReferenciaMaterialesEnviados>();
    }

    public class ReferenciaMaterialesEnviados
    {
        public int idMaterial { get; set; }

        public int idPrefactura { get; set; }


    }
}
