using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Codigos_Utiles;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrdenMarterial:RepositorioGenerico<JuncalOrdenMarterial>,IRepositorioJuncalOrdenMarterial
    {
        public RepositorioJuncalOrdenMarterial(JuncalContext db) : base(db)
        {
        }

        #region GetDatosMaterialesAndRemitoExcel

        /// <summary>
        /// Obtiene una lista de objetos ItemDataMateriales que contienen datos de materiales y remitos para una acería y lista de remitos específicos.
        /// </summary>
        /// <param name="idAceria">ID de la acería</param>
        /// <param name="remito">Lista de remitos</param>
        /// <returns>Lista de objetos ItemDataMateriales</returns>
        public List<ItemDataMateriales> GetDatosMaterialesAndRemitoExcel(int idAceria, List<string> remito)
        {
            // Consulta para obtener los datos de materiales y remitos para la acería y remitos específicos
            var query = from aceriaMaterial in _db.JuncalAceriaMaterials
                        .Where(a => a.Isdeleted == false && a.IdAceria == idAceria)
                        join material in _db.JuncalMaterials
                        on aceriaMaterial.IdMaterial equals material.Id into joinMaterial
                        from jMaterial in joinMaterial.DefaultIfEmpty()
                        join ordenMaterial in _db.JuncalOrdenMarterials.Where(a=>a.FacturadoParcial==false)
                        on jMaterial.Id equals ordenMaterial.IdMaterial into joinOrdenMaterial
                        from jOrdenMaterial in joinOrdenMaterial.DefaultIfEmpty()
                        join orden in _db.JuncalOrdens.Where(a => remito.Contains(a.Remito.Trim()) &&
                        a.Isdeleted == false && a.IdAceria == idAceria &&
                        a.IdEstado == Codigos.Enviado)
                        on jOrdenMaterial.IdOrden equals orden.Id into joinOrden
                        from jOrden in joinOrden.DefaultIfEmpty()
                        where jOrden != null && (jMaterial.Isdeleted == false && jOrdenMaterial.Isdeleted == false)
                        select new ItemDataMateriales(aceriaMaterial, jOrdenMaterial, jOrden, jMaterial);

            // Devolver la lista de objetos ItemDataMateriales
            return query.ToList();
        }

        #endregion GetDatosMaterialesAndRemitoExcel

    }
}
