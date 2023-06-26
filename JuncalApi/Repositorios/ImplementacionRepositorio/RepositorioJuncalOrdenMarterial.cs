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
        
        public List<ItemDataMateriales> DataMaterial(int idAceria, List<string> remito)
        {

            var query = from aceriaMaterial in _db.JuncalAceriaMaterials
                        .Where(a => a.Isdeleted == false && a.IdAceria == idAceria)
                        join material in _db.JuncalMaterials
                        on aceriaMaterial.IdMaterial equals material.Id into joinMaterial
                        from jMaterial in joinMaterial.DefaultIfEmpty()
                        join ordenMaterial in _db.JuncalOrdenMarterials
                        on jMaterial.Id equals ordenMaterial.IdMaterial into joinOrdenMaterial
                        from jOrdenMaterial in joinOrdenMaterial.DefaultIfEmpty()
                        join orden in _db.JuncalOrdens.Where(a => remito.Contains(a.Remito.Trim()) &&
                        a.Isdeleted == false && a.IdAceria == idAceria)
                        on jOrdenMaterial.IdOrden equals orden.Id into joinOrden
                        from jOrden in joinOrden.DefaultIfEmpty()
                        where jOrden != null && (jMaterial.Isdeleted==false && jOrdenMaterial.Isdeleted==false)
                        select new ItemDataMateriales(aceriaMaterial, jOrdenMaterial, jOrden,jMaterial);

           
           
            return query.ToList();
        }
    }
}
