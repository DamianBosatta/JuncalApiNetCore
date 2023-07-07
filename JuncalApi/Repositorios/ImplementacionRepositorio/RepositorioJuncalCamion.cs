using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCamion : RepositorioGenerico<JuncalCamion>, IRepositorioJuncalCamion
    {
      

        public RepositorioJuncalCamion(JuncalContext db) : base(db)
        {
        }

        #region GetCamiones

        /// <summary>
        /// Obtiene una lista de objetos JuncalCamion que representan los camiones.
        /// </summary>
        /// <returns>Lista de objetos JuncalCamion</returns>
        public List<JuncalCamion> GetCamiones()
        {
            // Consulta para obtener los camiones
            var query = (from camion in _db.JuncalCamions.Where(a => a.Isdeleted == false)
                         join chofer in _db.JuncalChofers.Where(a => a.Isdeleted == false)
                         on camion.IdChofer equals chofer.Id into JoinChofer
                         from jchofer in JoinChofer.DefaultIfEmpty()
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
                             IdChofer = camion.IdChofer,
                             IdTransportista = camion.IdTransportista,
                             IdInterno = camion.IdInterno,
                             IdTipoCamion = camion.IdTipoCamion,
                             ApellidoChofer= jchofer.Apellido,
                             NombreChofer = jchofer.Nombre,
                             NombreTransportista = jtransportista.Nombre,
                             DescripcionTipoCamion = jtipoCamion.Nombre
                         });

            // Ejecutar la consulta y obtener los resultados como una lista
            return query.ToList();
        }

        #endregion GetCamiones

    }
}
    
    
    

