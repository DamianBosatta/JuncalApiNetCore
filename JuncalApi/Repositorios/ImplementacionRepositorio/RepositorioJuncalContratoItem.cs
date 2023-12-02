using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalContratoItem:RepositorioGenerico<JuncalContratoItem>,IRepositorioJuncalContratoItem
    {
        public RepositorioJuncalContratoItem(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region GET CONTRATO ITEM FOR ID CONTRATO

        /// <summary>
        /// Obtiene una lista de objetos JuncalContratoItem que representan los elementos de un contrato específico.
        /// </summary>
        /// <param name="idContrato">ID del contrato</param>
        /// <returns>Lista de objetos JuncalContratoItem</returns>
        public List<JuncalContratoItem> GetContratoItemForIdContrato(int idContrato)
        {
            // Consulta para obtener los elementos del contrato para el ID de contrato dado
            var query = from itemContrato in _db.JuncalContratoItems.Where(a => a.IdContrato == idContrato && a.Isdeleted == false)
                        join aceriaMaterial in _db.JuncalAceriaMaterials.Where(a => a.Isdeleted == false)
                        on itemContrato.IdMaterial equals aceriaMaterial.Id
                        join material in _db.JuncalMaterials.Where(a => a.Isdeleted == false)
                        on aceriaMaterial.IdMaterial equals material.Id
                        select new { itemContrato, material };

            // Crear una lista de objetos JuncalContratoItem a partir de los resultados de la consulta
            List<JuncalContratoItem> itemsContrato = query.Select(objQuery => new JuncalContratoItem(
                objQuery.itemContrato.Id,
                objQuery.itemContrato.IdContrato,
                objQuery.itemContrato.IdMaterial,
                objQuery.itemContrato.Precio,
                objQuery.material.Nombre))
                .ToList();

            // Devolver la lista de elementos del contrato
            return itemsContrato;
        }

        #endregion GetContratoItemForIdContrato

        #region GET PRECIO MATERIAL

        public decimal GetPrecioMaterial(int idContrato, int idMaterial)
        {
            var query = (from itemMaterial in _db.JuncalContratoItems
                         join contrato in _db.JuncalContratos
                         on itemMaterial.IdContrato equals contrato.Id
                         join material in _db.JuncalMaterials
                         on itemMaterial.IdMaterial equals material.Id
                         select new { itemMaterial, contrato, material });


            return query.FirstOrDefault().itemMaterial.Precio;




        }

        #endregion
    }
}
