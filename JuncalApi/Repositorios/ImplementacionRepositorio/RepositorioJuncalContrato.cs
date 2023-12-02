using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;


namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalContrato: RepositorioGenerico<JuncalContrato>, IRepositorioJuncalContrato
    {
        public RepositorioJuncalContrato(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region GET CONTRATOS

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

        #region CAMBIAR ESTADOS
        public int cambiarEstado(DateTime fecha)
        {
            var query = _db.JuncalContratos
                        .Where(a => a.FechaVencimiento < fecha && !a.Isdeleted && a.Activo)
                        .ToList();

            int cambiosEstados = 0;
            foreach (var objQuery in query)
            {
                objQuery.Activo = false;
                _db.Entry(objQuery).State = EntityState.Modified;
                cambiosEstados++;
            }
            _db.SaveChanges();
            return cambiosEstados;
        }
        #endregion cambiarEstado

    }
}
