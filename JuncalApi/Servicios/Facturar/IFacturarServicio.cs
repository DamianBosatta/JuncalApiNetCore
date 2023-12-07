using JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido;
using JuncalApi.Dto.DtoRequerido.DtoFacturarOrden;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;


namespace JuncalApi.Servicios.Facturar
{
    public interface IFacturarServicio
    {
        public void Facturacion(List<AgrupacionPreFacturar> listPreFacturar, out List<int> ordenesFacturadas, out int cantidadMaterialesFacturados, out List<DtoRespuestaFacturar> RespuestaFacturacion);
        public JuncalProveedorCuentaCorriente FacturarRemitoInterno(FacturarRemitoInternoRequerido ordenInternoRequerido);
        public List<JuncalProveedorCuentaCorriente> FacturarRemitoExterno(List<AgrupacionPreFacturar> agrupPreFacturar);
    }
}
