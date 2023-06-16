using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrdenMarterial:RepositorioGenerico<JuncalOrdenMarterial>,IRepositorioJuncalOrdenMarterial
    {
        public RepositorioJuncalOrdenMarterial(JuncalContext db) : base(db)
        {
        }
        
        public ItemDataMateriales DataMaterial(int idAceria, int idOrden, string codigoMateria)
        {

            var query = from ordenMaterial in _db.JuncalOrdenMarterials.Where(a => a.IdOrden == idOrden) //materiales de la orden                    
                        join aceriaMaterial in _db.JuncalAceriaMaterials.Where(a => a.IdAceria == idAceria && a.Cod == codigoMateria) on
                        ordenMaterial.IdMaterial equals aceriaMaterial.IdMaterial
                        select new ItemDataMateriales(aceriaMaterial,ordenMaterial);


            return query.FirstOrDefault();


        }
    }
}
