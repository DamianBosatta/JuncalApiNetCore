using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRespuesta;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalFactura : RepositorioGenerico<JuncalFactura>, IRepositorioJuncalFactura
    {
        public RepositorioJuncalFactura(JuncalContext db) : base(db)
        {
        }

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
                            NombreUsuario = facturas.NombreUsuario,
                            JuncalFacturaMateriales = facturaMaterialGroup.ToList()


                        };

            return query.ToList();
        }


    }
}
