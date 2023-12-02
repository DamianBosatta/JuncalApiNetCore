using AutoMapper;
using JuncalApi.Dto.DtoRequerido.DtoFacturarOrden;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Codigos_Utiles;
using JuncalApi.UnidadDeTrabajo;


namespace JuncalApi.Servicios.Remito
{
    public class ServiceRemito:IServiceRemito
    {
        private readonly IUnidadDeTrabajo _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceRemito> _logger;

        public ServiceRemito(IUnidadDeTrabajo uow, IMapper mapper, ILogger<ServiceRemito>logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        #region OBTENER REMITOS
        /// <summary>
        /// Obtiene una lista de remitos relacionados con una orden específica.
        /// </summary>
        /// <param name="idOrden">Identificador único de la orden para la cual se desean obtener los remitos.</param>
        /// <returns>Lista de objetos RemitoRespuesta.</returns>
        public List<RemitoRespuesta> GetRemitos(int idOrden)
        {
            List<RemitoRespuesta> listaOrdenesRespuesta = new List<RemitoRespuesta>();

            try
            {
                var remitos = _uow.RepositorioJuncalOrden.GetRemito(idOrden);

                if (remitos != null && remitos.Count > 0)
                {
                    listaOrdenesRespuesta = _mapper.Map<List<RemitoRespuesta>>(remitos);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener remitos");
                throw;
            }

            return listaOrdenesRespuesta;
        }
        #endregion

    }
}
