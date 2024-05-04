using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCamion : RepositorioGenerico<JuncalCamion>, IRepositorioJuncalCamion
    {
      

        public RepositorioJuncalCamion(JuncalContext db, ILogger logger) : base(db, logger)
        {
        }

        #region GET CAMIONES

        /// <summary>
        /// Obtiene una lista de objetos JuncalCamion que representan los camiones.
        /// </summary>
        /// <returns>Lista de objetos JuncalCamion</returns>
        public List<JuncalCamion> GetCamiones()
        {
            // Consulta para obtener los camiones
            var query = (from camion in _db.JuncalCamions.Where(a => a.Isdeleted == false)                       
                         join transportista in _db.JuncalTransportista.Where(a => a.Isdeleted == false)
                         on camion.IdTransportista equals transportista.Id into JoinTransportista
                         from jtransportista in JoinTransportista.DefaultIfEmpty()
                         join tipoCamion in _db.JuncalTipoCamions
                         on camion.IdTipoCamion equals tipoCamion.Id into JoinTipoCamion
                         from jtipoCamion in JoinTipoCamion.DefaultIfEmpty()
                         select new JuncalCamion
                         {
                             Id = camion.Id,
                             Patente = camion.Patente,
                             Marca = camion.Marca,
                             Tara = camion.Tara,                            
                             IdTransportista = camion.IdTransportista,
                             IdInterno = camion.IdInterno,
                             IdTipoCamion = camion.IdTipoCamion,                          
                             NombreTransportista = jtransportista.Nombre,
                             DescripcionTipoCamion = jtipoCamion.Nombre
                         });

            // Ejecutar la consulta y obtener los resultados como una lista
            return query.ToList();
        }

        #endregion GetCamiones

    }
}
    
    
    

