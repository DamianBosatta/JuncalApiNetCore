namespace JuncalApi.Dto.DtoRequerido
{
    public class AcopladoRequerido
    {
       
        public string Patente { get; set; } = string.Empty;     

        public string? Marca { get; set; }

        public string? Año { get; set; }

        public int? IdTipo { get; set; }

        public AcopladoRequerido(string _patente,string _marca,string _año,int _idTipo)
        {
            Patente = _patente is null ? string.Empty:_patente;
            Marca = _marca is null ? string.Empty:_marca;
            Año = _año is null ? string.Empty : _año;
            IdTipo = _idTipo == 0 ? null : _idTipo;
        }
    }
}
