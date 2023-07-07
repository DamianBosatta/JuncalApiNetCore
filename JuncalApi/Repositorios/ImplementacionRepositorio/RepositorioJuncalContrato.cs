using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;


namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalContrato: RepositorioGenerico<JuncalContrato>, IRepositorioJuncalContrato
    {
        public RepositorioJuncalContrato(JuncalContext db) : base(db)
        {
        }

        #region GetContratos

        /// <summary>
        /// Obtiene una lista de objetos JuncalContrato que representan los contratos.
        /// </summary>
        /// <returns>Lista de objetos JuncalContrato</returns>
        public List<JuncalContrato> GetContratos()
        {
            // Consulta para obtener los contratos
            var query = from contrato in _db.JuncalContratos.Where(a => a.Isdeleted == false)
                        join aceria in _db.JuncalAceria.Where(a => a.Isdeleted == false)
                        on contrato.IdAceria equals aceria.Id
                        select new { contrato, aceria };

            // Lista para almacenar los contratos
            List<JuncalContrato> listContrato = new List<JuncalContrato>();

            // Iterar sobre los resultados de la consulta
            foreach (var objQuery in query)
            {
                // Crear una instancia de JuncalContrato y agregarla a la lista de contratos
                listContrato.Add(new JuncalContrato(objQuery.contrato.Id, objQuery.contrato.Nombre, objQuery.contrato.Numero, (DateTime)objQuery.contrato.FechaVigencia,
                    (DateTime)objQuery.contrato.FechaVencimiento, (int)objQuery.contrato.IdAceria, objQuery.contrato.Activo, objQuery.contrato.ValorFlete,
                    objQuery.aceria.Nombre));
            }

            // Devolver la lista de contratos
            return listContrato;
        }

        #endregion GetContratos

    }
}
