using AutoMapper;
using JuncalApi.Modelos.Item;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;

namespace JuncalApi.Servicios.Facturar
{
    public class FacturarServicio:IFacturarServicio
    {

        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public FacturarServicio(IUnidadDeTrabajo uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }

        public List<ItemFacturado> itemFacturados(List<JuncalPreFacturar> listaPreFacturados)
        {         

            return _uow.RepositorioJuncalPreFactura.GetAgrupamientoFacturacion(listaPreFacturados);
            

        }
    }
}
