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

        public JuncalCuentasCorriente FacturarRemitoInterno(FacturarRemitoInternoRequerido ordenInternoRequerido)
        {
            JuncalCuentasCorriente cuentaCorriente = new JuncalCuentasCorriente();
            try
            {
                var listaPrecio = _uow?.RepositorioJuncalProveedorListaPreciosMateriales
                    .GetAllByCondition(a => a.IdProveedorListaprecios == ordenInternoRequerido.IdListaPrecio);

                if (listaPrecio != null && listaPrecio.Any())
                {
                    var precioMaterial = listaPrecio.FirstOrDefault(a => a.IdMaterialJuncal == ordenInternoRequerido.IdMaterial);

                    if (precioMaterial != null)
                    {
                        decimal dineroFacturado = (decimal)(precioMaterial.Precio * ordenInternoRequerido.Peso);

                        DateTime fechaActual = DateTime.Now;
                        TimeOnly horaActual = TimeOnly.FromDateTime(DateTime.Now);

                        cuentaCorriente = new JuncalCuentasCorriente
                        {
                            IdProvedoor = (int)ordenInternoRequerido.OrdenInterno.IdProveedor,
                            IdTipoMoviento = 3,
                            Fecha = fechaActual,
                            Hora = horaActual,
                            Importe = dineroFacturado,
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
