namespace JuncalApi.Dto.DtoExcel
{
    public class ExcelGenerico
    {
        public string DescripcionContrato { get; set;}

        public DateTime FechaRemito { get; set;}//

        public string Remito { get; set;}  //

        public string NombreMaterial{ get; set;}//

        public decimal KgDescargado { get; set;}//

        public decimal KgBruto { get; set;}//

        public decimal KgTara { get; set;}//

        public string NombreCliente { get; set;}

        public string Chofer { get; set;}//

        public bool DiferenciaMaterial { get; set; }

        public bool DiferenciaPeso { get; set; }

    }
}
