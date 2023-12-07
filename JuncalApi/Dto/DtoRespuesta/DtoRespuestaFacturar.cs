namespace JuncalApi.Dto.DtoRespuesta
{
    public class DtoRespuestaFacturar
    {
        public int IdProveedor { get; set; } = 0;
       
        public string NroRemito {  get; set; }= string.Empty;

        public decimal TotalFacturado { get; set; } = 0;

       
    }
}
