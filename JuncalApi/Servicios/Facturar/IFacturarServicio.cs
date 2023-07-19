using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Servicios.Facturar
{
    public interface IFacturarServicio
    {
        public List<ItemFacturado> itemFacturados(List<JuncalPreFacturar> listaPreFacturados);
    }
}
