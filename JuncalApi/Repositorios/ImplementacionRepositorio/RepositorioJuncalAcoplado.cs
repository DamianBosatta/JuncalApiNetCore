using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalAcoplado:RepositorioGenerico<JuncalAcoplado>,IRepositorioJuncalAcoplado
    {
        public RepositorioJuncalAcoplado(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region GET ACOPLADOS

        /// <summary>
        /// Obtiene una lista de objetos JuncalAcoplado que representan los acoplados.
        /// </summary>
        /// <returns>Lista de objetos JuncalAcoplado</returns>
        public List<JuncalAcoplado> GetAcoplados()
        {
            // Consulta para obtener los acoplados
            var query = (from acoplado in _db.JuncalAcoplados.Where(a => a.Isdeleted == false)
                         join tipoAcoplado in _db.JuncalTipoAcoplados
                         on acoplado.IdTipo equals tipoAcoplado.Id
                         select new JuncalAcoplado
                         {
                             Id = acoplado.Id,
                             Patente = acoplado.Patente,
                             Marca = acoplado.Marca,
                             Año = acoplado.Año,
                             IdTipo = acoplado.IdTipo,
                             TipoAcoplado = tipoAcoplado.Nombre
                         });

            // Ejecutar la consulta y obtener los resultados como una lista
            return query.ToList();
        }

        #endregion GetAcoplados

    }
}
