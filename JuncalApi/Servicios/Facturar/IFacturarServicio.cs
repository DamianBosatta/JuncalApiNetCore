using JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Servicios.Facturar
{
    public interface IFacturarServicio
    {
        public void Facturacion(List<AgrupacionPreFacturar> listPreFacturar, out List<int> ordenesFacturadas, out int cantidadMaterialesFacturados);
    }
}
