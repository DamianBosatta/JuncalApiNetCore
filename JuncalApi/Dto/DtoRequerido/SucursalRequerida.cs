using System.Drawing;

namespace JuncalApi.Dto.DtoRequerido
{
    public class SucursalRequerida
    {
       public int? Numero { get; set; }

        public string Nombre { get; set; } = string.Empty;


        public SucursalRequerida(int? numero, string nombre)
        {
            Numero = numero is null? 0 : numero;
            Nombre = nombre is null ? string.Empty: nombre;
        }
    }
}
