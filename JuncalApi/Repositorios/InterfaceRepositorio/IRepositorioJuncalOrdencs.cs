using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;

namespace JuncalApi.Repositorios.InterfaceRepositorio
{
    public interface IRepositorioJuncalOrdencs:IRepositorioGenerico<JuncalOrden>
    {
        public List<RemitoRespuesta>? GetRemito(int idOrden);

        public List<RemitosPendientesRespuesta> GetRemitosPendientes();

        public List<ReporteAceria> ReporteAcerias(DateTime fechaReporte);


    }
}
