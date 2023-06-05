namespace JuncalApi.Dto.DtoRequerido
{
    public class ContratoItemRequerido
    {
       public int? IdContrato { get; set; }

        public int? IdMaterial { get; set; }

        public decimal? Precio { get; set; }


        public ContratoItemRequerido(int _idContrato,int _idMaterial,decimal _precio)
        {
            IdContrato = _idContrato==0?null:_idContrato;
            IdMaterial = _idMaterial==0?null:_idMaterial;
            Precio = _precio==0?null:_precio;

        }
    }
}
