using AutoMapper;
using JuncalApi.Dto.DtoRequerido;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.UnidadDeTrabajo;
using Mysqlx.Cursor;

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

        public List<RemitoRespuesta> GetRemitos(int idOrden)
        {
            List<RemitoRespuesta> listaOrdenesRespuesta= new List<RemitoRespuesta>();

            var remitos = _uow.RepositorioJuncalOrden.GetRemito(idOrden);

            if(remitos.Count>0||remitos!=null)
            {
                 listaOrdenesRespuesta = _mapper.Map<List<RemitoRespuesta>>(remitos);

            }


            return listaOrdenesRespuesta;


        }

        public JuncalProveedorCuentaCorriente FacturarRemitoInterno(FacturarRemitoInternoRequerido ordenInternoRequerido)
        {
            JuncalProveedorCuentaCorriente cuentaCorriente = new JuncalProveedorCuentaCorriente();
            try
            {
                var listaPrecio = _uow?.RepositorioJuncalProveedorListaPreciosMateriales
                    .GetAllByCondition(a => a.IdProveedorListaprecios == ordenInternoRequerido.IdListaPrecio);

                if (listaPrecio != null && listaPrecio.Any())
                {
                    var precioMaterial = listaPrecio.FirstOrDefault(a => a.Id == ordenInternoRequerido.IdMaterial);

                    if (precioMaterial != null)
                    {
                        decimal dineroFacturado = (decimal)(precioMaterial.Precio * ordenInternoRequerido.Peso);

                        DateTime fechaActual = DateTime.Now;
                        

                        cuentaCorriente = new JuncalProveedorCuentaCorriente
                        {
                            IdProveedor = (int)ordenInternoRequerido.OrdenInterno.IdProveedor,
                            IdTipoMovimiento = 3,
                            Fecha = fechaActual,
                            Observacion= ordenInternoRequerido.Observacion is null ? "Sin Observacion" : ordenInternoRequerido.Observacion.ToString(),
                            Importe = dineroFacturado,
                            Peso= (double?)ordenInternoRequerido.Peso,
                            IdMaterial= ordenInternoRequerido.IdMaterial,
                            IdRemito=ordenInternoRequerido.OrdenInterno.Id,
                            IdUsuario=ordenInternoRequerido.IdUsuario
                        };

                        return cuentaCorriente;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción específica o registrar el error
                Console.WriteLine($"Error al facturar remito interno: {ex.Message}");
                // También puedes lanzar la excepción para manejarla en niveles superiores
                throw;
            }

            // Retorno predeterminado si no se cumplen las condiciones anteriores
            return cuentaCorriente;
        }


    }
}
