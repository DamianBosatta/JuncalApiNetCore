using JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido;
using JuncalApi.Dto.DtoRequerido.DtoFacturarOrden;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;


namespace JuncalApi.Servicios.Facturar
{
    public interface IFacturarServicio
    {
        public void FacturacionMateriales(List<AgrupacionPreFacturar> listPreFacturar, out List<int> ordenesFacturadas, out int cantidadMaterialesFacturados);
        public JuncalProveedorCuentaCorriente FacturarRemitoInterno(FacturarRemitoInternoRequerido ordenInternoRequerido);
        public JuncalProveedorCuentaCorriente FacturarRemitoExterno(FacturarOrdenRequerido ordenFacturar);
    }
}
