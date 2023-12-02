using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalFactura : RepositorioGenerico<JuncalFactura>, IRepositorioJuncalFactura
    {
        public RepositorioJuncalFactura(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }
       
        #region JUNCAL FACTURA LIST
        public List<FacturaRespuesta> JuncalFacturaList()
        {
            var query = from facturas in _db.JuncalFacturas
                        join facturaMaterial in _db.JuncalFacturaMateriales
                        on facturas.Id equals facturaMaterial.IdFactura into facturaMaterialGroup
                        from facturaMaterial in facturaMaterialGroup.DefaultIfEmpty()
                        select new FacturaRespuesta
                        {
                            Id = facturas.Id,
                            Destinatario = facturas.Destinatario,
                            Direccion = facturas.Direccion,
                            Cuit = facturas.Cuit,
                            ContratoNumero = facturas.ContratoNumero,
                            ContratoNombre = facturas.ContratoNombre,
                            NumeroFactura = facturas.NumeroFactura,
                            Fecha = facturas.Fecha,
                            TotalFactura = facturas.TotalFactura,
                            NombreUsuario = facturas.NombreUsuario
                        };

            var result = query.ToList(); // Ejecutar consulta LINQ-to-SQL

            // Después de obtener los resultados de la base de datos, obtén los materiales de factura en memoria
            foreach (var facturaRespuesta in result)
            {
                facturaRespuesta.listaMateriales = _db.JuncalFacturaMateriales
                                                              .Where(fm => fm.IdFactura == facturaRespuesta.Id)
                                                              .ToList();
            }

            return result;
        }
        #endregion

        #region GET BY NUMERO FACTURA
        public FacturaRespuesta GetByNumeroFactura(string numeroFactura)
        {
            var query = from facturas in _db.JuncalFacturas
                        join facturaMaterial in _db.JuncalFacturaMateriales
                        on facturas.Id equals facturaMaterial.IdFactura into facturaMaterialGroup
                        from facturaMaterial in facturaMaterialGroup.DefaultIfEmpty()
                        where facturas.NumeroFactura == numeroFactura
                        select new FacturaRespuesta
                        {
                            Id = facturas.Id,
                            Destinatario = facturas.Destinatario,
                            Direccion = facturas.Direccion,
                            Cuit = facturas.Cuit,
                            ContratoNumero = facturas.ContratoNumero,
                            ContratoNombre = facturas.ContratoNombre,
                            NumeroFactura = facturas.NumeroFactura,
                            Fecha = facturas.Fecha,
                            TotalFactura = facturas.TotalFactura,
                            NombreUsuario = facturas.NombreUsuario
                        };

            var result = query.FirstOrDefault(); // Ejecutar consulta LINQ-to-SQL

            if (result != null)
            {
                result.listaMateriales = _db.JuncalFacturaMateriales
                                                    .Where(fm => fm.IdFactura == result.Id)
                                                    .ToList();
            }

            return result;
        }
        #endregion
    }
}
