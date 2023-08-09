using JuncalApi.DataBase;
using JuncalApi.Dto.DtoRequerido.DtoAgrupacionRequerido;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Codigos_Utiles;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;
using System.Data;
using System.Linq;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalOrdenMarterial : RepositorioGenerico<JuncalOrdenMarterial>, IRepositorioJuncalOrdenMarterial
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
        public List<ItemDataMateriales> GetDatosMaterialesAndRemitoExcel(int idAceria, List<string> remito, List<string> listaCodigos)
        {
            // Obtener las órdenes que cumplen las condiciones específicas
            var ordenes = _db.JuncalOrdens
                .Where(a => remito.Contains(a.Remito.Trim()) && a.Isdeleted == false &&
                            a.IdAceria == idAceria && a.IdEstado == Codigos.Enviado)
                .ToList();

            // Obtener los identificadores de las órdenes que cumplen las condiciones
            var ordenIds = ordenes.Select(o => o.Id).ToList();

            // Obtener los materiales asociados a las órdenes que cumplen la condición de FacturadoParcial == false
            var ordenesMateriales = _db.JuncalOrdenMarterials
                .Where(a => a.FacturadoParcial == false && ordenIds.Contains(a.IdOrden))
                .ToList();

            // Obtener los identificadores de los materiales asociados a las ordenesMateriales
            var materialIds = ordenesMateriales.Select(om => om.IdMaterial).ToList();

            // Obtener los registros de JuncalMaterials asociados a los JuncalOrdenMaterials anteriores y que cumplen la condición de Isdeleted == false
            var materiales = _db.JuncalMaterials
                .Where(material => materialIds.Contains(material.Id) && material.Isdeleted == false)
                .ToList();

            // Obtener los registros de JuncalAceriaMaterials asociados a la aceria específica (idAceria)
            var aceriaMateriales = _db.JuncalAceriaMaterials
                .Where(a => a.IdAceria == idAceria && listaCodigos.Contains(a.Cod))
                .ToList();

            // Combinar los resultados para obtener el resultado final
            var resultado = from orden in ordenes
                            join ordenMaterial in ordenesMateriales
                                on orden.Id equals ordenMaterial.IdOrden into joinOrdenMaterial
                            from jOrdenMaterial in joinOrdenMaterial.DefaultIfEmpty()
                            join material in materiales
                                on jOrdenMaterial.IdMaterial equals material.Id into joinMaterial
                            from jMaterial in joinMaterial.DefaultIfEmpty()
                            join aceriaMaterial in aceriaMateriales
                                on jMaterial.Id equals aceriaMaterial.IdMaterial into joinAceriaMaterial
                            from jAceriaMaterial in joinAceriaMaterial.DefaultIfEmpty()
                            where orden != null
                            select new
                            {
                                orden,
                                jOrdenMaterial,
                                jAceriaMaterial,
                                jMaterial
                            };




            // Finalmente, puedes trabajar con el resultado para realizar las operaciones necesarias
            // resultado.ToList() o cualquier otra operación que necesites.

            // Materializar la consulta en una lista
            var itemList = resultado.ToList();

            // Obtener la lista de códigos de materiales utilizando la lista resultante
            var codigosMateriales = _db.JuncalAceriaMaterials
        .Where(a => a.IdAceria == idAceria && a.Isdeleted == false)
        .Select(a => a.Cod)
        .ToList();


            // Convertir la lista anónima a una lista de objetos ItemDataMateriales
            var result = itemList.Select(item => new ItemDataMateriales(
                item.jAceriaMaterial,
                item.jOrdenMaterial,
                item.orden,
                item.jMaterial,
                codigosMateriales // Usar la lista de códigos de materiales previamente obtenida
            )).ToList();

            return result;
        }
        #endregion GetDatosMaterialesAndRemitoExcel

        #region OBTENER MATERIALES POR LISTA DE ID ORDENES

        public List<JuncalOrdenMarterial> ObtenerMaterialesPorListaDeOrdenes(List<int> idOrdenes)
        {

                                        return (from orden in idOrdenes
                                                join material in _db.JuncalOrdenMarterials
                                                    on orden equals material.IdOrden
                                                where !material.Isdeleted
                                                select new JuncalOrdenMarterial
                                                {
                                                    Id = material.Id,
                                                    IdMaterial = material.IdMaterial,
                                                    Peso = (decimal)material.Peso,
                                                    NumFactura = material.NumFactura,
                                                    FacturadoParcial = material.FacturadoParcial,
                                                    IdOrden = orden
                                                }).ToList();

           

        }

        #endregion
        

        
    }
}









