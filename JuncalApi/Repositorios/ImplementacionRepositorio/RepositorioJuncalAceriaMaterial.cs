using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Codigos_Utiles;
using JuncalApi.Repositorios.InterfaceRepositorio;
using System.Drawing;


namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalAceriaMaterial:RepositorioGenerico<JuncalAceriaMaterial>,IRepositorioJuncalAceriaMaterial
    {
        public RepositorioJuncalAceriaMaterial(JuncalContext db) : base(db)
        {
        }

        #region GetAceriaMaterialesForIdAceria

        /// <summary>
        /// Obtiene una lista de objetos JuncalAceriaMaterial para el Id de la acería especificado.
        /// </summary>
        /// <param name="idAceria">Id de la acería</param>
        /// <returns>Lista de objetos JuncalAceriaMaterial</returns>
        public List<JuncalAceriaMaterial> GetAceriaMaterialesForIdAceria(int idAceria)
        {
            // Consulta para obtener los registros relacionados con la acería y el material
            var query = from aceriaMaterial in _db.JuncalAceriaMaterials
                        .Where(a => a.IdAceria == idAceria && a.Isdeleted == false)
                        join material in _db.JuncalMaterials.Where(a => a.Isdeleted == false)
                        on aceriaMaterial.IdMaterial equals material.Id
                        select new { aceriaMaterial, material };

            // Lista para almacenar los materiales
            List<JuncalAceriaMaterial> materials = new List<JuncalAceriaMaterial>();

            // Iterar sobre los resultados de la consulta
            foreach (var objQuery in query)
            {
                // Crear una instancia de JuncalAceriaMaterial y agregarla a la lista de materiales
                materials.Add(new JuncalAceriaMaterial(objQuery.aceriaMaterial.Id, objQuery.aceriaMaterial.Nombre, objQuery.aceriaMaterial.IdAceria,
                    objQuery.aceriaMaterial.IdMaterial, objQuery.aceriaMaterial.Cod, objQuery.material.Nombre));
            }

            // Verificar si se encontraron materiales y devolver la lista o una lista vacía
            var respuesta = materials.Count() > 0 ? materials.ToList() : new List<JuncalAceriaMaterial>();

            return respuesta;
        }

        #endregion 

    }
}
