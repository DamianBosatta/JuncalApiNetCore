namespace JuncalApi.Dto.DtoRequerido
{
    public class AceriaMaterialRequerido
    {
      
       public string Nombre { get; set; } = string.Empty;

        public int? IdAceria { get; set; }

        public int? IdMaterial { get; set; }

        public string Cod { get; set; } = string.Empty;



        public AceriaMaterialRequerido(int _idAceria,int _idMaterial,string _nombre,string _cod)
        {
            Nombre = _nombre is null ? string.Empty :_nombre;
            IdAceria = _idAceria != 0 ? _idAceria : null;
            IdMaterial = _idMaterial != 0 ? _idMaterial : null;
            Cod = _cod is null ? string.Empty:_cod;
        }
    }
}
