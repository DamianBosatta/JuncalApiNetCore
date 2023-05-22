using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalContratoItem:RepositorioGenerico<JuncalContratoItem>,IRepositorioJuncalContratoItem
    {
        public RepositorioJuncalContratoItem(JuncalContext db) : base(db)
        {
        }

        public List<ItemContratoItem> GetContratoItemForIdContrato(int idContrato)
        {
            var  query = from itemContrato in _db.JuncalContratoItems.Where(a=>a.IdContrato==idContrato && a.Isdeleted==false)
                         join  material in _db.JuncalMaterials.Where(a=>a.Isdeleted==false)
                         on itemContrato.IdMaterial equals material.Id
                         select new { itemContrato, material };


        List<ItemContratoItem> itemsContrato = new List<ItemContratoItem>();


            foreach(var q in query)
            {
                itemsContrato.Add(new ItemContratoItem(q.itemContrato, q.material));

            }

            var respuesta = itemsContrato.Count()>0? itemsContrato.ToList() : new List<ItemContratoItem>();

            return respuesta;

        }
    }
}
