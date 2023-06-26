using JuncalApi.DataBase;
using JuncalApi.Modelos;
using JuncalApi.Modelos.Item;
using JuncalApi.Repositorios.InterfaceRepositorio;

namespace JuncalApi.Repositorios.ImplementacionRepositorio
{
    public class RepositorioJuncalCamion : RepositorioGenerico<JuncalCamion>, IRepositorioJuncalCamion
    {
      

        public RepositorioJuncalCamion(JuncalContext db) : base(db)
        {
        }


        public List<JuncalCamion> GetCamiones()
        {
            var query = (from camion in _db.JuncalCamions.Where(a=>a.Isdeleted==false)
                        join chofer in _db.JuncalChofers.Where(a=>a.Isdeleted==false)
                        on camion.IdChofer equals chofer.Id into JoinChofer
                        from jchofer in JoinChofer.DefaultIfEmpty()
                        join transportista in _db.JuncalTransportista.Where(a=>a.Isdeleted==false)
                        on camion.IdTransportista equals transportista.Id into JoinTransportista
                        from jtransportista in JoinTransportista.DefaultIfEmpty()
                        join tipoCamion in _db.JuncalTipoCamions 
                        on camion.IdTipoCamion equals tipoCamion.Id into JoinTipoCamion
                        from jtipoCamion in JoinTipoCamion.DefaultIfEmpty()
                        select new JuncalCamion
                        {
                           Id=camion.Id,
                           Patente=camion.Patente,
                           Marca=camion.Marca,
                           Tara=camion.Tara,
                           IdChofer=camion.IdChofer,
                           IdTransportista=camion.IdTransportista,
                           IdInterno=camion.IdInterno,
                           IdTipoCamion=camion.IdTipoCamion,
                           NombreChofer=jchofer.Nombre,
                           NombreTransportista=jtransportista.Nombre,
                           DescripcionTipoCamion=jtipoCamion.Nombre
                        
                        });
       

            return query.ToList();

        }
    }
}
    
    
    

