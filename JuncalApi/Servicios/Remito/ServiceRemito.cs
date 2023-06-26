using AutoMapper;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;

namespace JuncalApi.Servicios.Remito
{
    public class ServiceRemito:IServiceRemito
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;

        public ServiceRemito(IUnidadDeTrabajo uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;

        }

        public List<RemitoResponse> GetRemitos(int idOrden)
        {
            List<RemitoResponse> listaOrdenesRespuesta= new List<RemitoResponse>();

            var remitos = _uow.RepositorioJuncalOrden.GetRemito(idOrden);

            if(remitos.Count>0||remitos!=null)
            {
                 listaOrdenesRespuesta = _mapper.Map<List<RemitoResponse>>(remitos);

            }


            return listaOrdenesRespuesta;


        }



    }
}
