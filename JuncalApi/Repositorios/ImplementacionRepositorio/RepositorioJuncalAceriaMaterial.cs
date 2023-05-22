using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalAceriaMaterial:RepositorioGenerico<JuncalAceriaMaterial>,IRepositorioJuncalAceriaMaterial
    {
        public RepositorioJuncalAceriaMaterial(JuncalContext db) : base(db)
        {
        }

        public List<ItemAceriaMaterial> GetAceriaMaterialesForId(int idAceria)
        {
            var query = from aceriaMaterial in _db.JuncalAceriaMaterials.Where(a => a.IdAceria == idAceria && a.Isdeleted == false)
                        join material in _db.JuncalMaterials.Where(a => a.Isdeleted == false)
                        on aceriaMaterial.IdMaterial equals material.Id
                        select new {aceriaMaterial, material};
                         
            List<ItemAceriaMaterial> materials = new List<ItemAceriaMaterial>();

            foreach(var q in query)
            {
                materials.Add(new ItemAceriaMaterial(q.aceriaMaterial, q.material));

            }
         
          var respuesta = materials.Count()>0? materials.ToList() : new List<ItemAceriaMaterial>();


            return respuesta;
        }
    }
}
