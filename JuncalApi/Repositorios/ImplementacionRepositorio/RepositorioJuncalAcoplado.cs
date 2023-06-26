using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalAcoplado:RepositorioGenerico<JuncalAcoplado>,IRepositorioJuncalAcoplado
    {
        public RepositorioJuncalAcoplado(JuncalContext db) : base(db)
        {
        }

        public List<JuncalAcoplado> GetAcoplados()
        {
           

            var query = (from acoplado in _db.JuncalAcoplados.Where(a => a.Isdeleted == false)
                        join tipoAcoplado in _db.JuncalTipoAcoplados
                        on acoplado.IdTipo equals tipoAcoplado.Id
                        select new JuncalAcoplado{ 
                        Id=acoplado.Id,
                        Patente=acoplado.Patente,
                        Marca=acoplado.Marca,
                        Año=acoplado.Año,
                        IdTipo=acoplado.IdTipo,
                        TipoAcoplado=tipoAcoplado.Nombre                        
                        
                        });

     
            return query.ToList();

        }
    }
}
